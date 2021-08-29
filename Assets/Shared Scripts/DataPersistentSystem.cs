using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistentSystem : Singleton<DataPersistentSystem>
{
	public static DataPersistentSystem SharedInstance;
    public int LastScene;
	public Vector3 PlayerLastPos;
}
