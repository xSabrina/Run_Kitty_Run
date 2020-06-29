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

    // Use this for initialization
    void Awake()
    {
       
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


    //counts Time in minutes, second and milliseconds, should be called inj Update function
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

    //should be triggered through EndLevelPrefab
    public void EndLevel()
    {
        if (currentLevel.levelNr < levels.Count)
        {
            currentLevel = levels[currentLevel.levelNr];
            StartLevel();
        }
        else
        {

            SceneManager.LoadScene("MainMenu");
        }
        
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
 
}


