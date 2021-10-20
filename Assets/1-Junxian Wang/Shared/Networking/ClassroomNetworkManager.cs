using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using NativeWebSocket;
using System.Threading.Tasks;


public class ClassroomNetworkManager : Singleton<ClassroomNetworkManager>
{
	[SerializeField] ChatManager chatManager;
	[SerializeField] InGameNormalViewUIManager uiManager;

	public string Domain;
	public static WebSocket websocket;
	public bool isWebsocketConnected = false;
	public int roomID = -1;
	public string clientID = "None";

	int checkDelay_ms = 100;

	// Async condition task that awaits response of a particular request
	TaskCompletionSource<bool> joinRoomResponse;

	public void CreateRoom()
	{
		var request = new ServerMessage.CreateRoom.Request();
		SendWebSocketMessage(JsonConvert.SerializeObject(request));
	}

	public async Task CreateRoomBlocking()
	{
		var request = new ServerMessage.CreateRoom.Request();
		SendWebSocketMessage(JsonConvert.SerializeObject(request));

		while (roomID < 0)
		{
			await Task.Delay(checkDelay_ms);
		}
	}

	public void JoinRoom()
	{
		if (roomID < 0)
		{
			Debug.LogWarning("Room ID has not yet been attached to NetworkManager before joinRoom");
			return;
		}
		var request = new ServerMessage.JoinRoom.Request(roomID, clientID);
		SendWebSocketMessage(JsonConvert.SerializeObject(request));
	}

	public async Task<bool> JoinRoomBlocking()
	{
		if (roomID < 0)
		{
			throw new System.Exception("Room ID has not yet been attached to NetworkManager before joinRoom");
		}
		var request = new ServerMessage.JoinRoom.Request(roomID, clientID);
		SendWebSocketMessage(JsonConvert.SerializeObject(request));

		joinRoomResponse = new TaskCompletionSource<bool>();
		return await joinRoomResponse.Task;
	}

	public void SendChat(string msg)
	{
		if (roomID < 0)
		{
			Debug.LogWarning("Room ID has not yet been attached to NetworkManager before sendChat");
			return;
		}
		var request = new ServerMessage.Chat.Request(roomID, clientID, msg);
		SendWebSocketMessage(JsonConvert.SerializeObject(request));
	}

	public void LeaveRoom()
	{
		if (roomID < 0)
		{
			Debug.LogWarning("Room ID has not yet been attached to NetworkManager before leaveRoom");
			return;
		}
		var request = new ServerMessage.LeaveRoom.Request(roomID, clientID);
		SendWebSocketMessage(JsonConvert.SerializeObject(request));
	}

	protected override void Awake()
	{
		DontDestroy = true;
		base.Awake();
	}

	private void Update()
	{
#if !UNITY_WEBGL || UNITY_EDITOR
		websocket?.DispatchMessageQueue();
#endif
	}


	public async Task<bool> InitWebSocket(string address = "172.29.137.50")
	{
		Domain = $"ws://{address}:8002/ws";

		websocket = new WebSocket(Domain);

		TaskCompletionSource<bool> WebsocketConnectResponse = new TaskCompletionSource<bool>();

		websocket.OnOpen += () =>
		{
			Debug.Log("Connection open!");
			isWebsocketConnected = true;
			WebsocketConnectResponse.SetResult(true);
		};

		websocket.OnError += (e) =>
		{
			Debug.Log("Error! " + e);
		};

		websocket.OnClose += (e) =>
		{
			Debug.Log("Connection closed!");
			isWebsocketConnected = false;
			WebsocketConnectResponse.SetResult(false);
		};

		websocket.OnMessage += (bytes) =>
		{
			var message = System.Text.Encoding.UTF8.GetString(bytes);
			JSONResponseHandler(message);
			Debug.Log("OnMessage! " + message);
		};

		// waiting for messages
		websocket.Connect();

		return await WebsocketConnectResponse.Task;
	}

	private async void OnApplicationQuit()
	{
		// await websocket.Close();
	}

	private async void SendWebSocketMessage(string text)
	{
		if (websocket.State == WebSocketState.Open)
		{
			// Sending bytes
			// await websocket.Send(new byte[] { 10, 20, 30 });

			// Sending plain text
			await websocket.SendText(text);

			Debug.Log($"Message sent: {text}");
		}
	}

	private void JSONResponseHandler(string json)
	{
		dynamic response_raw = JsonConvert.DeserializeObject(json);
		if (response_raw is null)
		{
			return;
		}

		if (response_raw.type == "createRoom")
		{
			var response = JsonConvert.DeserializeObject<ServerMessage.CreateRoom.Response>(json);
			if (response.status.statusCode == ServerMessage.SuccessCode)
			{
				roomID = response.data.roomId;
			}
			else
			{
				// Debug.LogWarning($"Room creation failed with error code: {response.status.statusCode} {response.status.statusDesc}");
			}
		}
		else if (response_raw.type == "joinRoom")
		{
			var response = JsonConvert.DeserializeObject<ServerMessage.JoinRoom.Response>(json);
			
			if (response.status.statusCode != ServerMessage.SuccessCode)
			{
				joinRoomResponse.SetResult(false);
			}
			else
			{
				joinRoomResponse.SetResult(true);
			}
		}
		else if (response_raw.type == "leaveRoom")
		{
			var response = JsonConvert.DeserializeObject<ServerMessage.LeaveRoom.Response>(json);
		}
		else if (response_raw.type == "chat")
		{
			chatManager = FindObjectOfType<ChatManager>();
			uiManager = FindObjectOfType<InGameNormalViewUIManager>();
			uiManager.AddDebugText(json + "\n");
			var response = JsonConvert.DeserializeObject<ServerMessage.Chat.Response>(json);
			uiManager.AddDebugText($"{response.data.from} {response.data.message}" + "\n");
			chatManager.DisplayInChatList(response.data.from, response.data.message, ChatManager.Message.MessageType.All);
		}
	}
}

public class ServerMessage
{
	public static int SuccessCode = 200;

	public class CreateRoom
	{
		public class Request
		{
			public string type = "createRoom";
		}

		public class Response
		{
			public string type;
			public Status status ;
			public Data data;

			public class Status
			{
				public int statusCode;
				public string statusDesc;
			}

			public class Data
			{
				public int roomId;
			}
		}
	}

	public class JoinRoom
	{
		public class Request
		{
			public string type = "joinRoom";
			public Data data = new Data();

			public Request(int roomID, string clientID)
			{
				data.roomId = roomID;
				data.clientId = clientID;
			}

			public class Data
			{
				public int roomId;
				public string clientId;
			}
		}

		public class Response
		{
			public string type;
			public Status status;

			public class Status
			{
				public int statusCode;
				public string statusDesc;
			}
		}
	}

	public class LeaveRoom
	{
		public class Request
		{
			public string type = "leaveRoom";
			public Data data = new Data();

			public Request(int roomID, string clientID, string consumerID = "", string producerID = "")
			{
				data.roomId = roomID;
				data.clientId = clientID;
				data.consumerId = consumerID;
				data.ProducerId = producerID;
			}

			public class Data
			{
				public int roomId;
				public string clientId;
				public string consumerId;
				public string ProducerId;
			}
		}

		public class Response
		{
			public string type;
			public Status status;

			public class Status
			{
				public int statusCode;
				public string statusDesc;
			}
		}
	}


	public class Chat
	{
		public class Request
		{
			public string type = "chat";
			public Data data = new Data();
			
			public Request (int roomID, string clientID, string message)
			{
				data.roomId = roomID;
				data.clientId = clientID;
				data.message = message;
			}

			public class Data
			{
				public int roomId;
				public string clientId;
				public string message;
			}
		}

		public class Response
		{
			public string type;
			public Data data;

			public class Data
			{
				public string from;
				public string message;
			}
		}
	}
}
