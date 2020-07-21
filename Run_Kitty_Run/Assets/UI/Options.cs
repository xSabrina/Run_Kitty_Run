using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public GameObject optionsMenu;
    public Button fullscreenButton;
    public Button lowButton;
    public Button midButton;
    public Button highButton;
    public Sprite buttonActive;
    public Sprite buttonInactive;
    public AudioMixer audioMixer;

    //Open Menu
    public void OpenMenu()
    {
        optionsMenu.SetActive(true);

        //Show selected quality
        if (QualitySettings.GetQualityLevel() == 0)
        {
            lowButton.GetComponent<Image>().sprite = buttonActive;
        } else if (QualitySettings.GetQualityLevel() == 1)
        {
            midButton.GetComponent<Image>().sprite = buttonActive;
        } else if (QualitySettings.GetQualityLevel() == 2)
        {
            highButton.GetComponent<Image>().sprite = buttonActive;
        }

        //Show selected fullscreen
        if (Screen.fullScreen)
        {
            fullscreenButton.GetComponent<Image>().sprite = buttonActive;
        } else
        {
            fullscreenButton.GetComponent<Image>().sprite = buttonInactive;
        }
    }

    //Close Menu
    public void CloseMenu()
    {
        optionsMenu.SetActive(false);
    }

    //Set volume 
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Set graphics quality
    public void SetQualityLow()
    {
        QualitySettings.SetQualityLevel(0);
        lowButton.GetComponent<Image>().sprite = buttonActive;
        midButton.GetComponent<Image>().sprite = buttonInactive;
        highButton.GetComponent<Image>().sprite = buttonInactive;
    }

    public void SetQualityMid()
    {
        QualitySettings.SetQualityLevel(1);
        midButton.GetComponent<Image>().sprite = buttonActive;
        lowButton.GetComponent<Image>().sprite = buttonInactive;
        highButton.GetComponent<Image>().sprite = buttonInactive;
    }

    public void SetQualityHigh()
    {
        QualitySettings.SetQualityLevel(2);
        highButton.GetComponent<Image>().sprite = buttonActive;
        midButton.GetComponent<Image>().sprite = buttonInactive;
        lowButton.GetComponent<Image>().sprite = buttonInactive;
    }

    //Set fullscreen
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false;
            fullscreenButton.GetComponent<Image>().sprite = buttonInactive;
        } else
        {
            Screen.fullScreen = true;
            fullscreenButton.GetComponent<Image>().sprite = buttonActive;
        }
    }
}
