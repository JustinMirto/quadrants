using UnityEngine;
using UnityEngine.InputSystem;

public class QuadrantGrid : MonoBehaviour
{

    public Quadrant[] QuadrantList;
    public EmptyQuadrant[] EmptyQuadrantList;
    private Camera _camera;
    [SerializeField] private GameObject player;
    void Awake()
    {
        QuadrantList = GetComponentsInChildren<Quadrant>();
        EmptyQuadrantList = GetComponentsInChildren<EmptyQuadrant>();
        
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
                    if (!hit.transform.name.Contains("Quad")) //If touching the player and not the quadrant
                    {
                        return;
                    }

                    EmptyQuadrant lastEmptySpace = EmptyQuadrantList[0]; // Need to make code to find the closest empty space and which one it is
                    Vector3 lastEmptySpacePos = lastEmptySpace.transform.position;

                    if (Vector3.Distance(lastEmptySpace.transform.position, hit.transform.position) < 20)
                    {
                        if (Vector3.Distance(hit.transform.position, player.transform.position) < 14)
                        {
                            Vector3 Diff = lastEmptySpacePos - hit.transform.position;
                            Vector3 playerOrigin = player.transform.position;
                            //player.transform.position = Vector3.Lerp(playerOrigin, playerOrigin + Diff, 0.05f);
                            player.transform.position = playerOrigin + Diff;
                        }
                        Quadrant thisQuadrant = hit.transform.GetComponent<Quadrant>();
                        lastEmptySpace.transform.position = thisQuadrant.targetposition;
                        thisQuadrant.targetposition = lastEmptySpacePos;
                        //Debug.Log(lastEmptySpacePos);
                    }
                }
            }
        }
    }
}
