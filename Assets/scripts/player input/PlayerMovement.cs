using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody))] 
public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction move;

    public Vector3 jump;
    public float jumpSpeed = 25.0f;
    public float fallSpeed = 2.0f;
    public Vector3 respawnPoint = Vector3.zero;

    public bool isGrounded;
    Rigidbody rb;
    [SerializeField] float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        move = input.actions.FindAction("move");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        Physics.gravity = new Vector3(0, -100, 0);
    }

    void Update()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        Debug.Log("Respawn Point is: "+ respawnPoint);
        movement();
  
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerJump");
            Debug.Log("Press");
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }

        //How fast the player falls back down after jumping
        if (!isGrounded) {
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
            
        }


        //If the player falls offscreen respawns
        if (this.gameObject.transform.position.y < -10)
        {
            Debug.Log(this.gameObject.transform.position.y);
            this.gameObject.transform.position = respawnPoint;
            rb.velocity = Vector3.zero;
            FindAnyObjectByType<AudioManager>().Play("FallOff");
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Quadrant") || collision.gameObject.CompareTag("Respawn")) 
        {
            isGrounded = true;
        }
    }

    void movement() { 
        playerMovement();

    }

    void playerMovement() {
        Vector2 direction = move.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
    }
}
