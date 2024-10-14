using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadrantcontrol : MonoBehaviour
{
    public Vector2 MoveValue;
    public float fMouseX = 0.00f;
    public float fMouseY = 0.00f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Quadrant Control script has started");
    }

    // Update is called once per frame
    void Update()
    {
        // Registers every frame the mouse button is held down
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse is held down");

            fMouseX = Input.GetAxis("Mouse X");
            fMouseY = Input.GetAxis("Mouse Y");
            if (fMouseX > 0) //Moving right
            {

            }

            if (fMouseX < 0) //Moving left
            {

            }

            if (fMouseY > 0) //Moving down
            {

            }

            if (fMouseY > 0) //Moving up
            {

            }
        }
        
    }
}
