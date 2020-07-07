using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public string timer;
    private float seconds;
    private int minutes;
    public bool countTime = true;
    public static GameManagerScript instance = null;
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    public bool abilitiesEnabled = true;
    public int selectedAbility1 = 0;
    public int selectedAbility2 = 1;
    public Highscores highscoresScript;

    // Use this for initialization
    void Awake()
    {
        
       if (highscoresScript == null)
        {
            gameObject.GetComponent<Highscores>();
        }
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



    //for restarting Level or loading anew one

    public void StartLevel()
    {
        minutes = 0;
        seconds = 0;
        SceneManager.LoadScene(currentLevel.levelName);
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
        }
        else
        {

            AddHighscore(PlayerPrefs.GetString("Username", "DefaultUser"));

            //SceneManager.LoadScene("MainMenu");
        }

    }
    public void AddHighscore(string username)
    {
        StartCoroutine(AddHighscoreCoroutine(username));
    }


    IEnumerator AddHighscoreCoroutine(string username)
    {
        int score = 0;
        Debug.Log("Levels: "+ levels.Count);
        foreach (Level lvl in levels)
        {
            Debug.Log("Leveltime: "+lvl.levelTime);
            score += lvl.levelTime;
            Debug.Log("score: "+score);
        }

        if (PlayerPrefs.GetInt("Score") > score|| PlayerPrefs.HasKey("Score")==false)
        {
            yield return StartCoroutine(highscoresScript.DeleteHighscore(username));
            PlayerPrefs.SetInt("Score", score);
            yield return StartCoroutine(highscoresScript.UploadHighscore(username, score));

        }
        
        GetHighscores();
        Debug.Log("Added and loaded scores");
        
    }

   
    public void GetHighscores()
    {
        StartCoroutine(GetHighscoreCoroutine());
      
    }
    IEnumerator GetHighscoreCoroutine()
    {
        yield return StartCoroutine(highscoresScript.DownloadHighscores());
        Highscore[] highscores = highscoresScript.highscoresList;
        for (int i = highscores.Length-1; i >= 0; i--)
        {
            Debug.Log("username: " + highscores[i].userName + " score: " + highscores[i].score);
        }
    }
}


