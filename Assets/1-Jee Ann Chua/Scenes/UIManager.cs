using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Transform[] time;

    // schedule
    string[,] arr2d = new string[13,8]{
        {"","","","","","","",""},
        {"","","Class 2","","","","",""},
        {"","Class 1","","","Class 6","","",""},
        {"","Class 1","","Class 4","","Class 7","",""},
        {"","","","Class 5","","","",""},
        {"","","","","","","",""},
        {"","","","","","","",""},
        {"","","","","","","",""},
        {"","","Class 3","","","","",""},
        {"","","","","","","",""},
        {"","","","","","Class 8","",""},
        {"","","","","","","",""},
        {"","","","","","","",""},
    };

    public TMP_Text UpcomingClass;


    // Start is called before the first frame update
    void Start()
    {
        UpdateSchedule();
    }


    void UpdateSchedule(){
        for (int t = 0; t < 13; t++){
            for (int d = 1; d < 8; d++){
                GameObject obj = time[t].GetChild(d).gameObject;
                obj.GetComponentInChildren<TMP_Text>().text = arr2d[t,d];
            }
        }
    }

   
}
