using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLogic : MonoBehaviour
{
    public int currentLevel;
    public bool isLastLevel;
    public GameObject spawner = null;
    public GameObject receiver = null;
    private float counter = 0.0f;
    private float timeToCheck = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null || receiver == null) {
            Debug.LogError("Spawner or Receiver not set in GameManagerLogic");
        }
        // Get the children of spawner and receiver
        for (int i = 0; i < spawner.transform.childCount; i++) {
            GameObject child = spawner.transform.GetChild(i).gameObject;
            if (child.CompareTag("SpawnerLogic")) {
                spawner = child;
                break;
            }
        }
        for (int i = 0; i < receiver.transform.childCount; i++) {
            GameObject child = receiver.transform.GetChild(i).gameObject;
            if (child.CompareTag("ReceiverLogic")) {
                receiver = child;
                break;
            }
        }
    }

    // Check once per second if the player has lost the game
    void FixedUpdate() {
        counter += Time.deltaTime;
        if (counter > timeToCheck) {
            counter = 0.0f;
            // Count the number of Aclads in the scene
            int nAcladsAlive = GameObject.FindGameObjectsWithTag("Aclad").Length;
            int nAcladsToSpawn = spawner.GetComponent<Spawner>().GetAcladsToSpawn();
            int nAcladsReceived = receiver.GetComponent<ReceiverLogic>().GetAcladsEntered();
            int nAcladsNeeded = receiver.GetComponent<ReceiverLogic>().GetAcladsNeeded();

            int nAclads = nAcladsAlive + nAcladsToSpawn + nAcladsReceived;
            if (nAclads < nAcladsNeeded) {
                // Put the game on pause
                Time.timeScale = 0;
                // Find the canvas and show the GameOverPanel
                GameObject.Find("LoseCanvas").GetComponent<Canvas>().enabled = true;
            }
        }
    }

    public void ExitGame() {
        Time.timeScale = 1;
        // Load LevelSelection Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }

    public void NextLevel() {
        if (isLastLevel) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + (currentLevel + 1).ToString());
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + currentLevel.ToString());
    }
}
