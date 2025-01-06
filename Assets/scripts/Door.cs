using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private bool bIsOpen = false;
    GameObject[] doors;

    private void Awake()
    {
        doors = GetComponentsInChildren<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject doorLeft = doors[0];
        GameObject doorRight = doors[1];

        if (bIsOpen)
        {
            

        }
        else
        {
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bIsOpen)
        {
            bIsOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (bIsOpen)
        {
            bIsOpen = true;
        }
    }

}
