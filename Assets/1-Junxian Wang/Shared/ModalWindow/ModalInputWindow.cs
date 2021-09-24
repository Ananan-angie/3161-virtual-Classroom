using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(DisableOnClickingElsewhere))]
public class ModalInputWindow : MonoBehaviour
{
    public string QuestionText;
    public string PlaceholderText;
    public Button EnterButton;
    public Button CancelButton;
    public TextMeshProUGUI PlaceholderTMPro;
    public TextMeshProUGUI QuestionTMPro;
    public TMP_InputField InputField;

    public Action<string> onEnter;

	private void Start()
	{
        EnterButton.onClick.AddListener(() => {
            onEnter(InputField.text);
            InputField.text = "";
            gameObject.SetActive(false);
            }
        );
        CancelButton.onClick.AddListener(() => gameObject.SetActive(false));
	}

#if UNITY_EDITOR    
	private void Update()
	{
        QuestionTMPro.text = QuestionText;
        PlaceholderTMPro.text = PlaceholderText;
    }
#endif


}
