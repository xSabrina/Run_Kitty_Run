using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices.ComTypes;

public class GameUI : MonoBehaviour {

    private const int MainMenuScene = 0;
    private int LevelNumber = 0;
    public Text TimeText;
    public Text Level;
    public GameObject InGameMenu;
    public GameObject OptionsMenu;
    public AudioSource audioSource;
    public AudioClip clickSound;
    PlayerInputActions inputAction;

    //Start a level
    private void Start() 
    {
        StartLevel();
    }

    void Awake() 
    {
        //Set ESC key for toggling the menu
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Menu.performed += ctx => ToggleMenu();
    }

    void Update() 
    {
        //Update the level time
        UpdateTimer();
    }

    //Start next level
    private void StartLevel() 
    {
        GameManagerScript.instance.abilitiesEnabled = true; //enable the players abilities
        //Set the level text
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            Level.text = "Tutorial";
        } else if (GameManagerScript.instance.currentLevel.levelName == "Level_6")
        {
            Level.text = "Final!";
        } else 
        {
            LevelNumber = GameManagerScript.instance.currentLevel.levelNr;
            Level.text = "Level " + LevelNumber.ToString();
        }
    }

    //Toggle menu
    public void ToggleMenu() 
    {
        audioSource.PlayOneShot(clickSound);
        //Enable the "hierarchical" toggling - toggling refers to options menu if open, if not to ingame menu
        if (InGameMenu.activeInHierarchy) { 
            if (OptionsMenu.activeInHierarchy) {
                OptionsMenu.SetActive(false);
            }
            else
            {
                InGameMenu.SetActive(false);
                Time.timeScale = 1;
                GameManagerScript.instance.abilitiesEnabled = true;
            }
        } else {
            InGameMenu.SetActive(true);
            Time.timeScale = 0;
            GameManagerScript.instance.abilitiesEnabled = false;
        }
    }

    //Close Menu
    public void CloseMenu()
    {
        audioSource.PlayOneShot(clickSound);
        InGameMenu.SetActive(false);
        Time.timeScale = 1;
        GameManagerScript.instance.abilitiesEnabled = true;
    }

    //Back to main menu
    public void GoHome()
    {
        StartCoroutine(ClickHome());
    }

    //Go home but with click sound first
    IEnumerator ClickHome() 
    {
        audioSource.PlayOneShot(clickSound);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        GameManagerScript.instance.abilitiesEnabled = true;
        SceneManager.LoadScene(sceneBuildIndex: MainMenuScene);
    }

    //Update the timer
    private void UpdateTimer() 
    {
        TimeText.text = GameManagerScript.instance.timer;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

}
