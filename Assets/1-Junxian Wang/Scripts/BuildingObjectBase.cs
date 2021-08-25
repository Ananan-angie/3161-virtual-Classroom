using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New Building Object", menuName = "BuildingObjects/Create Buildable")]
public class BuildingObjectBase : ScriptableObject
{
	[SerializeField] BuildingObjectCategory category;
	[SerializeField] TileBase tileBase;

	public TileBase TileBase
	{
		get { return tileBase; }
	}

	public BuildingObjectCategory Category
	{
		get { return category; }
	}
}
