using UnityEngine;

public class ListenerViewInputController : InputController
{
	[SerializeField] SharedEvents sharedEvents;

	protected override void Awake()
	{
		base.Awake();
		Controls.UI.Back.performed += ctx => sharedEvents.BackToLastScene();
	}
}
