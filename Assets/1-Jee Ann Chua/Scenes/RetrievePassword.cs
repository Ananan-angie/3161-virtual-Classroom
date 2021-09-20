using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using TMPro;

public class RetrievePassword : MonoBehaviour
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_Text message;

    public void getPassword(){
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
