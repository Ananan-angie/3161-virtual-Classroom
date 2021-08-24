using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonHandler : MonoBehaviour
{
	[SerializeField] BuildingObjectBase buildable;
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
		Debug.Log($"Button clicked: {buildable.name}");
		creator.SelectBuilder(buildable);
	}
}
