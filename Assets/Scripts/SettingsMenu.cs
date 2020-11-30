﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public Resolution[] resolutions;
    public Dropdown resolution;
    private Scene currentScene;
    public AudioMixer masterAudio;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;

        resolutions = Screen.resolutions;
        resolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);


            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            resolution.AddOptions(options);
            resolution.value = currentResolutionIndex;
            resolution.RefreshShownValue();
        }
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution res = resolutions[_resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Quality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void ChangeMusicVolume(float volume)
    {
        masterAudio.SetFloat("MusicVolume", volume);

    }
    public void ChangeSFXVolume(float volume)
    {
        masterAudio.SetFloat("SFXVolume", volume);
    }

    public void ToggleMute(bool isMuted)
    {
        if (isMuted)
        {
            masterAudio.SetFloat("MusicVolume", -80);
            masterAudio.SetFloat("SFXVolume", -80);
        }
        else
        {
            masterAudio.SetFloat("MusicVolume", 0);
            masterAudio.SetFloat("SFXVolume", 0);
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        settingsMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    private void Update()
    {
        string sceneName = currentScene.name;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeInHierarchy == false)
            {
                Time.timeScale = 1;
                settingsMenu.SetActive(false);
            }
            if (sceneName == "GameScene")
            {
                Time.timeScale = 0;
                settingsMenu.SetActive(true);
            }
            else
            {
                return;
            }
        }
    }

}
