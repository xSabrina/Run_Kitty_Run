using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;
using UnityEngine.InputSystem;

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
        ToggleMenu();
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
        LevelNumber = SceneManager.GetActiveScene().buildIndex;
        Level.text = "Level " + LevelNumber.ToString();
    }

    //Open menu
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
