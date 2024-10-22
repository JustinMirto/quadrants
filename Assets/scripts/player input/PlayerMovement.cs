using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))] 
public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction move;

    public Vector3 jump;
    public float jumpSpeed = 3.0f;
    public Vector3 respawnPoint = Vector3.zero;

    public bool isGrounded;
    Rigidbody rb;
    [SerializeField] float speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        move = input.actions.FindAction("move");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        //If there is a respawn point
        if (collision.gameObject.transform.childCount > 0)
        {
            respawnPoint = collision.transform.GetChild(0).gameObject.transform.position;
            Debug.Log(respawnPoint);
        }
    }
    */
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }

        /*
        //If the player falls offscreen respawns
        if (this.gameObject.transform.position.y < -10)
        {
            Debug.Log(this.gameObject.transform.position.y);
            this.gameObject.transform.position = respawnPoint;
        }
        */

    }

    void movement() { 
        playerMovement();
    }

    void playerMovement() {
        Vector2 direction = move.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;

    }
}
