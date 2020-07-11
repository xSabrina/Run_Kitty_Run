using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Timers;

public class LevelEndScript : MonoBehaviour
{
    public GameObject levelEndScreen;
    public Text levelTimeText;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "TutorialLevel")
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Time.timeScale = 0;
                levelEndScreen.SetActive(true);
                levelTimeText.text = GameManagerScript.instance.timer;
                GameManagerScript.instance.abilitiesEnabled = false;
            }
        }

    }

    public void endLevel()
    {
        GameManagerScript.instance.EndLevel();
    }
}
