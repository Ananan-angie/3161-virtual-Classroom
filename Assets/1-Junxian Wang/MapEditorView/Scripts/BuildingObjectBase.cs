using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New Building Object", menuName = "BuildingObjects/Create Buildable")]
public class BuildingObjectBase : ScriptableObject
{
	[SerializeField] BuildingObjectCategory category;
	[SerializeField] Tile tile;

	public Tile Tile
	{
		get { return tile; }
	}

	public BuildingObjectCategory Category
	{
		get { return category; }
	}
}
