using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginViewUIManager : MonoBehaviour
{
    [SerializeField] Button settings;

	private void Awake()
	{
		settings.onClick.AddListener(() => SharedUtilities.TransitToScene(Scene.Setting));
	}
}
