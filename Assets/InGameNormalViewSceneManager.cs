using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomInfo
{
	public int roomID;
	public int playerCount;
}

public class InGameNormalViewSceneManager : MonoBehaviour
{
	[SerializeField] GameObject roomTilemapParent;
	public RoomInfo[] rooms;

}
