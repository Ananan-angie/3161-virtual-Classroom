using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistentSystem : Singleton<DataPersistentSystem>
{
	// Stored Data
    public int LastScene;
	public Vector3 PlayerLastPos;

	private void Awake()
	{
		base.Awake(true);
	}
}
