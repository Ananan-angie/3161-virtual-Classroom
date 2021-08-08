using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    
    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public Button loginButton;

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
        //Get Username from Input then convert it to int
        string userName = userNameField.text;
        //Get Password from Input 
        string password = passwordField.text;

        Debug.Log(userName);
        Debug.Log(password);
        string foundPassword;
        if (loginDetails.TryGetValue(userName, out foundPassword) && (foundPassword == password))
        {
            Application.LoadLevel("AppStartView");
        }
        else
        {
            Debug.Log("Invalid username/password");
        }
    }
    
}
