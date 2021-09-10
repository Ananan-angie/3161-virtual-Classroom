using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class LoginEvent : MonoBehaviour
{

    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public GameObject invalidMessage;
    public TextAsset jsonFile;

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
            }
        }
        if( !found )
        {
            invalidMessage.SetActive(true);
        }
    }


    
}
