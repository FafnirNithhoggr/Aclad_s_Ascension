using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void ClickPlay()
    {
        // Load the game scene
        SceneManager.LoadScene("LevelSelection");
    }

    public void ClickExit()
    {
        // Quit the game
        Application.Quit();
    }
}
