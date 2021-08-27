using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Building Category", menuName = "BuildingObjects/Create Category")]
public class BuildingObjectCategory : ScriptableObject
{
    [SerializeField] int layerOrder = 0;
	Tilemap tilemap;

    public int LayerOrder
	{
		get { return layerOrder; }
	}

	public Tilemap Tilemap
	{
		get { return tilemap; }
		set { tilemap = value; }
	}
}
