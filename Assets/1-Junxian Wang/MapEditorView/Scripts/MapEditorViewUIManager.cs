using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using SFB;

public class MapEditorViewUIManager : MonoBehaviour
{
    [SerializeField] MapEditorManager mapEditorManager;

    [SerializeField] Dropdown categoryDropdown;
    [SerializeField] Button tools_newButton;
    [SerializeField] Button tools_saveButton;
    [SerializeField] Button tools_loadButton;
    [SerializeField] Button tools_brushButton;
    [SerializeField] Button tools_lineButton;
    [SerializeField] Button tools_boxButton;
    [SerializeField] Button tools_eraserButton;
    [SerializeField] TMP_InputField input_map;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject itemContentTile;
    [SerializeField] GameObject itemContentRoom;
    [SerializeField] Transform itemContainerRoom;

    [SerializeField] GameObject roomButtonPrefab;
    [SerializeField] Button addRoomButton;

    List<string> dropDownOptionsText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        /* ========= Item Selector ========= */

        // Category Dropdown Menu
        UpdateCategoryDropdown();
        categoryDropdown.onValueChanged.AddListener(ctx => CategoryDropdownOnValueChanged(ctx));
        

		// Add the tile button to the corresponding category
		foreach (Tile tile in mapEditorManager.TileAssets)
		{
			GameObject tileButton = new GameObject(tile.name);
			tileButton.AddComponent<RectTransform>();
			tileButton.AddComponent<Button>();
			tileButton.GetComponent<Button>().onClick.AddListener(delegate { mapEditorManager.SelectBuilder(tile); });
            
            Image i = tileButton.AddComponent<Image>();
			tileButton.GetComponent<Image>().sprite = tile.sprite;

            tileButton.transform.SetParent(itemContentTile.transform, false);
		}

		/* ========= Tools Menu ========= */
        tools_newButton.onClick.AddListener(() => mapEditorManager.NewMap());
        tools_saveButton.onClick.AddListener(() =>
        {
            string[] paths = StandaloneFileBrowser.OpenFolderPanel("Save Map to Folder", TilemapSaveSystem.DefaultSavePath, false);
            mapEditorManager.SaveMap(paths[0]);
        });
        tools_loadButton.onClick.AddListener(() => 
        {
            string[] paths = StandaloneFileBrowser.OpenFolderPanel("Open Map from Folder", TilemapSaveSystem.DefaultSavePath, false);
            mapEditorManager.LoadMap(paths[0]);
        });
        tools_brushButton.onClick.AddListener(() => mapEditorManager.SelectPaintMode(PaintMode.Brush));
        tools_lineButton.onClick.AddListener(() => mapEditorManager.SelectPaintMode(PaintMode.Line));
        tools_boxButton.onClick.AddListener(() => mapEditorManager.SelectPaintMode(PaintMode.Box));
        tools_eraserButton.onClick.AddListener(() => mapEditorManager.SelectEraser());
        addRoomButton.onClick.AddListener(() => mapEditorManager.CreateNewRoom());
    }

    public void UpdateCategoryDropdown()
	{
        categoryDropdown.ClearOptions();
        dropDownOptionsText = new List<string>();

        foreach (ClassroomTilemap c in mapEditorManager.ClassroomTilemaps)
        {
            if (!c.IsRoom)
			{
                dropDownOptionsText.Add(c.name);
            }
        }
        
        dropDownOptionsText.Add("Room");

        categoryDropdown.AddOptions(dropDownOptionsText);

        mapEditorManager.SelectLayer(categoryDropdown.value);
    }

    public void UpdateDropdownRooms()
	{
        foreach (Transform t in itemContainerRoom)
		{
            Destroy(t.gameObject);
		}

        foreach (ClassroomTilemap c in mapEditorManager.ClassroomTilemaps)
        {
            if (c.IsRoom)
            {
                GameObject roomButton = Instantiate(roomButtonPrefab);
                roomButton.GetComponentInChildren<Text>().text = c.name;
                roomButton.transform.SetParent(itemContainerRoom);
                roomButton.GetComponent<Button>().onClick.AddListener(delegate { mapEditorManager.SelectRoom(c); });
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(itemContentRoom.GetComponent<RectTransform>());
    }

    private void CategoryDropdownOnValueChanged(int choice)
	{
        if (dropDownOptionsText[choice] == "Room")
		{
            itemContentTile.SetActive(false);
            itemContentRoom.SetActive(true);
            scrollRect.content = itemContentRoom.GetComponent<RectTransform>();
		}
        else
		{
            itemContentTile.SetActive(true);
            itemContentRoom.SetActive(false);
            scrollRect.content = itemContentTile.GetComponent<RectTransform>();
            mapEditorManager.SelectLayer(choice);
            try
            {
                mapEditorManager.VisibleRoomLayer.TilemapObject.SetActive(false);
            }
            catch (System.Exception) { }
        }
	}

    public void SetMapText(string mapName)
	{
        input_map.text = mapName;
	}
}
