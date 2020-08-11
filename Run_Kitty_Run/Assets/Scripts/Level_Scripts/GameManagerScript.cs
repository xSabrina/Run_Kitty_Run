using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public string timer;
    private float seconds;
    private int minutes;
    private AudioSource mainMusic;
    public string username;
    public bool countTime = true;
    public static GameManagerScript instance = null;
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    public bool abilitiesEnabled = true;
    public int selectedAbility1 = 0;
    public int selectedAbility2 = 1;
    public bool highscoreWaiter = true;
    public Highscore[] highscores;
    public Highscores highscoresScript;

    // Use this for initialization
    void Awake()
    {
        //get main music
        mainMusic = gameObject.GetComponent<AudioSource>();
        //determine if highscores are needed
        if (highscoresScript == null)
        {
            gameObject.GetComponent<Highscores>();
        }
        //dont destroy on load
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (countTime)
        {
            CountTime();
        }

    }


    //counts Time in minutes, second and milliseconds, should be called in Update function
    private void CountTime()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minutes += 1;
            seconds -= 60f;
        }
        timer = minutes.ToString("00") + ":" + seconds.ToString("00.00");

    }


    //for restarting Level or loading a new one
    public void StartLevel()
    {
        minutes = 0;
        seconds = 0;
        SceneManager.LoadScene(currentLevel.levelName);
        if (currentLevel.levelName != "Level_6")
        {
            mainMusic.Play();
        } else
        {
            mainMusic.Stop();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel.levelName);
    }

    public void SetCurrentLevel(int i)
    {
        currentLevel = levels[i];
    }

    //should be triggered through EndLevelPrefab
    public void EndLevel()
    {
        currentLevel.levelTime = minutes * 60 + (int)seconds;
        if (currentLevel.levelNr < levels.Count)
        {
            currentLevel = levels[currentLevel.levelNr];
            StartLevel();
            Time.timeScale = 1;
        }
        else
        {
            //AddHighscore(PlayerPrefs.GetString("Username", "DefaultUser"));
            SceneManager.LoadScene("EndScreen");
            Time.timeScale = 1;
        }

    }
   
    public void GetHighscores()
    {
        StartCoroutine(GetHighscoreCoroutine());
    }

    IEnumerator GetHighscoreCoroutine()
    {
        highscoreWaiter = true;
        yield return StartCoroutine(highscoresScript.DownloadHighscores());
        highscores = highscoresScript.highscoresList;
        for (int i = highscores.Length-1; i >= 0; i--)
        {
            Debug.Log("username: " + highscores[i].userName + " score: " + highscores[i].score);
        }
        highscoreWaiter = false;
    }

}


