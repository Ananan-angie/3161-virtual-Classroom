using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameNormalViewEvents : MonoBehaviour
{
	[SerializeField] GameObject player;

	public void TransitToSceneRecordPosition(Scene scene)
	{
		DataPersistentSystem.Instance.PlayerLastPos = player.transform.position;
		SharedUtilities.TransitToScene(scene);
	}

	public void ExitScene()
	{
		DataPersistentSystem.Instance.PlayerLastPos = Vector3.zero;
		SharedUtilities.TransitToScene(Scene.Welcome);
	}
}
