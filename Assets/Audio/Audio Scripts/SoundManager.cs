using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundEffectsSource;

    [Header("--------------------------------------")]
    [Header("Main Menu Audio")]
    public AudioClip backgroundMusic;
    public AudioClip uiSound;

    [Header("In Game Sound")]
    public AudioClip jump;
    public AudioClip fallOff;
    public AudioClip powerUp;

    private void Awake()
    {
        // Ensure the musicSource is set to loop for background music
        musicSource.loop = true;
    }

    public void playBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();

    }
    public void playSoundEffects(AudioClip clip) 
    { 
        soundEffectsSource.PlayOneShot(clip);
    }

}
