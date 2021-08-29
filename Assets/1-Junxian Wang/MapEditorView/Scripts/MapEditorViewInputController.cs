using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorViewInputController : MonoBehaviour
{
    PlayerControl control;

	private void Awake()
	{
        control = new PlayerControl();
		control.UI.Back.performed += ctx => SharedEvents.Instance.BackToLastScene();
	}
}
