using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TextMesh;
    public string front;
    public string end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerFormat(front, end);
    }

    public void TimerFormat(string front, string end)
    {
        TextMesh.text = front + System.DateTime.Now.ToShortTimeString() + end;
    }    
}
