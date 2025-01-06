using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    public void PlayGame()
    {
        // Load the next scene (e.g., Level 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel2()
    {
        // Load Level 2
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        // Load Level 3
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void pressButton()
    {
        soundManager.playSoundEffects(soundManager.uiSound);
    }
}
