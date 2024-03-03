using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame() {
        Debug.Log("Exit Game");
        // Load LevelSelection Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }
}
