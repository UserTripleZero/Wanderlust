using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float sprintMultiplier = 2f;
    public float raycastDistance = 1.5f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody rb;
    private Vector3 lastValidPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastValidPosition = transform.position;
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, raycastDistance, groundLayer);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal) * speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance, groundLayer))
        {
            lastValidPosition = transform.position;
        }

        if (!Physics.Raycast(transform.position, -Vector3.up, out hit, raycastDistance, groundLayer))
        {
            transform.position = lastValidPosition;
        }

        rb.MovePosition(transform.position + movement);

        if (hit.collider != null) {
            Debug.DrawLine(transform.position, hit.point, Color.green, 1f);
        } else {
            Debug.DrawLine(transform.position, transform.position, Color.red, 1f);
        }
    }
}


