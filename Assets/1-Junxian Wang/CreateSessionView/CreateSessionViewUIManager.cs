using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SFB;
using System.Diagnostics;

public class CreateSessionViewUIManager : MonoBehaviour
{
    [SerializeField] Button createSessionButton;
	[SerializeField] Button backButton;
	[SerializeField] Button sessionMapEditor;
	[SerializeField] Button importMap;
	[SerializeField] Button welcomeScreenEditor;
	public TextMeshProUGUI selectedMapName;

	private void Awake()
	{
		backButton.onClick.AddListener(() => SharedUtilities.BackToLastScene());
		sessionMapEditor.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.MapEditor));
		importMap.onClick.AddListener(OnImportMap);
		welcomeScreenEditor.onClick.AddListener(openEditor);
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

	private void OnImportMap()
	{
		string[] paths = StandaloneFileBrowser.OpenFolderPanel("Open Map from Folder", TilemapSaveSystem.DefaultSavePath, false);
		selectedMapName.text = Path.GetFileName(paths[0]);
	}
	
	private void openEditor()
	{
		var fileToOpen = Application.dataPath + "/1-Jee Ann Chua/Data/SessionDetails.txt";
		var process = new Process();
		process.StartInfo = new ProcessStartInfo()
		{
			UseShellExecute = true,
			FileName = fileToOpen
		};

		process.Start();
		process.WaitForExit();
	}
}
