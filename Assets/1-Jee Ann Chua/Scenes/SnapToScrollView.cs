using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapToScrollView : MonoBehaviour
{
    [SerializeField] public ScrollRect scrollRect;
    [SerializeField] public RectTransform contentPanel;

    public void SnapTo(RectTransform target)
    {
        Vector2 position = target.anchoredPosition;
        Vector2 offset = new Vector2(target.anchoredPosition.x, -85);
        Debug.Log(offset);
        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position) + offset; 
    }
}
