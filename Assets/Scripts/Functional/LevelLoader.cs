using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    // Start is called before the first frame update

    public void loadLevel()
    {
        StartCoroutine(loadNextScene(0));
    }

    IEnumerator loadNextScene(int sceneIndex)
    {
        transition.SetTrigger("start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneIndex);
    }
}