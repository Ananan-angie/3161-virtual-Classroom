using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardSceneEvent : MonoBehaviour
{
    PlayerControl controls;

	private void Awake()
	{
        controls = new PlayerControl();
        controls.UI.Back.performed += ctx => backHandler();
	}

    // Handles UI "back" action
	void backHandler()
    {
        SceneManager.LoadScene(DataPersistentSystem.SharedInstance.LastScene);
    }

	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}
}
