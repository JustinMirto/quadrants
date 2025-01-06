using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;


    private void Start()
    {
        LoadVolume();            
    }

    public void SetMusicVolume()
    { 
        float volume = musicSlider.value;
        //Mathf.Log10(volume)*20 is needed because the audio mixer volume changes logarithmically but the slider changes linearlly.

        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }

    public void SetSoundEffectsVolume()
    {
        float volume = soundEffectsSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundEffectsVolume", volume);
    }


    private void LoadVolume()
    {
        SetMusicVolume();
        SetSoundEffectsVolume();    
    }
}
