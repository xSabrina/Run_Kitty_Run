using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    private int lastLevel = 0;
    
    //Starts Game from GameManager
    //lastLevel should be set to last level played at the start of the game (can be saved with player prefabs)
    public void StartGame() {
        GameManagerScript.instance.SetCurrentLevel(lastLevel);
        GameManagerScript.instance.StartLevel();
    }

    //Start Tutorial
    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    //Quit Game
    public void QuitGame() {
        Application.Quit();
    }

}
