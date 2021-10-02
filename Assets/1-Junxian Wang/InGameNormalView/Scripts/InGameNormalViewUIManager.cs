using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameNormalViewUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI roomText;
    [SerializeField] GameObject map;
    [SerializeField] Button mapButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button createButton;
    [SerializeField] Button joinButton;
    [SerializeField] TextMeshProUGUI userInfo;
    [SerializeField] TextMeshProUGUI debug;

	private void Awake()
	{
        StartCoroutine(SharedUtilities.UpdateTime(timeText.text));

        mapButton.onClick.AddListener(() => SharedUtilities.OpenCloseGameObject(map));
        settingsButton.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.Setting));
        createButton.onClick.AddListener(() => ClassroomNetworkManager.Instance.CreateRoom());
        joinButton.onClick.AddListener(() => ClassroomNetworkManager.Instance.JoinRoom());

        userInfo.text = $"User Name: {ClassroomNetworkManager.Instance.clientID}  Room ID: {ClassroomNetworkManager.Instance.roomID}";
	}

    public void ChangeRoomText(int room)
	{
        roomText.text = $"Room: {room}";
        if (room < 0)
        {
            roomText.text = $"Room: None";
        }
    }

    public void AddDebugText(string text)
	{
        debug.text += text;
	}
}
