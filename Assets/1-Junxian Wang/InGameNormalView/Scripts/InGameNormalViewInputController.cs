using UnityEngine;

public class InGameNormalViewInputController : InputController
{
	[SerializeField] PlayerController playerController;
	[SerializeField] InGameNormalViewEvents sceneEvents;
	[SerializeField] ChatManager chatManager;

	protected override void Awake()
	{
		base.Awake();
		Controls.Gameplay.Move.performed += ctx => playerController.MovementThisFrame = ctx.ReadValue<Vector2>();
		Controls.Gameplay.Move.canceled += ctx => playerController.MovementThisFrame = Vector2.zero;
		Controls.Gameplay.Interact.performed += ctx => playerController.OnInteract();
		Controls.UI.Back.performed += ctx => sceneEvents.ExitScene();
		Controls.UI.Chating.performed += ctx => chatManager.Chat();
	}
}
