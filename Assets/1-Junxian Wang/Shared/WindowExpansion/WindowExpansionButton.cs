using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public class WindowExpansionButton : MonoBehaviour
{
    [SerializeField] string directionToCollapse = "up";
    [SerializeField] RectTransform windowToControl;
    [SerializeField] bool isTranslating = true;
    RectTransform parentWindow;
    Vector2 windowSize;
    TextMeshProUGUI buttonText;
    EventSystem eventSystem;

	private void Start()
	{
        // Get components
        GetComponent<Button>().onClick.AddListener(ToggleExpansion);
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        parentWindow = gameObject.transform.parent.GetComponent<RectTransform>();
        eventSystem = FindObjectOfType<EventSystem>();

        // Set up inital text
        SetText(!windowToControl.gameObject.activeSelf);

        // Get dimensions
        Canvas.ForceUpdateCanvases();
        windowSize = windowToControl.sizeDelta;
    }
    
	public void ToggleExpansion()
	{
        // Deselect from UI component
        eventSystem.SetSelectedGameObject(null);

        // Set window activation
        bool isCollapsing = windowToControl.gameObject.activeSelf;
        windowToControl.gameObject.SetActive(!isCollapsing);

        // Set button translation and text
        SetText(isCollapsing);
        if (isTranslating) SetTranslation(isCollapsing);
    }

    public void SetText(bool isCollapsing)
	{
        if (directionToCollapse == "up")
        {
            buttonText.text = isCollapsing ? "˅" : "˄";
        }
        else if (directionToCollapse == "down")
        {
            buttonText.text = isCollapsing ? "˄"  : "˅";
        }
        else if (directionToCollapse == "left")
        {
            buttonText.text = isCollapsing ? "˃" : "˂";
        }
        else if (directionToCollapse == "right")
        {
            buttonText.text = isCollapsing ? "˂" : "˃";
        }
    }

    public void SetTranslation(bool isCollapsing)
	{
		Vector2 translation = new Vector2();
        Vector2 sizeChange = new Vector2();

		if (directionToCollapse == "up")
        {
            translation.y = isCollapsing ? windowSize.y : -windowSize.y;
            sizeChange =  -translation;

        }
        else if (directionToCollapse == "down")
        {
            translation.y = isCollapsing ? -windowSize.y : windowSize.y;
            sizeChange = translation;
        }
        else if (directionToCollapse == "left")
        {
            translation.x = isCollapsing ? -windowSize.x : windowSize.x;
            sizeChange = translation;
        }
        else if (directionToCollapse == "right")
        {
            translation.x = isCollapsing ? windowSize.x : -windowSize.x;
            sizeChange = -translation;
        }

        parentWindow.sizeDelta += sizeChange;
        parentWindow.position += (Vector3)translation / 2;
    }
}
