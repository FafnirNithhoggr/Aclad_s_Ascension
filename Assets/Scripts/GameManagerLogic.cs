using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLogic : MonoBehaviour
{
    public int currentLevel;
    public bool isLastLevel;
    public void ExitGame() {
        Debug.Log("Exit Game");
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
}
