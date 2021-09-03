using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class WindowExpansionButton : MonoBehaviour
{
    [SerializeField] string directionToCollapse = "up";
    [SerializeField] RectTransform windowToControl;
    float windowHeight, windowWidth, buttonHeight, buttonWidth;
    TextMeshProUGUI buttonText;
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Awake()
    {
        // Get dimensions
        windowHeight = windowToControl.rect.height;
        windowWidth = windowToControl.rect.width;
        buttonHeight = GetComponent<RectTransform>().rect.height;
        buttonWidth = GetComponent<RectTransform>().rect.width;

        // Get components
        GetComponent<Button>().onClick.AddListener(ToggleExpansion);
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        eventSystem = FindObjectOfType<EventSystem>();

        // Set up inital text
        SetText(windowToControl.gameObject.activeSelf);
    }

    public void ToggleExpansion()
	{
        // Deselect from UI component
        eventSystem.SetSelectedGameObject(null);

        // Set window activation
        bool isActivating = !windowToControl.gameObject.activeSelf;
        windowToControl.gameObject.SetActive(isActivating);

        // Set button translation and text
        SetText(isActivating);
        SetTranslation(isActivating);
    }

    public void SetText(bool isActivating)
	{
        if (directionToCollapse == "up")
        {
            buttonText.text = isActivating ? "˄" : "˅";
        }
        else if (directionToCollapse == "down")
        {
            buttonText.text = isActivating ? "˅" : "˄";
        }
        else if (directionToCollapse == "left")
        {
            buttonText.text = isActivating ? "˂" : "˃";
        }
        else if (directionToCollapse == "right")
        {
            buttonText.text = isActivating ? "˃" : "˂";
        }
    }

    public void SetTranslation(bool isActivating)
	{
        Vector3 translation = new Vector3();

        if (directionToCollapse == "up")
        {
            translation.x = 0;
            translation.y = isActivating ? -windowHeight + buttonHeight : windowHeight - buttonHeight;
        }
        else if (directionToCollapse == "down")
        {
            translation.x = 0;
            translation.y = isActivating ? windowHeight - buttonHeight : -windowHeight + buttonHeight;
        }
        else if (directionToCollapse == "left")
        {
            translation.x = isActivating ? windowWidth - buttonWidth : -windowWidth + buttonWidth;
            translation.y = 0;
        }
        else if (directionToCollapse == "right")
        {
            translation.x = isActivating ? -windowWidth + buttonWidth : windowWidth - buttonWidth;
            translation.y = 0;
        }

        transform.position = transform.position + translation;
    }
}
