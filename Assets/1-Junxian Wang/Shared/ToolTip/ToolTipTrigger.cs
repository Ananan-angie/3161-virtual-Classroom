using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] 
	string header;
	
	[SerializeField]
	[ResizableTextArea()]
	string body;

	[SerializeField]
	[ReadOnly()]
	float showDelay = 1;

	ToolTipManager manager;
	private static LTDescr delayedCall;

	private void Start()
	{
		manager = FindObjectOfType<ToolTipManager>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		delayedCall = LeanTween.delayedCall(showDelay, () => 
		{
			manager.Show(header, body);
		});
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		LeanTween.cancel(delayedCall.uniqueId);
		manager.Hide();
	}

}
