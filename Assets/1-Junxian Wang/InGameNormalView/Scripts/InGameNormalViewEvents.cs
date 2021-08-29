using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameNormalViewEvents : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] int welcomeViewIndex;

	public void TransitToSceneRecordPosition(int sceneNo)
	{
		DataPersistentSystem.Instance.LastScene = SceneManager.GetActiveScene().buildIndex;
		DataPersistentSystem.Instance.PlayerLastPos = player.transform.position;
		SceneManager.LoadScene(sceneNo);
	}

	public void ExitScene()
	{
		DataPersistentSystem.Instance.PlayerLastPos = Vector3.zero;
		SceneManager.LoadScene(welcomeViewIndex);
	}
}
