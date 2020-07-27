using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    private int lastLevel = 0;
    public InputField usernameInput;
    public GameObject usernameUI;

    //Starts Game from GameManager
    //lastLevel should be set to last level played at the start of the game (can be saved with player prefabs)
    public void StartGame() {
        GameManagerScript.instance.username = usernameInput.text;
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

    //Open username input
    public void OpenUsername()
    {
        usernameUI.SetActive(true);
    }

    //Close username input 
    public void CloseUsername()
    {
        usernameUI.SetActive(false);
    }
}
