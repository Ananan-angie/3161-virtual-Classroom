using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistentSystem : MonoBehaviour
{
	public static DataPersistentSystem SharedInstance;
    public int lastScene;

	private void Awake()
	{
		if (SharedInstance == null)
		{
			SharedInstance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}
}
