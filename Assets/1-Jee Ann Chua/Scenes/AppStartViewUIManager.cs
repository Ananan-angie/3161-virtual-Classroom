using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class AppStartViewUIManager : MonoBehaviour
{

    [SerializeField] Transform[] time;
    [SerializeField] TextMeshProUGUI userInfo;
    [SerializeField] Button newSessionButton;
    [SerializeField] Button joinSessionButton;
    [SerializeField] Button mapEditorButton;
    [SerializeField] Button settingsButton;

    [SerializeField] ModalInputWindow sessionIDInput;

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
    string className;
    int scheduleDay;
    int scheduleTime;

    private void Awake()
    {
        userInfo.text = $"User Name: {ClassroomNetworkManager.Instance.clientID}";
        newSessionButton.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.CreateSession));
        joinSessionButton.onClick.AddListener(() => sessionIDInput.gameObject.SetActive(true));
        mapEditorButton.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.MapEditor));
        settingsButton.onClick.AddListener(() => SharedUtilities.TransitToScene(GameScene.Setting));
        sessionIDInput.OnEnter = async (text) => {
            ClassroomNetworkManager.Instance.roomID = int.Parse(text);
            bool roomJoined = await ClassroomNetworkManager.Instance.JoinRoomBlocking();
            if (roomJoined)
            {
                sessionIDInput.gameObject.SetActive(false);
                SharedUtilities.TransitToScene(GameScene.Welcome);
            }
            else sessionIDInput.DisplayErrorMessage("Room not joined: Invalid input.");
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSchedule();
    }

    void Update(){
        UpdateUpcomingClass();
    }

    void UpdateSchedule(){
        for (int t = 0; t < 13; t++){
            for (int d = 1; d < 8; d++){
                GameObject obj = time[t].GetChild(d).gameObject;
                obj.GetComponentInChildren<TMP_Text>().text = arr2d[t,d];
            }
        }
    }

    void UpdateUpcomingClass(){
        DateTime current = DateTime.Now;
        int day = (int) current.DayOfWeek;
        int hour = current.Hour + 1;

        // Sunday == 0, convert sunday to 7
        if (day == 0){
            day = 7;
        }

        // if current hour is later than 8pm or earlier than 8am, set to 8am the next day
        if (hour > 20 || hour < 8){
            hour = 8;
            day += 1;
        }

        // if exceeds Sunday, set it back to Monday
        if (day > 7){
            day = 1;
        }

        bool found = false;

        // finds the upcoming class of current time this week
        for (int d = 1; d < 8; d++){
            for (int t = 0; t < 13; t++){
                if(t + 8 >= hour && d >= day && arr2d[t,d] != ""){
                    className = arr2d[t,d];
                    scheduleDay = d;
                    scheduleTime = t+8;
                    found = true;
                    goto End;
                }
            }
        }
        
        // if no upcoming class this week, finds the first class of the week
        if(!found){
            for (int d = 1; d < 8; d++){
                for (int t = 0; t < 13; t++){
                    if(arr2d[t,d] != ""){
                        className = arr2d[t,d];
                        scheduleDay = d;
                        scheduleTime = t+8;
                        found = true;
                        goto End;
                    }
                }
            }
        }
    End:
        if(found){
            if(scheduleTime < 12){
                UpcomingClass.text = "Day: " + Enum.GetName(typeof(DayOfWeek),scheduleDay) + "\n Time: " + scheduleTime + "AM" + "\n Class Name: " + className + "\n Class Number : 01" + "\n Tutor: ";
            }
            else if(scheduleTime == 12){
                UpcomingClass.text = "Day: " + Enum.GetName(typeof(DayOfWeek),scheduleDay) + "\n Time: " + scheduleTime + "PM" + "\n Class Name: " + className + "\n Class Number : 01" + "\n Tutor: "; 
            }
            else{
                scheduleTime -= 12;
                UpcomingClass.text = "Day: " + Enum.GetName(typeof(DayOfWeek),scheduleDay) + "\n Time: " + scheduleTime + "PM" + "\n Class Name: " + className + "\n Class Number : 01" + "\n Tutor: "; 
            }
            
        }
        else{
            UpcomingClass.text = "No upcoming session.";
        }
    }
}
