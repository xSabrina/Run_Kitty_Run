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
    public List<Level> Levels = new List<Level>();
    public Level currentLevel;

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
        currentLevel = Levels[currentLevel.levelNr];
        StartLevel();
        minutes = 0;
        seconds = 0;
    }

    //for restarting Level or loading anew one

    public void StartLevel()
    {
        SceneManager.LoadScene(currentLevel.levelName);
    }
 
}


