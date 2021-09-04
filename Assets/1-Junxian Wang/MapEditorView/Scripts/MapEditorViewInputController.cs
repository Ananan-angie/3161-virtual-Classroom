using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorViewInputController : InputController
{
	protected override void Awake()
	{
		base.Awake();
		Controls.UI.Back.performed += ctx => SharedEvents.Instance.BackToLastScene();
	}
}
