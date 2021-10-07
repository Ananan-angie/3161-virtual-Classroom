using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class LoginViewUIManager : MonoBehaviour
{
    [SerializeField] Button settings;
	[SerializeField] Button registerButton;
	[SerializeField] Button registerAccButton;
	[SerializeField] Button forgotButton;
	[SerializeField] Button retrievePasswordButton;
	[SerializeField] TMP_InputField userNameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button loginButton;
    [SerializeField] GameObject invalidMessage;
    [SerializeField] TextAsset jsonFile;
	[SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField regUserNameField;
    [SerializeField] TMP_InputField regPassField;
	[SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_Text message;
    [SerializeField] GameObject successMessage;
	[SerializeField] GameObject registerPopUp;
	[SerializeField] GameObject forgotPopUp;
    [SerializeField] ModalInputWindow addressInputWndow;

	void Start()
    {
        settings.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.Setting));
        loginButton.onClick.AddListener(adminDetails);
        registerButton.onClick.AddListener(() => SharedUtilities.OpenCloseGameObject(registerPopUp));
        registerAccButton.onClick.AddListener(storeData);
        forgotButton.onClick.AddListener(() => SharedUtilities.OpenCloseGameObject(forgotPopUp));
        retrievePasswordButton.onClick.AddListener(getPassword);
        addressInputWndow.OnEnter = async input =>
        {
            if (!string.IsNullOrEmpty(input))
            {
                bool success = await ClassroomNetworkManager.Instance.InitWebSocket(input);

                if (success) addressInputWndow.gameObject.SetActive(false);
                else addressInputWndow.DisplayErrorMessage("Server not joined: Invalid address");
            }
            else addressInputWndow.DisplayErrorMessage("Address cannot be empty.");
        };

        addressInputWndow.gameObject.SetActive(!ClassroomNetworkManager.Instance.isWebsocketConnected);
    }

	void adminDetails()
    {
        //Get Username from Input
        string userName = userNameField.text;
        //Get Password from Input 
        string password = passwordField.text;

        UserDetails col = JsonUtility.FromJson<UserDetails>(jsonFile.text);
        List<UserDetail> listOfDetails = col.userdetails.ToList();

        bool found = false;
        foreach(UserDetail userdetails in listOfDetails){
            if(userdetails.username == userName && userdetails.password == password)
            {
                SharedUtilities.TransitToScene(GameScene.AppStart);
                found = true;
                ClassroomNetworkManager.Instance.clientID = userdetails.name;
                break;
            }
        }
        if( !found )
        {
            invalidMessage.SetActive(true);
        }
    }
	void storeData(){
        // Read existing json file linked in inspector
        UserDetails col = JsonUtility.FromJson<UserDetails>(jsonFile.text);

        // Convert to list so we can add new entries
        List<UserDetail> listOfDetails = col.userdetails.ToList();
        
        // Get input and add to list
        UserDetail _UserDetail = new UserDetail(){        
            name = nameField.text,
            username = regUserNameField.text,
            password = regPassField.text};
        listOfDetails.Add(_UserDetail);

        // Revert back to array
        col.userdetails = listOfDetails.ToArray();

        // This will be the new json
        string newJsonString = JsonUtility.ToJson(col);

        // Write into json file and display message
        System.IO.File.WriteAllText(Application.dataPath + "/1-Jee Ann Chua/Data/UserDetail.json", newJsonString);
        successMessage.SetActive(true);
    }

	void getPassword(){
        UserDetails col = JsonUtility.FromJson<UserDetails>(jsonFile.text);
        List<UserDetail> listOfDetails = col.userdetails.ToList();

        bool found = false;
        foreach(UserDetail userdetails in listOfDetails){
            if(userdetails.username == emailInput.text){
                message.text = "Your password is " + userdetails.password;
                found = true;
            }
        }

        if(!found){
            message.text = "User not found";
        }

        message.gameObject.SetActive(true);
    }
}

[System.Serializable]
public class UserDetails
{
    public UserDetail[] userdetails;
}

[System.Serializable]
public class UserDetail 
{
    public string name;
    public string username;
    public string password;
}

