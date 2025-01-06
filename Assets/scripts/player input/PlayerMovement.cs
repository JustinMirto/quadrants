using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] float speed = 2;


    //Enables Audio
    SoundManager soundManager;
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        move = input.actions.FindAction("move");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 0.5f, 1.0f);
        Physics.gravity = new Vector3(0, -70, 0);


    }
    int life = 3;

    void Update()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        respawnPoint.y += 4;
        movement();
  
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //FindAnyObjectByType<AudioManager>().Play("PlayerJump");
            soundManager.playSoundEffects(soundManager.jump);
            Debug.Log("Press");
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }

        //How fast the player falls back down after jumping
        if (!isGrounded) {
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
            
        }

        //int life = 3;
        //If the player falls offscreen respawns
        if (this.gameObject.transform.position.y < -10)
        {
            Debug.Log(respawnPoint);
            this.gameObject.transform.position = respawnPoint;
            rb.velocity = Vector3.zero;
            life--;
                if (life == 2) 
                {
                GameObject heartLife = GameObject.FindWithTag("heart2");
                Destroy(heartLife);
                }

                else if (life == 1)
                {
                    GameObject heartLife = GameObject.FindWithTag("heart1");
                    Destroy(heartLife);
                }

                else if (life == 0)
                {
                    GameObject heartLife = GameObject.FindWithTag("heart");
                    Destroy(heartLife);
                }


            soundManager.playSoundEffects(soundManager.fallOff);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn") || collision.gameObject.CompareTag("RotateClockwise") || collision.gameObject.CompareTag("MoveOnly")) 
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
