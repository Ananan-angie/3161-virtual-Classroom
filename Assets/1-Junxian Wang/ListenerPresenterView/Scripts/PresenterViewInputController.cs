using UnityEngine;

public class PresenterViewInputController : InputController
{
	[SerializeField] ChatManager chatManager;

	protected override void Awake()
	{
		base.Awake();
		Controls.UI.Back.performed += ctx => SharedUtilities.BackToLastScene();
		Controls.UI.Chating.performed += ctx => chatManager.SendChat();
	}
}
