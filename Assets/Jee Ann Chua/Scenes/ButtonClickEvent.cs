using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickEvent : MonoBehaviour
{
    public GameObject map;
    
    public void ChangetoScene(string scene)
    {
        Application.LoadLevel(scene);
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


}