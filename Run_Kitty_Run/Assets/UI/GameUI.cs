using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;

public class GameUI : MonoBehaviour {
    private const int MainMenuScene = 0;
    private int LevelNumber = 0;
    bool Playing = true;
    float CurTime = 0f;
    //Game objects
    public Text TimeText;
    public Text Level;
    /*public Image Ability1;
    public Image Ability2;
    public Image Ability3;
    public Image[] Abilities;
    public Sprite Ab1;
    public Sprite Ab2;
    public Sprite Ab3;
    public Sprite Ab1Dis;
    public Sprite Ab2Dis;
    public Sprite Ab3Dis;
    public Sprite[] Sprites;*/

    private void Start() {
        /*Abilities = [Ability1, Ability2, Ability3];
        Sprites = [Ab1, Ab2, Ab3, Ab1Dis, Ab2Dis, Ab3Dis];*/
        StartLevel();
    }

    void Update() {
        UpdateTimer();
    }

    //Start next level
    private void StartLevel() {
        CurTime = 0f;
        LevelNumber = SceneManager.GetActiveScene().buildIndex;
        Level.text = "Level " + LevelNumber.ToString();
        StartCooldown(1, 5000);
    }

    //Open menu
    public void OpenMenu() {
        
    }

    //Back to main menu
    public void GoHome() {
        Debug.Log("MENU!");
        SceneManager.LoadScene(sceneBuildIndex: MainMenuScene);
    }

    //Update the timer
    private void UpdateTimer() {
        if (Playing) {
            CurTime += Time.deltaTime;
            if (CurTime < 9) {
                TimeText.text = "00,0" + CurTime.ToString("#.00");
            } else {
                TimeText.text = "00," + CurTime.ToString("#.00");
            }
        }
    }

    //Make cooldown for actions visible
    public void StartCooldown(int ability, int duration) {
        Debug.Log("COOLDOWN START");
        /*Abilities[ability].sprite = Sprites[ability];
        var timer = new System.Timers.Timer();
        timer.Interval = duration;
        timer.Elapsed += OnStopCooldown;
        timer.AutoReset = false;
        timer.Enabled = true;*/
    }

    //Stop an existing cooldown
    public void OnStopCooldown(System.Timers.ElapsedEventHandler e) {
        Debug.Log("COOLDOWN STOP");
        /*Abilities[ability].sprite = Sprites[ability + 3];*/
    }

}
