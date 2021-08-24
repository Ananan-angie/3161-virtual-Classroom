using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class GetText : MonoBehaviour
{
    public GameObject textObject;

    // Start is called before the first frame update
    void Start()
    {
        string readFromFilePath = Application.dataPath + "/1-Jee Ann Chua/Scenes/test.txt";
        string[] fileLines = File.ReadAllLines(readFromFilePath);
        
        textObject.GetComponent<UnityEngine.UI.Text>().text = string.Join("\n", fileLines);

    }
}