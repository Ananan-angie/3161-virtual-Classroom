using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintModeButtonHandler : MonoBehaviour
{
	public PaintMode paintMode;
	Button button;
	BuildingCreator creator;

	private void Awake()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(buttonHandler);
	}

	private void Start()
	{
		creator = FindObjectOfType<BuildingCreator>();
	}

	private void buttonHandler()
	{
		Debug.Log($"Button clicked: {paintMode}");
		creator.SelectPaintMode(paintMode);
	}
}
