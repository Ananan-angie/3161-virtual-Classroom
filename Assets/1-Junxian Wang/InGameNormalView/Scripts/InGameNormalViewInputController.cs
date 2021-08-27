using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameNormalViewInputController : MonoBehaviour
{
	[SerializeField] PlayerController playerController;
	[SerializeField] InGameNormalViewEvents sceneEvents;
	public PlayerControl Controls;
	[SerializeField] ChatManager chatManager;

	private void Awake()
	{
		// Setup player control
		Controls = new PlayerControl();
		Controls.Gameplay.Move.performed += ctx => playerController.MovementThisFrame = ctx.ReadValue<Vector2>();
		Controls.Gameplay.Move.canceled += ctx => playerController.MovementThisFrame = Vector2.zero;
		Controls.Gameplay.Interact.performed += ctx => playerController.OnInteract();
		Controls.UI.Back.performed += ctx => sceneEvents.ExitScene();
		Controls.UI.Chating.performed += ctx => chatManager.Chat();
	}

	private void OnEnable()
	{
		Controls.Enable();
	}

	private void OnDisable()
	{
		Controls.Disable();
	}
}
