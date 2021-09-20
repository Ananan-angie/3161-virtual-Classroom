using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SharedUtilities : Singleton<SharedUtilities>
{
	protected override void Awake()
	{
        base.Awake();	
	}

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
        Debug.Log("Hello from: " + button.name);
    }

    public IEnumerator UpdateTime(string text)
	{
        while (true)
		{
            text = $"Time: {System.DateTime.Now.ToShortTimeString()}";
            yield return new WaitForSeconds(2);
        }
    }
}
