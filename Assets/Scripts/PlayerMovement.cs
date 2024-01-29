using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float Speed = 5f;  // Movement speed
    public float JumpForce = 5f;  // Jump force
    private bool Grounded;  // Whether the player is on the ground

    void Start()
    {
        // Initialize Rigidbody and freeze rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get keyboard input and move
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * Speed * Time.deltaTime;
        transform.Translate(movement);

        // Jump
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Reset vertical velocity, apply jump force, and set grounded to false
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        Grounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is on the ground by detecting collision with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
}
