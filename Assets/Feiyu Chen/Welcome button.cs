using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcomebutton : MonoBehaviour

    public void Enter()
    {
        SceneManagement.LoadScene(ScenceManager.GetActiveScene().buildIndex + 1);
    }
     public void Exit()
    {
        Application.Exit();
    }
        
}
