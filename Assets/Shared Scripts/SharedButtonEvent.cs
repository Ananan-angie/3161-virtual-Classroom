using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedButtonEvent : MonoBehaviour
{

    public void ChangeToScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
        DataPersistentSystem.SharedInstance.lastScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void BackToLastScene()
	{
        SceneManager.LoadScene(DataPersistentSystem.SharedInstance.lastScene);
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
