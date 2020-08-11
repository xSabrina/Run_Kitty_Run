using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    private int lastLevel = 0;
    private AudioSource[] allAudioSources;
    private AudioSource menuMusic;
    public InputField usernameInput;
    public GameObject usernameUI;
    public AudioSource audioSource;
    public AudioClip clickSound;
    public Level tutorialLevel;

    public void Awake()
    {
        //since MainMenu.cs sits on two gameObjects, do this only for the higher hierarchy one
        if (gameObject.name == "UI")
        {
            //stop all currently playing music
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
            //start menu music
            menuMusic = gameObject.GetComponent<AudioSource>();
            menuMusic.loop = true;
            menuMusic.Play();
        }
    }
    //Starts the game, respectively the first level
    public void StartGame() 
    {
        audioSource.PlayOneShot(clickSound);
        GameManagerScript.instance.username = usernameInput.text; //set username
        GameManagerScript.instance.SetCurrentLevel(lastLevel); //start level
        GameManagerScript.instance.StartLevel();
    }

    //Start Tutorial
    public void StartTutorial()
    {
        audioSource.PlayOneShot(clickSound);
        GameManagerScript.instance.currentLevel = tutorialLevel;
        SceneManager.LoadScene("TutorialLevel");
    }

    //Quit Game
    public void QuitGame() 
    {
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
