using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.EventTrigger;

public class QuadrantGrid : MonoBehaviour
{

    public Quadrant[] QuadrantList;
    public EmptyQuadrant[] EmptyQuadrantList;
    public List<GameObject> gameObjects;

    public float fQuadrantSize = 5f; //Current scale in terms of the x and z coords

    [SerializeField] private InputAction position, press;
    [SerializeField] private float swipeResistance = 100f;
    [SerializeField] private float pressLimit = 10f;
    private bool bIsPressed;

    private Vector2 initialSwipePos;
    private Vector2 CurrentSwipePos => position.ReadValue<Vector2>();
    private Vector2 swipeDirection = Vector2.zero;

    private Camera _camera;
    [SerializeField] private GameObject player;

    private Quadrant currentQuadrant;

    void Awake()
    {
        QuadrantList = GetComponentsInChildren<Quadrant>();
        EmptyQuadrantList = GetComponentsInChildren<EmptyQuadrant>();
        gameObjects = new List<GameObject> ();
        foreach (Transform child in transform)
        {
            gameObjects.Add(child.gameObject);
        }

        position.Enable();
        press.Enable();
        press.performed += _ => {
            initialSwipePos = CurrentSwipePos;
        };
        press.canceled += _ => DetectSwipe();
        currentQuadrant = null;
        bIsPressed = false;
    }

    private void DetectSwipe()
    {
        Vector2 delta = CurrentSwipePos - initialSwipePos;
        swipeDirection = Vector2.zero;

        Debug.Log("Detecting swipe");

        if (Math.Abs(delta.x) > swipeResistance) // Swipe on the x axis
        {
            swipeDirection.x = Mathf.Clamp(delta.x, -1, 1);
        }

        if (Math.Abs(delta.y) > swipeResistance) // Swipe on the x axis
        {
            swipeDirection.y = Mathf.Clamp(delta.y, -1, 1);
        }

        if (Math.Abs(delta.x) <= pressLimit) // Press on the x axis
        {
            if (Math.Abs(delta.y) <= pressLimit) // Press on the x axis
            {
                bIsPressed = true;
            }
        }

        Debug.Log(swipeDirection + " is the swipe direction");
        
    }

    private void Start()
    {
        _camera = Camera.main;
        Debug.Log("new control script is starting");
        
        InitialiseGrid();
    }

    // Update is called once per frame
    void Update()
    {
        HandleQuadrantMovement();
    }

    void HandleQuadrantMovement()
    {
        if (_camera != null)
        {
            //Quadrant currentQuadrant = null;

            // Registers every frame the mouse button is held down
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                RaycastHit hit2;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Quadrant"))) //Detects if ray cast is hit then output hit info to the dest variable
                {

                    currentQuadrant = hit.transform.GetComponentInParent<Quadrant>();
                    if (currentQuadrant == null)
                    {
                        currentQuadrant = hit.transform.GetComponent<Quadrant>();
                    }

                    if (Physics.Raycast(ray, out hit2, Mathf.Infinity, 1 << LayerMask.NameToLayer("QuadrantClick"))) //Detects which collider has been hit by the ray cast
                    {
                        Debug.Log(hit2.transform.name);

                        GameObject neighbour = null;

                        //Debug.Log(hit.transform.name);
                        //Debug.Log(thisQuadrant);

                        //handleDirectionMovementWithColliders(thisQuadrant, hit2);
                        //HandleDirectionMovementWithSwipe(currentQuadrant);
                    }
                    else
                    {
                        if (currentQuadrant.CompareTag("RotateClockwise"))
                        {
                            //Debug.Log("Rotating Quadrant");
                            //thisQuadrant.RotateClockwise();
                        }
                    }


                    if (bIsPressed)
                    {
                        if (currentQuadrant.CompareTag("RotateClockwise"))
                        {
                            Debug.Log("Rotating Quadrant");
                            currentQuadrant.RotateClockwise();
                        }
                        bIsPressed = false;
                    }

                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Handling mouse up " + currentQuadrant);
                if (currentQuadrant != null)
                {
                    Debug.Log("Tag is " + currentQuadrant.tag);
                    if (currentQuadrant.CompareTag("MoveOnly") || currentQuadrant.CompareTag("Respawn"))
                    {
                        HandleDirectionMovementWithSwipe(currentQuadrant);
                        currentQuadrant = null;
                    }
                }
            }
        }
    }

    void handleDirectionMovementWithColliders(Quadrant thisQuadrant, RaycastHit hit2)
    {
        GameObject neighbour = null;

        //Debug.Log(hit.transform.name);
        //Debug.Log(thisQuadrant);

        Debug.Log(thisQuadrant.neighbouringQuadrants.Count);
        foreach (KeyValuePair<string, GameObject> pair in thisQuadrant.neighbouringQuadrants)
        {
            Debug.Log(pair);
        }

        if (hit2.transform.name.Contains("North"))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("North", out neighbour);
        }
        else if (hit2.transform.name.Contains("South"))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("South", out neighbour);
        }
        else if (hit2.transform.name.Contains("East"))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("East", out neighbour);
        }
        else if (hit2.transform.name.Contains("West"))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("West", out neighbour);
        }

        //EmptyQuadrant lastEmptySpace = EmptyQuadrantList[0]; // Need to make code to find the closest empty space and which one it is
        if (!neighbour)
        {
            Debug.Log("There are no neighbours");
            return;
        }

        Debug.Log(neighbour.gameObject.GetType());
        MoveQuadrant(thisQuadrant, neighbour);
    }

    void HandleDirectionMovementWithSwipe(Quadrant thisQuadrant)
    {
        GameObject neighbour = null;

        if (swipeDirection.Equals(Vector2.zero))
        {
            return;
        }

        if (swipeDirection.Equals(Vector2.up))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("North", out neighbour);
        }
        else if (swipeDirection.Equals(Vector2.down))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("South", out neighbour);
        }
        else if (swipeDirection.Equals(Vector2.right))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("East", out neighbour);
        }
        else if (swipeDirection.Equals(Vector2.left))
        {
            thisQuadrant.neighbouringQuadrants.TryGetValue("West", out neighbour);
        }

        //EmptyQuadrant lastEmptySpace = EmptyQuadrantList[0]; // Need to make code to find the closest empty space and which one it is
        if (!neighbour)
        {
            Debug.Log("There are no neighbours");
            return;
        }

        MoveQuadrant(thisQuadrant, neighbour);
    }

    void MoveQuadrant(Quadrant thisQuadrant, GameObject neighbour)
    {
        if (neighbour.GetComponent<EmptyQuadrant>() != null)
        //(thisQuadrant.neighbouringQuadrants.ContainsValue(lastEmptySpace.gameObject))
        //(Vector3.Distance(lastEmptySpace.transform.position, hit.transform.position) < 20)
        {
            //Debug.Log("Found empty quadrant");

            EmptyQuadrant lastEmptySpace = neighbour.GetComponent<EmptyQuadrant>();
            Vector3 lastEmptySpacePos = lastEmptySpace.transform.position;

            //thisQuadrant.neighbouringQuadrants.Clear();

            if (player != null)
            {
                if (Vector3.Distance(thisQuadrant.transform.position, player.transform.position) < 14)
                {
                    Vector3 Diff = lastEmptySpacePos - thisQuadrant.transform.position;
                    Vector3 playerOrigin = player.transform.position;
                    //player.transform.position = Vector3.Lerp(playerOrigin, playerOrigin + Diff, 0.05f);
                    player.transform.position = playerOrigin + Diff;
                }
            }

            lastEmptySpace.transform.position = thisQuadrant.targetposition;
            thisQuadrant.targetposition = lastEmptySpacePos;

            InitialiseGrid();

        }

        currentQuadrant = null;
    }

    //Taken from chatGPT
    //Will need to update to reduce strain
    void InitialiseGrid()
    {
        //Debug.Log("Reinitialising grid");
        //Debug.Log("Quad size is " + fQuadrantSize); // For some reason the quad size remains at 5 will need to check this?

        // Map to hold quadrants by their 2D grid positions (x, z only)
        Dictionary<Vector2, GameObject> gridMap = new Dictionary<Vector2, GameObject>();


        foreach (Quadrant quadrant in QuadrantList)
        {
            Vector2 gridPos = new Vector2(
                Mathf.Round(quadrant.targetposition.x / fQuadrantSize),
                Mathf.Round(quadrant.targetposition.z / fQuadrantSize)
            );
            gridMap[gridPos] = quadrant.gameObject;
        }

        foreach (EmptyQuadrant empty in EmptyQuadrantList)
        {
            Vector2 gridPos = new Vector2(
                Mathf.Round(empty.transform.position.x / fQuadrantSize),
                Mathf.Round(empty.transform.position.z / fQuadrantSize)
            );
            gridMap[gridPos] = empty.gameObject;
        }

        foreach(KeyValuePair<Vector2, GameObject> pair in gridMap)
        {
            //Debug.Log(pair);
        }

        // Assign neighbors (quadrants or empty spaces) for each quadrant
        foreach (Quadrant quadrant in QuadrantList)
        {
            Vector2 pos = new Vector2(
                Mathf.Round(quadrant.targetposition.x / fQuadrantSize),
                Mathf.Round(quadrant.targetposition.z / fQuadrantSize)
            );

            quadrant.ClearNeighbours();
            //Debug.Log(pos);


            // Check for neighboring quadrants or empty spaces in each direction
            if (gridMap.TryGetValue(pos + Vector2.up, out GameObject north))
            {
                //Debug.Log("Setting north");
                quadrant.SetNeighbour("North", north);
            }
            if (gridMap.TryGetValue(pos + Vector2.down, out GameObject south))
            {
                //Debug.Log("Setting south");
                quadrant.SetNeighbour("South", south);
            }
            if (gridMap.TryGetValue(pos + Vector2.right, out GameObject east))
            {
                //Debug.Log("Setting east");
                quadrant.SetNeighbour("East", east);
            }
            if (gridMap.TryGetValue(pos + Vector2.left, out GameObject west))
            {
                //Debug.Log("Setting west");
                quadrant.SetNeighbour("West", west);
            }
        }



    }


}


