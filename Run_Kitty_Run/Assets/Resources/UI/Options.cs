﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Threading;

public class Options : MonoBehaviour
{
    private int volumeSoundCounter = 10;
    public GameObject optionsMenu;
    public Button fullscreenButton;
    public Button lowButton;
    public Button midButton;
    public Button highButton;
    public Sprite buttonActive;
    public Sprite buttonInactive;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioClip clickSound;

    //Open Menu
    public void OpenMenu()
    {
        //Show menu & play opening sound
        optionsMenu.SetActive(true);
        audioSource.PlayOneShot(clickSound);

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

        //Show selected fullscreen option
        if (Screen.fullScreen)
        {
            fullscreenButton.GetComponent<Image>().sprite = buttonActive;
        } else
        {
            fullscreenButton.GetComponent<Image>().sprite = buttonInactive;
        }

        //Show selected volume
        float audioVolume;
        audioMixer.GetFloat("volume", out audioVolume);
        audioSlider.value = audioVolume;
    }

    //Close Menu
    public void CloseMenu()
    {
        StartCoroutine(ClickClose());
    }

    //Close menu but with click sound
    IEnumerator ClickClose()
    {
        audioSource.PlayOneShot(clickSound);
        Time.timeScale = 1; //has to be activated shortly to play the sound
        yield return new WaitForSeconds(0.3f);
        //Dont set time scale 0 in main menu (otherwise first level will start paused)
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            Time.timeScale = 0;
        }
        //Close options menu
        optionsMenu.SetActive(false);
    }

    //Set volume 
    public void SetVolume(float volume)
    {
        //Play a sound to give auditive feedback that volume is changed within certain steps
        if (volumeSoundCounter > 20)
        {
            audioSource.PlayOneShot(clickSound);
            volumeSoundCounter = 0;
        }
        volumeSoundCounter++;
        audioMixer.SetFloat("volume", volume);
    }

    //Set graphics quality low
    public void SetQualityLow()
    {
        audioSource.PlayOneShot(clickSound);
        QualitySettings.SetQualityLevel(0);
        lowButton.GetComponent<Image>().sprite = buttonActive;
        midButton.GetComponent<Image>().sprite = buttonInactive;
        highButton.GetComponent<Image>().sprite = buttonInactive;
    }

    //Set graphics quality middle
    public void SetQualityMid()
    {
        audioSource.PlayOneShot(clickSound);
        QualitySettings.SetQualityLevel(1);
        midButton.GetComponent<Image>().sprite = buttonActive;
        lowButton.GetComponent<Image>().sprite = buttonInactive;
        highButton.GetComponent<Image>().sprite = buttonInactive;
    }

    //Set graphics quality high
    public void SetQualityHigh()
    {
        audioSource.PlayOneShot(clickSound);
        QualitySettings.SetQualityLevel(2);
        highButton.GetComponent<Image>().sprite = buttonActive;
        midButton.GetComponent<Image>().sprite = buttonInactive;
        lowButton.GetComponent<Image>().sprite = buttonInactive;
    }

    //Set fullscreen
    public void ToggleFullscreen()
    {
        audioSource.PlayOneShot(clickSound);
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
