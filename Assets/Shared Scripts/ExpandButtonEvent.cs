using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandButtonEvent : MonoBehaviour
{
    [SerializeField] private string directionToCollapse = "up";
    [SerializeField] private GameObject controlledWindow;
    private float windowHeight, windowWidth, buttonHeight, buttonWidth;

    // Start is called before the first frame update
    void Start()
    {
        windowHeight = controlledWindow.GetComponent<RectTransform>().rect.height;
        windowWidth = controlledWindow.GetComponent<RectTransform>().rect.width;
        buttonHeight = gameObject.GetComponent<RectTransform>().rect.height;
        buttonWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }


    public void ToggleExpansion()
	{

        bool isActive = !controlledWindow.activeSelf;
        controlledWindow.SetActive(isActive);
        float deltaX = 0, deltaY = 0;

        if (directionToCollapse == "up")
		{
            deltaX = 0;
            deltaY = isActive ? -windowHeight + buttonHeight : windowHeight - buttonHeight;
		}
        else if (directionToCollapse == "down")
		{
            deltaX = 0;
            deltaY = isActive ? windowHeight - buttonHeight : -windowHeight + buttonHeight;
        }
        else if (directionToCollapse == "left")
        {
            deltaX = isActive ? windowWidth - buttonWidth : -windowWidth + buttonWidth;
            deltaY = 0;
        }
        else if (directionToCollapse == "right")
        {
            deltaX = isActive ? -windowWidth + buttonWidth : windowWidth - buttonWidth;
            deltaY = 0;
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + deltaX, gameObject.transform.position.y + deltaY);

    }
}
