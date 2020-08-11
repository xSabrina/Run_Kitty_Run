using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public GameObject textBox;

    //displays tutorial text when player enters trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            textBox.SetActive(true);
        }
    }

    //hides tutorial text when player enters trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            textBox.SetActive(false);
        }
    }
}
