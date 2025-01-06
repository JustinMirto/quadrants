using UnityEngine;
using TMPro; // TextMeshProUGUI
using System.Collections;
using UnityEngine.SceneManagement; // To handle scene management

public class Powerup : MonoBehaviour
{
    public TextMeshProUGUI powerupText;  
    public float fadeDuration = 1f;      // Duration for fade
    public float moveSpeed = 20f;        // Speed of upward movement

    // Enables audio
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    void Start()
    {
        powerupText.gameObject.SetActive(false);  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerupText.text = "Powerup Collected!";
            powerupText.gameObject.SetActive(true);
            StartCoroutine(FadeAndMoveText());
            Destroy(gameObject);
            soundManager.playSoundEffects(soundManager.powerUp);

            // Load the next scene
            LoadNextScene();
        }
    }

    IEnumerator FadeAndMoveText()
    {
        float elapsedTime = 0f;
        Color originalColor = powerupText.color;
        Vector3 originalPosition = powerupText.rectTransform.position;

        while (elapsedTime < fadeDuration)
        {
            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            powerupText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Move upwards
            powerupText.rectTransform.position = originalPosition + Vector3.up * moveSpeed * (elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        powerupText.gameObject.SetActive(false);  // Hide after fade out
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) > 3 ? 0 : currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
