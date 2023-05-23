using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider OceanVolumeSlider;
    public Slider MiscVolumeSlider;

    private void Awake() 
    {
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        MasterVolumeSlider.value = MasterVolume;

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        MusicVolumeSlider.value = MusicVolume;

        float OceanVolume = PlayerPrefs.GetFloat("OceanVolume", 1);
        OceanVolumeSlider.value = OceanVolume;
        
        float MiscVolume = PlayerPrefs.GetFloat("MiscVolume", 1);
        MiscVolumeSlider.value = MiscVolume;

    }

    public void SetMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetOceanVolume(float sliderValue)
    {
        audioMixer.SetFloat("OceanVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("OceanVolume", sliderValue);
    }

    public void SetMiscVolume(float sliderValue)
    {
        audioMixer.SetFloat("MiscVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MiscVolume", sliderValue);
    }

    public void ResetAllVolume()
    {
        PlayerPrefs.DeleteKey("MasterVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("OceanVolume");
        PlayerPrefs.DeleteKey("MiscVolume");

        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        MasterVolumeSlider.value = MasterVolume;

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        MusicVolumeSlider.value = MusicVolume;

        float OceanVolume = PlayerPrefs.GetFloat("OceanVolume", 1);
        OceanVolumeSlider.value = OceanVolume;
        
        float MiscVolume = PlayerPrefs.GetFloat("MiscVolume", 1);
        MiscVolumeSlider.value = MiscVolume;
    }

}
