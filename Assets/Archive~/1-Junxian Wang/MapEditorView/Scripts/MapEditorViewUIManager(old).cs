using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapEditorViewUIManager : MonoBehaviour
{
    [SerializeField] BuildingCreator buildingCreator;

    [SerializeField] Dropdown categoryDropdown;
    [SerializeField] Button tools_brushButton;
    [SerializeField] Button tools_lineButton;
    [SerializeField] Button tools_boxButton;

    [SerializeField] GameObject tileViewport;

    Dictionary<string, GameObject> tileContainers = new Dictionary<string, GameObject>();
    GameObject activeTileContainer;
    List<string> dropDownOptionsText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        /* ========= Item Selector ========= */
        // Category Dropdown Menu
        foreach (BuildingObjectCategory category in buildingCreator.Categories)
		{
            dropDownOptionsText.Add(category.name);
        }
        categoryDropdown.AddOptions(dropDownOptionsText);
        categoryDropdown.onValueChanged.AddListener(ctx => categoryDropdownCallback(ctx));

        // Add a tiles container for each category
        foreach (BuildingObjectCategory category in buildingCreator.Categories)
		{
            GameObject tileContainer = new GameObject(category.name);
            tileContainer.AddComponent<RectTransform>();
            tileContainer.AddComponent<GridLayoutGroup>();
            tileContainer.GetComponent<RectTransform>().sizeDelta = tileViewport.GetComponent<RectTransform>().sizeDelta;
            tileContainer.transform.SetParent(tileViewport.transform, false);
            tileContainer.SetActive(false);
            tileContainers.Add(category.name, tileContainer);
		}

        // Add the buildable button to the corresponding category
        foreach (BuildingObjectBase building in buildingCreator.Buildings)
		{
            GameObject tileButton = new GameObject(building.name);
            tileButton.AddComponent<RectTransform>();
            tileButton.AddComponent<Button>();
            tileButton.GetComponent<Button>().onClick.AddListener(delegate { buildingCreator.SelectBuilder(building); });
            tileButton.AddComponent<Image>();
            tileButton.GetComponent<Image>().sprite = building.Tile.sprite;

            GameObject tileContainer = tileContainers[building.Category.name];
            tileButton.transform.SetParent(tileContainer.transform, false);
		}

        /* ========= Tools Menu ========= */
        tools_brushButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Brush); } );
        tools_lineButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Line); });
        tools_boxButton.onClick.AddListener(delegate { buildingCreator.SelectPaintMode(PaintMode.Box); });
    }

    void categoryDropdownCallback(int index)
	{
        setActiveTileContainer(dropDownOptionsText[index]);
    }

    void setActiveTileContainer(string categoryName)
	{
        if (activeTileContainer != null)
		{
            activeTileContainer.SetActive(false);
        }
        GameObject currentContainer = tileContainers[categoryName];
        currentContainer.SetActive(true);
        activeTileContainer = currentContainer;
    }
}
