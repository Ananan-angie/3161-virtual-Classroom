using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedEvents : MonoBehaviour
{
	private void Start()
	{
        if (FindObjectOfType<DataPersistentSystem>() == null)
		{
            throw new MissingReferenceException("Error: SharedEvents require DataPersistentSystem to be present in scene.");
		}
	}

	public void TransitToScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
        DataPersistentSystem.SharedInstance.LastScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void BackToLastScene()
	{
        SceneManager.LoadScene(DataPersistentSystem.SharedInstance.LastScene);
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
