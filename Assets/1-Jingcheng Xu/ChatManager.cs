using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public string usrname;
    public int maxMessages = 25;
    public GameObject textObject,chatPanel;
    public TMP_InputField chatBox;
    public Color Acolor, Scolor, Pcolor;

    [SerializeField]
    GameObject player;
    [SerializeField]
    TMP_Dropdown dropdown;
    [SerializeField]
    List<string> dropitems;
    [SerializeField]
    List<Message> messagesList = new List<Message>();

    PlayerControl controls;

    private void Awake()
    {
        controls = new PlayerControl();
        controls.UI.Chating.performed += ctx => Chat();
        dropitems.Add("All");
        dropitems.Add("Stuff");
        dropitems.Add("Private");
        foreach (var item in dropitems)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }

    private void Update()
    {
        if (chatBox.isFocused)
        {
            PlayerController pc = player.GetComponentInChildren<PlayerController>();
            player.GetComponentInChildren<PlayerController>().enabled = false;
        }
        else
        {
            PlayerController pc = player.GetComponentInChildren<PlayerController>();
            player.GetComponentInChildren<PlayerController>().enabled = true;
        }
    }

    void Chat()
    {
        if (chatBox.text != "")
        {
            if (dropitems[dropdown.value] == "All")
            {
                SentToChat(chatBox.text, Message.MessageType.All);
            }
            else if (dropitems[dropdown.value] == "Stuff")
            {
                SentToChat(chatBox.text, Message.MessageType.Stuff);
            }
            else
            {
                SentToChat(chatBox.text, Message.MessageType.Private);
            }
            chatBox.text = "";
        }
        else if (!chatBox.isFocused)
        {
            chatBox.ActivateInputField();
        } 
    }

    public void SentToChat(string text, Message.MessageType messageType)
    {
        if (messagesList.Count >= maxMessages)
        {
            Destroy(messagesList[0].textObject.gameObject);
            messagesList.Remove(messagesList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = usrname+": "+text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<TMP_Text>();

        newMessage.textObject.text = newMessage.text;
        newMessage.textObject.color = MessageTypeColor(messageType);


        messagesList.Add(newMessage);

        Color MessageTypeColor(Message.MessageType messageType)
        {
            Color color = Acolor;
            switch (messageType)
            {
                case Message.MessageType.All:
                    color = Acolor;
                    break;
                case Message.MessageType.Stuff:
                    color = Scolor;
                    break;
                case Message.MessageType.Private:
                    color = Pcolor;
                    break;
            }
            return color;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public TMP_Text textObject;
        public MessageType messageType;

        public enum MessageType
        {
            All,
            Stuff,
            Private
        }
    }
}
