using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Quadrant : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 targetposition; // Position the Quadrant is suppose to goto
    public Dictionary<string, GameObject> neighbouringQuadrants = new Dictionary<string, GameObject>();
    private Quaternion targetrotation = Quaternion.identity;

    [SerializeField] public bool bInfMovement = true;
    [SerializeField] public int iNumMovements = 5;

    void Awake()
    {
        targetposition = transform.position; //Make sure the quadrant doesn't move to Vector0 automatically
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetposition, 0.1f); // Will smoothly move from its original position to its target position 10% per frame
        transform.rotation = Quaternion.Lerp(transform.rotation, targetrotation, 0.1f); // Will smoothly rotate from its original rotation to its target rotation 10% per frame
    }

    public void ClearNeighbours()
    {
        this.neighbouringQuadrants.Clear();
    }

    public void SetNeighbour(string neighbourKey, GameObject neighbourValue)
    {
        this.neighbouringQuadrants[neighbourKey] = neighbourValue;
    }

    public GameObject GetNeighbour(string neighbourKey)
    {
        return neighbouringQuadrants[neighbourKey];
    }

    public void RotateClockwise()
    {
        Debug.Log("Rotating Quadrant at " + transform.rotation.y);
        targetrotation *= Quaternion.Euler(0, transform.rotation.y + 90, 0); // Creates a 90 degree rotation
    }

}
