using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableOnClickingElsewhere : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
	bool isMouseOver = false;

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(gameObject);
	}

	void IDeselectHandler.OnDeselect(BaseEventData eventData)
	{
		if (!isMouseOver) gameObject.SetActive(false);
	}

	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		isMouseOver = true;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		isMouseOver = false;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
