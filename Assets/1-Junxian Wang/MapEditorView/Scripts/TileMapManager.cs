using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] Transform gridTransform;
    public List<Tilemap> Tilemaps;

    // Start is called before the first frame update
    void Start()
    {
        CreateTileMaps();
    }

    void CreateTileMaps()
	{
        foreach (BuildingObjectCategory category in buildingManager.Categories)
		{
            if (gridTransform.Find(category.name) == null)
			{
                // Create new tilemap game object
                GameObject obj = new GameObject("Tilemap_" + category.name);
                Tilemap map = obj.AddComponent<Tilemap>();
                TilemapRenderer tr = obj.AddComponent<TilemapRenderer>();

                // Set parent
                obj.transform.SetParent(gridTransform);

                // Attributes
                tr.sortingOrder = category.LayerOrder;

                // Attach to category
                category.Tilemap = map;

                // Store in class
                Tilemaps.Add(map);
			}
            else
			{
                Debug.Log($"Tilemap {category.name} already exists, initalizer cannot create it and thus it may have different attributes than intended, category also cannot refernece it.");
			}
		}
	}
}
