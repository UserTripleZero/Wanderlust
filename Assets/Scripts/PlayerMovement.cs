using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;    // The speed at which the player moves
    public float jumpForce = 10f;    // The force applied to the player when jumping
    public float sprintMultiplier = 2f;    // The multiplier applied to the player's speed when sprinting

    private bool isGrounded;    // Whether or not the player is on the ground
    private Rigidbody rb;    // The player's Rigidbody component

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log(1 / Time.deltaTime);

        // Check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.1f);

        // Move the player based on input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Project the camera vectors onto the XZ plane
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the player's movement vector based on input and camera orientation
        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal) * speed * Time.fixedDeltaTime;

        // Apply sprinting multiplier if shift is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }

        // Jump if space is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Apply the movement vector to the player's Rigidbody component
        rb.MovePosition(transform.position + movement);
    }
}
