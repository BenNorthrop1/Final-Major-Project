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
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        MasterVolumeSlider.value = MasterVolume;

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        MusicVolumeSlider.value = MusicVolume;

        float OceanVolume = PlayerPrefs.GetFloat("OceanVolume");
        OceanVolumeSlider.value = OceanVolume;
        
        float MiscVolume = PlayerPrefs.GetFloat("MiscVolume");
        MiscVolumeSlider.value = MiscVolume;

    }

    public void SetMasterVolume()
    {
        float MasterVolume = MasterVolumeSlider.value;
        audioMixer.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
    }

    public void SetMusicVolume()
    {
        float MusicVolume = MusicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
    }

    public void SetOceanVolume()
    {
        float OceanVolume = OceanVolumeSlider.value;
        audioMixer.SetFloat("OceanVolume", OceanVolume);
        PlayerPrefs.SetFloat("OceanVolume", OceanVolume);
    }

    public void SetMiscVolume()
    {
        float MiscVolume = MiscVolumeSlider.value;
        audioMixer.SetFloat("MiscVolume", MiscVolume);
        PlayerPrefs.SetFloat("MiscVolume", MiscVolume);
    }



}
