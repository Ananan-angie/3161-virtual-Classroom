using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeViewUIManager : MonoBehaviour
{
    [SerializeField] Button enterButton;
    [SerializeField] Button exitButton;

	private void Awake()
	{
		enterButton.onClick.AddListener(() => SharedUtilities.TransitToScene(Scene.InGameNormal));
		exitButton.onClick.AddListener(() => SharedUtilities.BackToLastScene());
	}
}
