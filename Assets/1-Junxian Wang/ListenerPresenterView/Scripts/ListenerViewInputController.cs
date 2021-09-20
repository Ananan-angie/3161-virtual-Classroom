using UnityEngine;

public class ListenerViewInputController : InputController
{
	[SerializeField] ChatManager chatManager;

	protected override void Awake()
	{
		base.Awake();
		Controls.UI.Back.performed += ctx => SharedUtilities.Instance.BackToLastScene();
		Controls.UI.Chating.performed += ctx => chatManager.SendChat();
	}
}
