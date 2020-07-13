﻿using System.Collections;
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
    PlayerInputActions inputAction;
    

    private void Start() {
        StartLevel();
        Debug.Log(GameManagerScript.instance.username);
    }

    void Awake() {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Menu.performed += ctx => ToggleMenu();
    }

    void Update() {
        UpdateTimer();
    }

    //Start next level
    private void StartLevel() {
        Time.timeScale = 1;
        GameManagerScript.instance.abilitiesEnabled = true;
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            Level.text = "Tutorial";
        } else
        {
            LevelNumber = GameManagerScript.instance.currentLevel.levelNr;
            Level.text = "Level " + LevelNumber.ToString();
        }
    }

    //Toggle menu
    public void ToggleMenu() {
        if (InGameMenu.activeSelf) {
            InGameMenu.SetActive(false);
            Time.timeScale = 1;
            GameManagerScript.instance.abilitiesEnabled = true;
        } else {
            InGameMenu.SetActive(true);
            Time.timeScale = 0;
            GameManagerScript.instance.abilitiesEnabled = false;
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
