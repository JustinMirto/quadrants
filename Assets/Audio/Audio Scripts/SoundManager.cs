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
    private static SoundManager instance;

    private void Awake()
    {
        // Singleton Pattern: Ensure only one SoundManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

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
