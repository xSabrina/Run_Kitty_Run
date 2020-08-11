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
    public AudioSource audioSource;
    public AudioClip clickSound;

    private AudioSource levelEndSound;

    private AudioSource mainMusic;


    void Start() {
        levelEndSound =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        mainMusic =  GameObject.Find("GameManager").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
                if(levelEndSound != null){
                    GameObject.Find("GameManager").GetComponent<AudioSource>().Pause();
                    levelEndSound.Play();
                }
            }
            levelTimeText.text = GameManagerScript.instance.timer;
            GameManagerScript.instance.abilitiesEnabled = false;
            }

    }

    public void endLevel()
    {
        audioSource.PlayOneShot(clickSound);
        GameManagerScript.instance.EndLevel();
    }
}
