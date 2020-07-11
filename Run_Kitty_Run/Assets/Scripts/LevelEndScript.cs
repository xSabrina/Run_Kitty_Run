using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
            //Time.timeScale = 0;
            //levelEndScreen.SetActive(true);
            //levelTimeText.text = GameManagerScript.instance.timer;
            //GameManagerScript.instance.abilitiesEnabled = false;
            endLevel();
        }

    }

    public void endLevel()
    {
        GameManagerScript.instance.EndLevel();
    }
}
