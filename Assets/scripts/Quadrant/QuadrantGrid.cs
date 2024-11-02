using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuadrantGrid : MonoBehaviour
{

    public Quadrant[] QuadrantList;
    public EmptyQuadrant[] EmptyQuadrantList;
    public List<GameObject> gameObjects;

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
                if (Physics.Raycast(ray, out hit)) //Detects if ray cast is hit then output hit info to the dest variable
                {
                    EmptyQuadrant lastEmptySpace = EmptyQuadrantList[0]; // Need to make code to find the closest empty space and which one it is
                    Vector3 lastEmptySpacePos = lastEmptySpace.transform.position;
                    Quadrant thisQuadrant = hit.transform.GetComponent<Quadrant>();

                    foreach (KeyValuePair<string, GameObject> entry in thisQuadrant.neighbouringQuadrants)
                    {
                        if (entry.Value.name.Contains("Empty"))
                        {
                            //Debug.Log(this.transform.name + " -> " + entry);
                        }
                        //Debug.Log(thisQuadrant.transform.name + " -> " + entry);
                    }

                    //Diagonal quadrants also contain empty space
                    if //(thisQuadrant.neighbouringQuadrants.ContainsValue(lastEmptySpace.gameObject))
                       (Vector3.Distance(lastEmptySpace.transform.position, hit.transform.position) < 20)
                    {

                        //thisQuadrant.neighbouringQuadrants.Clear();

                        if (Vector3.Distance(hit.transform.position, player.transform.position) < 14)
                        {
                            Vector3 Diff = lastEmptySpacePos - hit.transform.position;
                            Vector3 playerOrigin = player.transform.position;
                            //player.transform.position = Vector3.Lerp(playerOrigin, playerOrigin + Diff, 0.05f);
                            player.transform.position = playerOrigin + Diff;
                        }

                      
                        //Collider colliderQuad = thisQuadrant.GetComponent<Collider>(); 
                        //Collider colliderEmpty = lastEmptySpace.GetComponent<Collider>();

                        //colliderQuad.enabled = false; Dont mess with colliders it prevents the onCollisionExit from working
                        //colliderEmpty.enabled = false;

                        lastEmptySpace.transform.position = thisQuadrant.targetposition;
                        thisQuadrant.targetposition = lastEmptySpacePos;

                        //colliderQuad.enabled = true;
                        //colliderEmpty.enabled = true;

                    }
                }
            }
        }
    }

    void setNeighbouringQuadrant(Quadrant thisQuadrant)
    {

    }

    void setNeighbouringQuadrants()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            GameObject gameObjectStart = gameObjects[i];
            Quadrant thisQuad = gameObjectStart.GetComponent<Quadrant>();

            if (thisQuad == null) // Only care about quadrants
            {
                continue;
            }


            foreach (GameObject gameObj in gameObjects)
            {
                if (gameObjectStart.Equals(gameObj))
                {
                    continue;
                }


            }

        }
    }

}


