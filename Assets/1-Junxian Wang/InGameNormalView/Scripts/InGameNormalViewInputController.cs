using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameNormalViewInputController : MonoBehaviour
{
	[SerializeField] PlayerController playerController;
	[SerializeField] InGameNormalViewEvents sceneEvents;
	PlayerControl controls;

	private void Awake()
	{
		// Setup player control
		controls = new PlayerControl();
		controls.Gameplay.Move.performed += ctx => playerController.MovementThisFrame = ctx.ReadValue<Vector2>();
		controls.Gameplay.Move.canceled += ctx => playerController.MovementThisFrame = Vector2.zero;
		controls.Gameplay.Interact.performed += ctx => playerController.OnInteract();
		controls.UI.Back.performed += ctx => sceneEvents.ExitScene();
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
