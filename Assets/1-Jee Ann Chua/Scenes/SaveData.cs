using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using TMPro;

public class SaveData : MonoBehaviour
{
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField userNameField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] List<UserDetail> saveListData = new List<UserDetail>();
    [SerializeField] TextAsset jsonFile;
    [SerializeField] GameObject successMessage;

    public void storeData(){
        // Read existing json file linked in inspector
        UserDetails col = JsonUtility.FromJson<UserDetails>(jsonFile.text);

        // Convert to list so we can add new entries
        List<UserDetail> listOfDetails = col.userdetails.ToList();
        
        // Get input and add to list
        UserDetail _UserDetail = new UserDetail(){        
            name = nameField.text,
            username = userNameField.text,
            password = passwordField.text};
        listOfDetails.Add(_UserDetail);

        // Revert back to array
        col.userdetails = listOfDetails.ToArray();

        // This will be the new json
        string newJsonString = JsonUtility.ToJson(col);

        // Write into json file and display message
        System.IO.File.WriteAllText(Application.dataPath + "/1-Jee Ann Chua/Data/UserDetail.json", newJsonString);
        successMessage.SetActive(true);
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
