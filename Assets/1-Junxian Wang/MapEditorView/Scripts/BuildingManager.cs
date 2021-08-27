using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] List<BuildingObjectBase> buildings;
	[SerializeField] List<BuildingObjectCategory> categories;

	public List<BuildingObjectBase> Buildings
	{
		get { return buildings; }
	}

	public List<BuildingObjectCategory> Categories
	{
		get { return categories; }
	}
}
