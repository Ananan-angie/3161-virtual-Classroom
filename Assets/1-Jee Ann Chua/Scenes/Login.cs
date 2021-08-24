using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public GameObject invalidMessage;

    void Start()
    {
        //Subscribe to onClick event
        loginButton.onClick.AddListener(adminDetails);
    }

    Dictionary<string, string> loginDetails = new Dictionary<string, string>
    {
        {"abc123", "abc123"},
        {"qwert123", "qwert"},
    };


    public void adminDetails()
    {
        //Get Username from Input
        string userName = userNameField.text;
        //Get Password from Input 
        string password = passwordField.text;

        string foundPassword;

        //Look for key-pair value
        if (loginDetails.TryGetValue(userName, out foundPassword) && (foundPassword == password))
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            invalidMessage.SetActive(true);
        }

    }
    
}
