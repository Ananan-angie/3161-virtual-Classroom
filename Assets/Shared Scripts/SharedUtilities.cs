using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Scene
{
    Login,
    CreateSession,
    Setting,
    Welcome,
    MapEditor,
    Presenter,
    Listener,
    AppStart,
    InGameNormal
}


public static class SharedUtilities
{

	public static void TransitToScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
        DataPersistentSystem.Instance.LastScene = SceneManager.GetActiveScene().buildIndex;
    }

    public static void BackToLastScene()
	{
        SceneManager.LoadScene(DataPersistentSystem.Instance.LastScene);
    }

    public static void OpenCloseMap(GameObject map)
    {
        if (map.activeSelf)
        {
            map.SetActive(false);
        }
        else {
            map.SetActive(true);
        }
    }

    public static void PrintButtonName(GameObject button)
    {
        Debug.Log("Hello from: " + button.name);
    }

    public static IEnumerator UpdateTime(string text)
	{
        while (true)
		{
            text = $"Time: {System.DateTime.Now.ToShortTimeString()}";
            yield return new WaitForSeconds(2);
        }
    }
}
