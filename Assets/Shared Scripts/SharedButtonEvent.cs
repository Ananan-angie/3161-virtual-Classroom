using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedButtonEvent : MonoBehaviour
{

    public void ChangeToScene(string sceneNo)
    {
        Application.LoadLevel(sceneNo);
    }

    public void OpenCloseMap(GameObject map)
    {
        if (map.activeSelf)
        {
            map.SetActive(false);
        }
        else {
            map.SetActive(true);
        }
    }

    public void PrintButtonName(GameObject button)
    {
        print("Hello from: " + button.name);
    }
}
