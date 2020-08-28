using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    #region public variables
    public GameObject IWantToDisableThis;

    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider SFXSlider;
    #endregion

    public void Awake()
    {
        LoadPlayerPrefs();
    }

    public void Start()
    {
        //Debug.Log("game is running");

        if(!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 0);
            Screen.fullScreen = false;
        }
        else
        {
            if(PlayerPrefs.GetInt("fullscreen") == 0)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }

        if(!PlayerPrefs.HasKey("quality"))
        {
            PlayerPrefs.SetInt("quality", 5);//dont have magic numbers
            QualitySettings.SetQualityLevel(5);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        }

        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    #region Change settings
    //this changes the screen from fullscreen to windowed
    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    #endregion
    public void QuitGame()
    {
        Debug.Log("Quitting Game");

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", value);
    }
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFXVol", value);
    }


    #region Save and load player prefs

    public void SavePlayerPrefs()
    {
        //save quality
        PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());

        //save fullscreen
        if(fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        //save audio sliders
        float musicVol;
        if (mixer.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }

        float SFXVol;
        if (mixer.GetFloat("SFXVol", out SFXVol))
        {
            PlayerPrefs.SetFloat("SFXVol", SFXVol);
        }

        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        //load quality
        qualityDropdown.value = PlayerPrefs.GetInt("quality");

        //load fullscreen
        if(PlayerPrefs.GetInt("fullscreen") == 0)
        {
            //set gui toggle off
            fullscreenToggle.isOn = false;
        }
        else 
        {
            //set gui toggle on
            fullscreenToggle.isOn = true;
        }

        //load audio sliders
        float musicVol = PlayerPrefs.GetFloat("MusicVol");
        musicSlider.value = musicVol;
        mixer.SetFloat("MusicVol", musicVol);

        float SFXVol = PlayerPrefs.GetFloat("SFXVol");
        SFXSlider.value = SFXVol;
        mixer.SetFloat("SFXVol", SFXVol);
    }
    #endregion
    public void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "Testing Box");

        if (GUI.Button(new Rect(20, 40, 80, 20), "Press me")) // 20 x-direct, 40 y-direct, 80 width, 20height
        {
            Debug.Log("Press me button got pressed");
            IWantToDisableThis.SetActive(false);
        }
        
        if (GUI.Button(new Rect(20, 80, 80, 20), "Press me 2"))
        {
            Debug.Log("Press me 2 button got pressed");
            QuitGame();
        }

    }
}
