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
    //Game objects
    public Text TimeText;
    public Text Level;
    public GameObject InGameMenu;
    public Image MenuResumeButton;
    public Image MenuOptionsButton;
    public Image MenuCancelButton;
    PlayerInputActions inputAction;
    

    private void Start() {
        StartLevel();
        ToggleMenu();
    }

    void Awake() {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Menu.performed += ctx => ToggleMenu();
        inputAction.PlayerControls.Blink.performed += ctx => Debug.Log(ctx);
    }

    void Update() {
        UpdateTimer();
    }

    //Start next level
    private void StartLevel() {
        LevelNumber = GameManagerScript.instance.currentLevel.levelNr;
        Level.text = "Level " + LevelNumber.ToString();
    }

    //Toggle menu
    public void ToggleMenu() {
        Debug.Log("MENU TOGGLE");
        if (InGameMenu.activeSelf) {
            Debug.Log("Closing Menu...");
            InGameMenu.SetActive(false);
            Time.timeScale = 1;
            GameManagerScript.instance.abilitiesEnabled = true;
            Debug.Log("Menu closed.");
        } else {
            Debug.Log("Opening Menu...");
            InGameMenu.SetActive(true);
            Time.timeScale = 0;
            GameManagerScript.instance.abilitiesEnabled = false;
            Debug.Log("Menu opened.");
        }
    }

    //Close Menu
    public void CloseMenu()
    {
        InGameMenu.SetActive(false);
        Time.timeScale = 1;
        GameManagerScript.instance.abilitiesEnabled = true;
    }

    //Back to main menu
    public void GoHome() {
        SceneManager.LoadScene(sceneBuildIndex: MainMenuScene);
    }

    //Update the timer
    private void UpdateTimer() {
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
