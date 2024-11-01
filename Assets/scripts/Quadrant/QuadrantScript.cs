using System.Collections.Generic;
using UnityEngine;

public class Quadrant : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 targetposition; // Position the Quadrant is suppose to goto
    public Dictionary<string, GameObject> neighboringQuadrants = new Dictionary<string, GameObject>();
    void Start()
    {
        targetposition = transform.position; //Make sure the quadrant doesn't move to Vector0 automatically
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetposition, 0.05f); // Will smoothly move from its original position to its target position 5% per frame
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        //Debug.Log(transform.name + " has collided with " + collision.transform.name);
        Vector3 normal = collision.GetContact(0).normal;

        //Deal with corner states
        if (normal.x == 1) // Colliding object is touching the right face
        {
            Debug.Log(transform.name + " west face is touching " + collision.collider + "'s East face with a normal of " + normal);
        }
        if (normal.z == 1) // Colliding object is touching the up face
        {
            Debug.Log(transform.name + " south face is touching " + collision.collider + "'s North face with a normal of" + normal);
        }
        if (normal.x == -1) // Colliding object is touching the left face
        {
            Debug.Log(transform.name + " east is touching " + collision.collider + "'s West face with a normal of " + normal);
        }
        if (normal.z == -1) // Colliding object is touching the down face
        {
            Debug.Log(transform.name + " north face is touching " + collision.collider + "'s South face with a normal of " + normal);
        }
        */

        
        ContactPoint contact = collision.GetContact(0);
        Vector3 normal = contact.normal;
        Vector3 contactPoint = contact.point;

            // Identify the other cube
        GameObject otherCube = collision.gameObject;

            // Calculate distances from the contact point to the centers of both cubes
        Vector3 thisCubeCenter = transform.position;
        Vector3 otherCubeCenter = otherCube.transform.position;

            // Calculate the distance vector between the two cube centers
        Vector3 distanceVector = otherCubeCenter - thisCubeCenter;

        // Check which cube is adjacent or diagonal
        if (Mathf.Abs(normal.x) > 0.9f) // Normal is primarily in the x direction
        {
            if (Mathf.Abs(distanceVector.z) < 0.1f) // No significant z difference
            {
                if(normal.x > 0)
                {
                    if (this.neighboringQuadrants.ContainsKey("East"))
                    {
                        this.neighboringQuadrants["East"] = otherCube;
                    }
                    else
                    {
                        this.neighboringQuadrants.Add("East", otherCube);
                    }
                    //This quadrant's east face is in contact
                }
                else
                {
                    if (this.neighboringQuadrants.ContainsKey("West"))
                    {
                        this.neighboringQuadrants["West"] = otherCube;
                    }
                    else
                    {
                        this.neighboringQuadrants.Add("West", otherCube);
                    }
                    //This quadrant's west face is in contact
                }

                //Debug.Log($"{transform.name} is directly touching {otherCube.name} on the East/West face.");
            }
            else
            {
                //Debug.Log($"{transform.name} is at a diagonal with {otherCube.name}.");
            }
        }
        else if (Mathf.Abs(normal.z) > 0.9f) // Normal is primarily in the z direction
        {
            if (Mathf.Abs(distanceVector.x) < 0.1f) // No significant x difference
            {
                if(normal.z > 0)
                {
                    //This quadrant's north face is in contact
                    if (this.neighboringQuadrants.ContainsKey("North"))
                    {
                        this.neighboringQuadrants["North"] = otherCube;
                    }
                    else
                    {
                        this.neighboringQuadrants.Add("North", otherCube);
                    }
                }
                else
                {
                    //This quadrant's south face is in contact
                    if (this.neighboringQuadrants.ContainsKey("South"))
                    {
                        this.neighboringQuadrants["South"] = otherCube;
                    }
                    else
                    {
                        this.neighboringQuadrants.Add("South", otherCube);
                    }
                }

                //Debug.Log($"{transform.name} is directly touching {otherCube.name} on the North/South face.");
            }
            else
            {
                //Debug.Log($"{transform.name} is at a diagonal with {otherCube.name}.");
            }
        }

        foreach (KeyValuePair<string, GameObject> entry in this.neighboringQuadrants)
        {
            Debug.Log(this.transform.name + " -> " + entry);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name + " has triggered with " + other.transform.name);
    }
}
