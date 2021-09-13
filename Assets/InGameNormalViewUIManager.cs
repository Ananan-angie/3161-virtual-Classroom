using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameNormalViewUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI roomText;

    // Update is called once per frame
    void Update()
    {
        timeText.text = $"Time: {System.DateTime.Now.ToShortTimeString()}";
    }

    public void ChangeRoomText(int room)
	{
        roomText.text = $"Room: {room}";
        if (room < 0)
        {
            roomText.text = $"Room: None";
        }
    }
}
