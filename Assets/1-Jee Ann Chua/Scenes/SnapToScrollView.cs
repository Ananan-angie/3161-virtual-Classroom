using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapToScrollView : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;

    public void SnapTo(RectTransform target)
    {
        Vector2 offset = new Vector2(122,-20);
        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
            (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
            - (Vector2)scrollRect.transform.InverseTransformPoint(target.position) + offset; 
    }
}
