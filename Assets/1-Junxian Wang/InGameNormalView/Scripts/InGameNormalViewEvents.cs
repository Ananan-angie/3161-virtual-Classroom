using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameNormalViewEvents : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] int welcomeViewIndex;

	private void Start()
	{
		if (FindObjectOfType<DataPersistentSystem>() == null)
		{
			throw new MissingReferenceException("Error: SharedEvents require DataPersistentSystem to be present in scene.");
		}
	}

	public void TransitToSceneRecordPosition(int sceneNo)
	{
		DataPersistentSystem.SharedInstance.LastScene = SceneManager.GetActiveScene().buildIndex;
		DataPersistentSystem.SharedInstance.PlayerLastPos = player.transform.position;
		SceneManager.LoadScene(sceneNo);
	}

	public void ExitScene()
	{
		DataPersistentSystem.SharedInstance.PlayerLastPos = Vector3.zero;
		SceneManager.LoadScene(welcomeViewIndex);
	}
}
