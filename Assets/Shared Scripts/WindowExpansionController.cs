using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowExpansionController : MonoBehaviour
{
    [SerializeField] string directionToCollapse = "up";
    float windowHeight, windowWidth;

    // Start is called before the first frame update
    void Awake()
    {
        windowHeight = gameObject.GetComponent<RectTransform>().rect.height;
        windowWidth = gameObject.GetComponent<RectTransform>().rect.width;
    }


    public void ToggleExpansion(GameObject button)
	{
        float buttonHeight = button.GetComponent<RectTransform>().rect.height;
        float buttonWidth = button.GetComponent<RectTransform>().rect.width;

        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
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

        button.transform.position = new Vector3(button.transform.position.x + deltaX, button.transform.position.y + deltaY);
    }
}
