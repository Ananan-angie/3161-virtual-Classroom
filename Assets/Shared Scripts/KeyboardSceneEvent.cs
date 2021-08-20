using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardSceneEvent : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        /* Exit Scene */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(DataPersistentSystem.SharedInstance.LastScene);
        }
    }
}
