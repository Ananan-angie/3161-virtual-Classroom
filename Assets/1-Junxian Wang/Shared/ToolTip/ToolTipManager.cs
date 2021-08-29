using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ToolTipManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] GameObject tooltip;
    [SerializeField] float textWrapLength = 400;
    RectTransform tooltipRT;
	Vector2 positionDelta = new Vector2(20, -10);

	private void Start()
	{
        tooltipRT = tooltip.GetComponent<RectTransform>();
		Hide();
    }

	public void Show(string header, string body) 
	{
		// Dynamically turn on header
        if (string.IsNullOrEmpty(header))
		{
            headerText.gameObject.SetActive(false);
		}
        else
		{
            headerText.gameObject.SetActive(true);
            headerText.text = header;
		}
        bodyText.text = body;

		// Update (Enforcing) width using a Layout Element
		tooltip.GetComponent<LayoutElement>().enabled = (headerText.rectTransform.rect.width > textWrapLength || bodyText.rectTransform.rect.width > textWrapLength);

		// Update position and pivot
		Vector2 mousePos = Mouse.current.position.ReadValue();

		float pivotX = (mousePos.x + tooltipRT.rect.width > Screen.width) ? 1 : 0;
		float pivotY = (mousePos.y - tooltipRT.rect.height < 0) ? 0 : 1;
		tooltipRT.pivot = new Vector2(pivotX, pivotY);

		tooltip.transform.position = mousePos + positionDelta;

		// Turn it on
		tooltip.SetActive(true);

		// Fade in animation
		CanvasGroup cg = tooltip.GetComponent<CanvasGroup>();
		cg.alpha = 0;
		LeanTween.alphaCanvas(cg, 1, 0.2f);
	}

    public void Hide()
	{
        tooltip.SetActive(false);
	}
}
