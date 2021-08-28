using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using NaughtyAttributes;

//public enum BuildingCategoryTriggerType
//{
//	ListenerChair,
//	PresenterChair
//}

[CreateAssetMenu(fileName = "New Building Category", menuName = "BuildingObjects/Create Category")]
public class BuildingObjectCategory : ScriptableObject
{
    [MinValue(1), MaxValue(98)]
	int layerOrder = 0;
	[SerializeField] 
	bool isCollidable = false;
	[SerializeField] 
	bool isTrigger = false;
	[EnableIf("isTrigger")]
	// BuildingCategoryTriggerType triggerType;

	GameObject tilemapObject;

	public GameObject TilemapObject
	{
		get 
		{
			if (tilemapObject == null)
			{
				tilemapObject = CreateTilemap();
			}
			return tilemapObject;
		}
	}

	public Tilemap Tilemap
	{
		get
		{
			if (tilemapObject == null)
			{
				tilemapObject = CreateTilemap();
			}
			return tilemapObject.GetComponent<Tilemap>();
		}
	}

	GameObject CreateTilemap()
	{
		// Create new tilemap game object
		GameObject obj = new GameObject("Tilemap_" + name);
		obj.AddComponent<Tilemap>();
		TilemapRenderer tr = obj.AddComponent<TilemapRenderer>();

		// Attributes
		tr.sortingOrder = layerOrder;

		if (isCollidable)
		{
			obj.AddComponent<TilemapCollider2D>();
		}
		else if (isTrigger)
		{
			TilemapCollider2D collider = obj.AddComponent<TilemapCollider2D>();
			collider.isTrigger = true;
		}

		return obj;
	}
}
