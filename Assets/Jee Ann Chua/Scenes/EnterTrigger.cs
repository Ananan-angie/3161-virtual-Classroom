using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTrigger : MonoBehaviour
{
    public int Scene;
    private bool hasPlayer;
 
    private void Update() {
        if (hasPlayer && Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene(Scene);
            DataPersistentSystem.SharedInstance.lastScene = SceneManager.GetActiveScene().buildIndex;
        }
    }
 
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            hasPlayer = true;
        }
    }
 
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            hasPlayer = false;
        }
    }
}
