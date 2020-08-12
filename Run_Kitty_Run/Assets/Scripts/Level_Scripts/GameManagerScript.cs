using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //String for displaying leveltimer
    public string timer;
    //Floats for counting leveltime
    private float seconds;
    private int minutes;
    public AudioClip bossMusic;
    private AudioSource mainMusic;
    //String for username used for highscore
    public string username;
    //Bool that defines if timer is running or not
    public bool countTime = true;
    public static GameManagerScript instance = null;
    //List of levelobjects
    public List<Level> levels = new List<Level>();
    //Current level the player is in used for loading the correct scene
    public Level currentLevel;
    //Variables that define which abilities are being used and if they are currently usable
    public bool abilitiesEnabled = true;
    public int selectedAbility1 = 0;
    public int selectedAbility2 = 1;
    public bool highscoreWaiter = true;
    //List of highscores
    public Highscore[] highscores;
    public Highscores highscoresScript;

    //Use this for initialization
    void Awake()
    {
        //Get main music
        mainMusic = gameObject.GetComponent<AudioSource>();
        //Determines if highscores script needs to beassigned
        if (highscoresScript == null)
        {
            gameObject.GetComponent<Highscores>();
        }
        //Do not destroy on load for gamemanager singleton
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

    void Update()
    {
        if (countTime)
        {
            CountTime();
        }
    }

    //Count time in minutes, second and milliseconds
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

    //Restart level or loading a new one
    public void StartLevel()
    {
        minutes = 0;
        seconds = 0;
        SceneManager.LoadScene(currentLevel.levelName);

        //Play background music; if the last level (level 6) has started change background music
        if (currentLevel.levelName != "Level_6")
        {
            mainMusic.Play();
        } else
        {
            mainMusic.Stop();
            mainMusic.clip = bossMusic;
            mainMusic.Play();
        }
    }

    //Start or restart the current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel.levelName);
    }

    //Set current level object through levels list
    public void SetCurrentLevel(int i)
    {
        currentLevel = levels[i];
    }

    //Finish a level, triggered through EndLevelPrefab
    public void EndLevel()
    {
        currentLevel.levelTime = minutes * 60 + (int)seconds;
        //Check if there is a next level, otherwise starts end screen
        if (currentLevel.levelNr < levels.Count)
        {
            currentLevel = levels[currentLevel.levelNr];
            StartLevel();
            Time.timeScale = 1;
        }
        else
        {
            mainMusic.Stop();
            SceneManager.LoadScene("EndScreen");
            Time.timeScale = 1;
        }
    }
   
    public void GetHighscores()
    {
        StartCoroutine(GetHighscoreCoroutine());
    }

    //Get highscores through Highscores script
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


