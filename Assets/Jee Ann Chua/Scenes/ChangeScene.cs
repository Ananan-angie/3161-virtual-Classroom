using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    
    public void ChangetoScene(string scene)
    {
        Application.LoadLevel(scene);
    }


}