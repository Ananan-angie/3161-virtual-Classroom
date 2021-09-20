using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CreateSessionViewUIManager : MonoBehaviour
{
    [SerializeField] Button createSessionButton;

	private void Start()
	{
		createSessionButton.onClick.AddListener(async () =>
		{
			createSessionButton.interactable = false;
			
			await ClassroomNetworkManager.Instance.CreateRoomBlocking();

			ClassroomNetworkManager.Instance.JoinRoom();

			SharedUtilities.Instance.TransitToScene(8);

			createSessionButton.interactable = true;
		});
	}
}
