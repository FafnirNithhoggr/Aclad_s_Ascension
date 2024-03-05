using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int levelNumber;
    public bool isLastLevel;
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = levelNumber.ToString();

    }

    public void ClickLevel()
    {
        // Load the game scene
        if (levelNumber < 4) { // THIS IS JUST TEMPORARY, REMOVE THIS WHEN WE HAVE MORE LEVELS
            SceneManager.LoadScene("Level" + levelNumber.ToString());
        } else {
            SceneManager.LoadScene("Level3");
        }
    }
}
