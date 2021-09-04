using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
	public PlayerControl Controls;

	protected virtual void Awake()
	{
		// Setup player control
		Controls = new PlayerControl();

		// To use in other input controllers:
		// base.Awake()
		// Controls.xxx.xxx
	}

	protected void OnEnable()
	{
		Controls.Enable();
	}

	protected void OnDisable()
	{
		Controls.Disable();
	}
}
