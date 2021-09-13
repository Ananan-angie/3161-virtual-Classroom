using UnityEngine;

public class ListenerViewInputController : InputController
{
	[SerializeField] SharedEvents sharedEvents;
	[SerializeField] ChatManager chatManager;

	protected override void Awake()
	{
		base.Awake();
		Controls.UI.Back.performed += ctx => sharedEvents.BackToLastScene();
		Controls.UI.Chating.performed += ctx => chatManager.Chat();
	}
}
