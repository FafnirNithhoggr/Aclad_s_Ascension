using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelArrowBack : MonoBehaviour
{
    public void ClickArrowBack()
    {
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
