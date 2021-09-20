using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class LoginEvent : MonoBehaviour
{

    [SerializeField] TMP_InputField userNameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button loginButton;
    [SerializeField] GameObject invalidMessage;
    [SerializeField] TextAsset jsonFile;

    void Start()
    {
        //Subscribe to onClick event
        loginButton.onClick.AddListener(adminDetails);
    }


    public void adminDetails()
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
                SceneManager.LoadScene(7);
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


    
}
