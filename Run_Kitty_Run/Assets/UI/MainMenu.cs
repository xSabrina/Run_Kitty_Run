using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    private int lastLevel = 0;
    public InputField usernameInput;
    public GameObject usernameUI;
    public AudioSource audioSource;
    public AudioClip clickSound;

    //Starts Game from GameManager
    //lastLevel should be set to last level played at the start of the game (can be saved with player prefabs)
    public void StartGame() {
        audioSource.PlayOneShot(clickSound);
        GameManagerScript.instance.username = usernameInput.text;
        GameManagerScript.instance.SetCurrentLevel(lastLevel);
        GameManagerScript.instance.StartLevel();
    }

    //Start Tutorial
    public void StartTutorial()
    {
        audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("TutorialLevel");
    }

    //Quit Game
    public void QuitGame() {
        audioSource.PlayOneShot(clickSound);
        Application.Quit();
    }

    //Open username input
    public void OpenUsername()
    {
        audioSource.PlayOneShot(clickSound);
        usernameUI.SetActive(true);
    }

    //Close username input 
    public void CloseUsername()
    {
        audioSource.PlayOneShot(clickSound);
        usernameUI.SetActive(false);
    }
}
