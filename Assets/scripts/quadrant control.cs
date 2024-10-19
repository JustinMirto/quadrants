using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadrantcontrol : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace;
    private Camera _camera;
    public Vector2 MoveValue;
    public float fMouseX = 0.00f;
    public float fMouseY = 0.00f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Quadrant Control script has started");
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Registers every frame the mouse button is held down
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse is held down");

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(Vector3.Distance(emptySpace.transform.position, hit.transform.position));
                if(Vector3.Distance(emptySpace.transform.position, hit.transform.position) < 20)
                {
                    Vector3 lastEmptySpacePos = emptySpace.transform.position;
                    emptySpace.transform.position = hit.transform.position;
                    hit.transform.position = lastEmptySpacePos;
                }
            }

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
