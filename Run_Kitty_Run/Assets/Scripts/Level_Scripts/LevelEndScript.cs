using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndScript : MonoBehaviour
{
    public GameObject levelEndScreen;
    public Text levelTimeText;
    public AudioSource audioSource;
    public AudioClip clickSound;
    private AudioSource levelEndSound;

    void Start() {
        levelEndSound =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    //Show endlevel ui, sets leveltime text, and disables time and abilities when player reaches the trigger
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

                //Pause background music and play level end sound
                if(levelEndSound != null){
                    GameObject.Find("GameManager").GetComponent<AudioSource>().Pause();
                    levelEndSound.Play();
                }
                levelTimeText.text = GameManagerScript.instance.timer;
                GameManagerScript.instance.abilitiesEnabled = false;
            }
        }

    }

    public void endLevel()
    {
        audioSource.PlayOneShot(clickSound);
        GameManagerScript.instance.EndLevel();
    }

}
