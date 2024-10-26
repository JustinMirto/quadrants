using UnityEngine;

public class Quadrant : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 targetposition; // Position the Quadrant is suppose to goto
    void Start()
    {
        targetposition = transform.position; //Make sure the quadrant doesn't move to Vector0 automatically
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetposition, 0.05f); // Will smoothly move from its original position to its target position 5% per frame
    }
}
