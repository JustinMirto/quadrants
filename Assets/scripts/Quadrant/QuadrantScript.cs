using System.Collections.Generic;
using UnityEngine;

public class Quadrant : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 targetposition; // Position the Quadrant is suppose to goto
    public Dictionary<string, GameObject> neighbouringQuadrants = new Dictionary<string, GameObject>();

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
        //neighbouringQuadrants.Clear();
        Debug.Log(this.transform.name + " Collided with " + collision.collider.name);
        
        ContactPoint contact = collision.GetContact(0);
        Vector3 normal = contact.normal;
        Vector3 contactPoint = contact.point;

            // Identify the other Quadrant
        GameObject otherQuadrant = collision.gameObject;

            // Calculate distances from the contact point to the centers of both Quadrants
        Vector3 thisQuadrantCenter = transform.position;
        Vector3 otherQuadrantCenter = otherQuadrant.transform.position;

            // Calculate the distance vector between the two Quadrant centers
        Vector3 distanceVector = otherQuadrantCenter - thisQuadrantCenter;

        // Check which Quadrant is adjacent or diagonal
        if (Mathf.Abs(normal.x) > 0.9f) // Normal is primarily in the x direction
        {
            if (Mathf.Abs(distanceVector.z) < 0.1f) // No significant z difference
            {
                if(normal.x > 0)
                {
                    if (this.neighbouringQuadrants.ContainsKey("East"))
                    {
                        this.neighbouringQuadrants["East"] = otherQuadrant;
                    }
                    else
                    {
                        this.neighbouringQuadrants.Add("East", otherQuadrant);
                    }
                    //This quadrant's east face is in contact
                }
                else
                {
                    if (this.neighbouringQuadrants.ContainsKey("West"))
                    {
                        this.neighbouringQuadrants["West"] = otherQuadrant;
                    }
                    else
                    {
                        this.neighbouringQuadrants.Add("West", otherQuadrant);
                    }
                    //This quadrant's west face is in contact
                }

                //Debug.Log($"{transform.name} is directly touching {otherQuadrant.name} on the East/West face.");
            }
            else
            {
                //Debug.Log($"{transform.name} is at a diagonal with {otherQuadrant.name}.");
            }
        }
        else if (Mathf.Abs(normal.z) > 0.9f) // Normal is primarily in the z direction
        {
            if (Mathf.Abs(distanceVector.x) < 0.1f) // No significant x difference
            {
                if(normal.z > 0)
                {
                    //This quadrant's north face is in contact
                    if (this.neighbouringQuadrants.ContainsKey("North"))
                    {
                        this.neighbouringQuadrants["North"] = otherQuadrant;
                    }
                    else
                    {
                        this.neighbouringQuadrants.Add("North", otherQuadrant);
                    }
                }
                else
                {
                    //This quadrant's south face is in contact
                    if (this.neighbouringQuadrants.ContainsKey("South"))
                    {
                        this.neighbouringQuadrants["South"] = otherQuadrant;
                    }
                    else
                    {
                        this.neighbouringQuadrants.Add("South", otherQuadrant);
                    }
                }

                //Debug.Log($"{transform.name} is directly touching {otherQuadrant.name} on the North/South face.");
            }
            else
            {
                //Debug.Log($"{transform.name} is at a diagonal with {otherQuadrant.name}.");
            }
        }

        foreach (KeyValuePair<string, GameObject> entry in this.neighbouringQuadrants)
        {
            if (entry.Value.name.Contains("Empty"))
            {
                //Debug.Log(this.transform.name + " -> " + entry);
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.collider.name + " This collider is exiting " + this.transform.name);
    }
}
