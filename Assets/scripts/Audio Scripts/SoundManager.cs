using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    [Header("Level Background Sound")]
    public AudioClip LevelOne;

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            musicSource.clip = LevelOne;
            musicSource.Play();
        }
    }

        public void playSoundEffects(AudioClip clip) 
    { 
        soundEffectsSource.PlayOneShot(clip);
    }

}
