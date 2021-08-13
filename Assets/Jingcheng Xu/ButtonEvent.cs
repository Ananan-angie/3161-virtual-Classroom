using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameObject map;

    public void ChangeToScene(int sceneNo)
    {
        Application.LoadLevel(sceneNo);
    }

    public void OpenCloseMap()
    {
        if (map.active)
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
