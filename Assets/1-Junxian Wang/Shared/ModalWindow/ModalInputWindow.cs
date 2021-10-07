using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class ModalInputWindow : MonoBehaviour
{
    public string QuestionText;
    public string PlaceholderText;

    public Button EnterButton;
    public Button CancelButton;
    public TextMeshProUGUI PlaceholderTMPro;
    public TextMeshProUGUI QuestionTMPro;
    public TMP_InputField InputField;
    public TextMeshProUGUI ErrorMessage;

    /*
     * OnEnter is responsible for handling the input, as well as 
     * disabling the model window on finish, and DisplayErrorMessage() on invalid input 
     */
    public Action<string> OnEnter;

	private void Start()
	{
        EnterButton.onClick.AddListener(
        () => {
            OnEnter(InputField.text);
            InputField.text = "";
        });
        CancelButton.onClick.AddListener(() => gameObject.SetActive(false));

        QuestionTMPro.text = QuestionText;
        PlaceholderTMPro.text = PlaceholderText;

#if UNITY_EDITOR
        InvokeRepeating("UpdateInspectorText", 0, 0.1f);
#endif
    }

    private void UpdateInspectorText()
	{
        QuestionTMPro.text = QuestionText;
        PlaceholderTMPro.text = PlaceholderText;
    }


    public void DisplayErrorMessage(string error)
	{
        ErrorMessage.text = error;
        ErrorMessage.gameObject.SetActive(true);
        LeanTween.delayedCall(ErrorMessage.gameObject, 2f, () => ErrorMessage.gameObject.SetActive(false));
	}
}
