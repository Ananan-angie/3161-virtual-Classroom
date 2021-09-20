using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[AddComponentMenu("UI/Blur Panel")]
public class BlurPanel : Image
{
	public bool animate;
	public float time = 0.5f;
	public float delay = 0f;
	CanvasGroup canvas_;

	protected override void Awake()
	{
		base.Awake();
		canvas_ = GetComponent<CanvasGroup>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		color = Color.black * 0.1f;
		if (Application.isPlaying)
		{
			material.SetFloat("_Size", 1);
			canvas_.alpha = 1;
			LeanTween.value(gameObject, UpdateBlur, 0, 1, time).setDelay(delay);
		}
	}

	void UpdateBlur(float value)
	{
		material.SetFloat("_Size", value);
		canvas_.alpha = value;
	}
}
