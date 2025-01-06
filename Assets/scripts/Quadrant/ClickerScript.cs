using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerScipt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Only execute if the game is not paused
        if (!PauseMenu.GameIsPaused)
        {
            this.transform.rotation = Quaternion.identity; // Lock rotation
        }
    }
}
