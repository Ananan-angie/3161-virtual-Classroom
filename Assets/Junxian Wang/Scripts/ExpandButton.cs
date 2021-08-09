using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandButton : MonoBehaviour
{
    [SerializeField] private string directionToCollapse = "up";
    [SerializeField] private GameObject parentWindow;
    [SerializeField] private GameObject button;
    private float parentHeight, parentWidth, buttonHeight, buttonWidth;
    private GameObject controlledContent;

    // Start is called before the first frame update
    void Start()
    {
        parentHeight = parentWindow.GetComponent<RectTransform>().rect.height;
        parentWidth = parentWindow.GetComponent<RectTransform>().rect.width;
        buttonHeight = button.gameObject.GetComponent<RectTransform>().rect.height;
        buttonWidth = button.gameObject.GetComponent<RectTransform>().rect.width;
        controlledContent = parentWindow.transform.Find("Window Container").gameObject;
    }

    public void ToggleExpansion()
	{
        bool isActive = !controlledContent.activeSelf;
        controlledContent.SetActive(isActive);
        float deltaX = 0, deltaY = 0;

        if (directionToCollapse == "up")
		{
            deltaX = 0;
            deltaY = isActive ? -parentHeight + buttonHeight : parentHeight - buttonHeight;
		}
        else if (directionToCollapse == "down")
		{
            deltaX = 0;
            deltaY = isActive ? parentHeight - buttonHeight : -parentHeight + buttonHeight;
        }
        else if (directionToCollapse == "left")
        {
            deltaX = isActive ? parentWidth - buttonWidth : -parentWidth + buttonWidth;
            deltaY = 0;
        }
        else if (directionToCollapse == "right")
        {
            deltaX = isActive ? -parentWidth + buttonWidth : parentWidth - buttonWidth;
            deltaY = 0;
        }

        button.gameObject.transform.position = new Vector3(button.gameObject.transform.position.x + deltaX, button.gameObject.transform.position.y + deltaY);

    }
}
