using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "New TilemapConstructor", menuName = "BuildingObjects/Create Tilemap Constructor")]
public class TilemapConstructor : ScriptableObject
{
    [MinValue(1), MaxValue(98)]
	int layerOrder = 0;
	[SerializeField] 
	bool isCollidable = false;
	[SerializeField] 
	bool isTrigger = false;
	[EnableIf("isTrigger")]

	GameObject tilemapObject;

	public GameObject TilemapObject
	{
		get 
		{
			if (tilemapObject == null)
			{
				throw new System.Exception("Constructor not yet linked to a tilemap");
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
				throw new System.Exception("Constructor not yet linked to a tilemap");
			}
			return tilemapObject.GetComponent<Tilemap>();
		}
	}

	public void CreateTilemap()
	{
		if (tilemapObject == null)
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

			tilemapObject = obj;
		}
	}
}
