using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedEvents : Singleton<SharedEvents>
{

	public void TransitToScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
        DataPersistentSystem.Instance.LastScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void BackToLastScene()
	{
        SceneManager.LoadScene(DataPersistentSystem.Instance.LastScene);
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
