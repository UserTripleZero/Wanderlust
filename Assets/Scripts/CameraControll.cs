using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform player; // reference to the player object
    public Vector3 offset; // offset the camera from the player
    public float rotationSpeed; // rotation speed of the camera
    private float currentVerticalRotation = 0f; // current vertical rotation of the camera
    private float currentHorizontalRotation = 0f; // current horizontal rotation of the camera

    private void Start()
    {
        // Lock and hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void LateUpdate()
    {
        // Update camera position to follow the player
        transform.position = player.position + offset;

        // Handle mouse input to rotate the camera around the player
        currentHorizontalRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Clamp the vertical rotation so the camera doesn't flip over
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -89f, 90f);

        transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
    }
}

