using UnityEngine;

public class ListenerViewInputController : MonoBehaviour
{
	[SerializeField] SharedEvents sharedEvents;
	PlayerControl controls;

	private void Awake()
	{
		// Setup player control
		controls = new PlayerControl();
		controls.UI.Back.performed += ctx => sharedEvents.BackToLastScene();
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
