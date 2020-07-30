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
            //stop all playing music
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
        GameManagerScript.instance.currentLevel = tutorialLevel;
        SceneManager.LoadScene("TutorialLevel");
    }

    //Quit Game
    public void QuitGame() {
        StartCoroutine(ClickQuit());
    }

    //Quit game but with click sound
    IEnumerator ClickQuit()
    {
        audioSource.PlayOneShot(clickSound);
        yield return new WaitForSeconds(0.3f);
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
