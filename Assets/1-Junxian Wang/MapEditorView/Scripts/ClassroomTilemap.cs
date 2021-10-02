using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[System.Serializable]
public class ClassroomTilemap : ICloneable
{

	public string name;

	public int layerOrder = 0;

	public bool isCollidable = false;

	public bool isTrigger = false;

	public string triggerTag;

	public int roomID;

	GameObject tilemapObject;

	public bool IsRoom { get { return triggerTag == "Room"; } }

	public ClassroomTilemap(string name, int layerOrder, bool isCollidable = false, bool isTrigger = false, string triggerTag = null, int roomID = 0)
	{
		this.name = name;
		this.layerOrder = layerOrder;
		this.isCollidable = isCollidable;
		this.isTrigger = isTrigger;
		this.triggerTag = triggerTag;
		this.roomID = roomID;
	}

	public GameObject TilemapObject
	{
		get 
		{
			if (tilemapObject == null)
			{
				throw new Exception("Constructor not yet linked to a tilemap");
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
				throw new Exception("Constructor not yet linked to a tilemap");
			}
			return tilemapObject.GetComponent<Tilemap>();
		}
	}

	public void CreateTilemap(Transform parent = null, bool overwrite = false)
	{
		if (tilemapObject == null || overwrite)
		{
			// Create new tilemap game object
			GameObject obj = new GameObject("Tilemap_" + name);
			obj.AddComponent<Tilemap>();
			TilemapRenderer tr = obj.AddComponent<TilemapRenderer>();

			// Attributes
			tr.sortingOrder = layerOrder;
			if (isTrigger)
			{
				obj.tag = triggerTag;

				if (IsRoom)
				{
					obj.name = "Room " + roomID.ToString();
				}
			}

			// Components
			if (isCollidable)
			{
				obj.AddComponent<TilemapCollider2D>();
			}
			else if (isTrigger)
			{
				TilemapCollider2D collider = obj.AddComponent<TilemapCollider2D>();
				collider.isTrigger = true;
			}

			// Set parent
			if (parent)
			{
				obj.transform.SetParent(parent);
			}

			// Link newly created object to the class variable
			tilemapObject = obj;
		}
	}

	public void ResetTilemapObject()
	{
		tilemapObject = null;
	}

	public object Clone()
	{
		return MemberwiseClone();
	}
}
