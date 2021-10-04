using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CreateSessionViewUIManager : MonoBehaviour
{
    [SerializeField] Button createSessionButton;
	[SerializeField] Button backButton;

	private void Awake()
	{
		backButton.onClick.AddListener(() => SharedUtilities.BackToLastScene());
	}

	private void Start()
	{
		createSessionButton.onClick.AddListener(async () =>
		{
			createSessionButton.interactable = false;
			
			await ClassroomNetworkManager.Instance.CreateRoomBlocking();

			ClassroomNetworkManager.Instance.JoinRoom();

			SharedUtilities.TransitToScene(GameScene.Welcome);

			createSessionButton.interactable = true;
		});
	}
}
