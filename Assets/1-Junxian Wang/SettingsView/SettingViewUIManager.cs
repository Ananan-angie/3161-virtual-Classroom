using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingViewUIManager : MonoBehaviour
{
    [SerializeField] Button backButton;

	private void Awake()
	{
		backButton.onClick.AddListener(() => SharedUtilities.BackToLastScene());
	}
}
