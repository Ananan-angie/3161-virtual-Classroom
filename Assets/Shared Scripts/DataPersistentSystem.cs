using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistentSystem : Singleton<DataPersistentSystem>
{
	// Scene Transition
    public int LastScene;

	// InGameNormalView scene transition
	public Vector3 PlayerLastPos;
	

	protected override void Awake()
	{
		DontDestroy = true;
		base.Awake();
	}
}
