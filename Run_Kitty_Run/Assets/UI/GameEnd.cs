using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{

    public Text timeText;
    public Text highscoresNames;
    public Text highscoresScores;
    public GameObject highscoresUI;
    private string highscoresTextNames;
    private string highscoresTextScores;
    private int completeTime;
    private int completeMinutes = 0;
    private int completeSeconds = 0;

    void Awake()
    {
        ShowTime();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Show complete time
    public void ShowTime()
    {
        //Calculate complete time
        for (int i = 0; i < GameManagerScript.instance.levels.Count; i++)
        {
            completeTime += GameManagerScript.instance.levels[i].levelTime;
        }
        completeMinutes = completeTime / 60;
        completeSeconds = completeTime % 60;
        //Show complete time
        timeText.text = completeMinutes.ToString("00") + ":" + completeSeconds.ToString("00.00");
    }

    //Update highscores
    public void UpdateHighscores()
    {
        //Add Highscore
        StartCoroutine(GameManagerScript.instance.highscoresScript.UploadHighscore(GameManagerScript.instance.username, completeTime));
        //Show Highscores
        GameManagerScript.instance.GetHighscores();
        StartCoroutine(ShowHighscores());
    }

    //Open Highscores
    public void OpenHighscores()
    {
        UpdateHighscores();
        highscoresUI.SetActive(true);
    }

    //Show highscores
    IEnumerator ShowHighscores()
    {
        //Wait for highscores Coroutine
        while (GameManagerScript.instance.highscoreWaiter)
        {
            yield return new WaitForSeconds(0.1f);
        }

        highscoresTextNames = "NAME\n";
        highscoresTextScores = "SCORE\n";
        for (int i = GameManagerScript.instance.highscores.Length - 1; i >= 0; i--)
        {
            //Limit to TOP 5
            if ((GameManagerScript.instance.highscores.Length - i) < 6 ) {
                highscoresTextNames += GameManagerScript.instance.highscores[i].userName + "\n";
                highscoresTextScores += GameManagerScript.instance.highscores[i].score + "\n";
            }
        }
        highscoresNames.text = highscoresTextNames;
        highscoresScores.text = highscoresTextScores;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
