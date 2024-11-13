using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class QuadrantGrid : MonoBehaviour
{

    public Quadrant[] QuadrantList;
    public EmptyQuadrant[] EmptyQuadrantList;
    public List<GameObject> gameObjects;

    public float fQuadrantSize = 5f; //Current scale in terms of the x and z coords

    private Camera _camera;
    [SerializeField] private GameObject player;
    void Awake()
    {
        QuadrantList = GetComponentsInChildren<Quadrant>();
        EmptyQuadrantList = GetComponentsInChildren<EmptyQuadrant>();
        gameObjects = new List<GameObject> ();
        foreach (Transform child in transform)
            gameObjects.Add(child.gameObject);
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
            // Registers every frame the mouse button is held down
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                RaycastHit hit2;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Quadrant"))) //Detects if ray cast is hit then output hit info to the dest variable
                {

                    if (Physics.Raycast(ray, out hit2, Mathf.Infinity, 1 << LayerMask.NameToLayer("QuadrantClick"))) //Detects which collider has been hit by the ray cast
                    {
                        //Debug.Log(hit2.transform.name);

                        Quadrant thisQuadrant = hit.transform.GetComponentInParent<Quadrant>();
                        if (thisQuadrant == null)
                        {
                            thisQuadrant = hit.transform.GetComponent<Quadrant>();
                        }

                        GameObject neighbour = null;

                        //Debug.Log(hit.transform.name);
                        //Debug.Log(thisQuadrant);

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

                        if (neighbour.GetComponent<EmptyQuadrant>() != null)
                            //(thisQuadrant.neighbouringQuadrants.ContainsValue(lastEmptySpace.gameObject))
                        //(Vector3.Distance(lastEmptySpace.transform.position, hit.transform.position) < 20)
                        {
                            Debug.Log("Found empty quadrant");

                            EmptyQuadrant lastEmptySpace = neighbour.GetComponent<EmptyQuadrant>();
                            Vector3 lastEmptySpacePos = lastEmptySpace.transform.position;

                            //thisQuadrant.neighbouringQuadrants.Clear();

                            if (player != null)
                            {
                                if (Vector3.Distance(hit.transform.position, player.transform.position) < 14)
                                {
                                    Vector3 Diff = lastEmptySpacePos - hit.transform.position;
                                    Vector3 playerOrigin = player.transform.position;
                                    //player.transform.position = Vector3.Lerp(playerOrigin, playerOrigin + Diff, 0.05f);
                                    player.transform.position = playerOrigin + Diff;
                                }
                            }
                            
                            lastEmptySpace.transform.position = thisQuadrant.targetposition;
                            thisQuadrant.targetposition = lastEmptySpacePos;

                            InitialiseGrid();

                        }

                    }
                }

                
            }
        }
    }


    //Taken from chatGPT
    //Will need to update to reduce strain
    void InitialiseGrid()
    {
        Debug.Log("Reinitialising grid");
        Debug.Log("Quad size is " + fQuadrantSize); // For some reason the quad size remains at 5 will need to check this?

        // Map to hold quadrants by their 2D grid positions (x, z only)
        Dictionary<Vector2, GameObject> gridMap = new Dictionary<Vector2, GameObject>();


        foreach (Quadrant quadrant in QuadrantList)
        {
            Vector2 gridPos = new Vector2(
                Mathf.Round(quadrant.targetposition.x / fQuadrantSize),
                Mathf.Round(quadrant.targetposition.z / fQuadrantSize)
            );
            gridMap[gridPos] = quadrant.gameObject;
            Debug.Log(gridPos);
        }

        foreach (EmptyQuadrant empty in EmptyQuadrantList)
        {
            Vector2 gridPos = new Vector2(
                Mathf.Round(empty.transform.position.x / fQuadrantSize),
                Mathf.Round(empty.transform.position.z / fQuadrantSize)
            );
            gridMap[gridPos] = empty.gameObject;
            Debug.Log(gridPos);
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


