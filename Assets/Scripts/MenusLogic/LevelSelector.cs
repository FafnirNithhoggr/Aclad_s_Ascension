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
        SceneManager.LoadScene("Level" + levelNumber.ToString());
    }
}
