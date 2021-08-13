using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorViewButtonEvent : MonoBehaviour
{
	[SerializeField] GameObject easySetupWindow, advancedSetupWindow;

	public void EasySetupButtonEvent()
	{
		easySetupWindow.SetActive(!easySetupWindow.activeSelf);
		advancedSetupWindow.SetActive(false);
	}

	public void AdvancedSetupButtonEvent()
	{
		advancedSetupWindow.SetActive(!advancedSetupWindow.activeSelf);
		easySetupWindow.SetActive(false);
	}
}
