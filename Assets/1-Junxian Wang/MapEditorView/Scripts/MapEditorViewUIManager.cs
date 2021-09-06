using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapEditorViewUIManager : MonoBehaviour
{
    [SerializeField] BuildingCreator buildingCreator;

    [SerializeField] Dropdown categoryDropdown;
    [SerializeField] Button tools_newButton;
    [SerializeField] Button tools_saveButton;
    [SerializeField] Button tools_loadButton;
    [SerializeField] Button tools_brushButton;
    [SerializeField] Button tools_lineButton;
    [SerializeField] Button tools_boxButton;
    [SerializeField] Button tools_eraserButton;

    [SerializeField] GameObject tileViewport;

    List<string> dropDownOptionsText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        /* ========= Item Selector ========= */

        // Category Dropdown Menu
        foreach (TilemapConstructor c in buildingCreator.TilemapConstructors)
		{
            dropDownOptionsText.Add(c.name);
        }
        categoryDropdown.AddOptions(dropDownOptionsText);
        categoryDropdown.onValueChanged.AddListener(ctx => buildingCreator.SelectCategory(ctx));
        buildingCreator.SelectCategory(categoryDropdown.value);

		// Add the tile button to the corresponding category
		foreach (Tile tile in buildingCreator.TileAssets)
		{
			GameObject tileButton = new GameObject(tile.name);
			tileButton.AddComponent<RectTransform>();
			tileButton.AddComponent<Button>();
			tileButton.GetComponent<Button>().onClick.AddListener(delegate { buildingCreator.SelectBuilder(tile); });
            
            Image i = tileButton.AddComponent<Image>();
			tileButton.GetComponent<Image>().sprite = tile.sprite;

            tileButton.transform.SetParent(tileViewport.transform, false);
		}

		/* ========= Tools Menu ========= */
        tools_newButton.onClick.AddListener(delegate { buildingCreator.ClearTilemaps(); });
        tools_saveButton.onClick.AddListener(delegate { TilemapSaveSystem.Save(buildingCreator.TilemapConstructors); });
        tools_loadButton.onClick.AddListener(delegate { TilemapSaveSystem.Load(buildingCreator.TilemapConstructors); });
        tools_brushButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Brush); });
        tools_lineButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Line); });
        tools_boxButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Box); });
        tools_eraserButton.onClick.AddListener(delegate { buildingCreator.SelectEraser(); });
    }
}
