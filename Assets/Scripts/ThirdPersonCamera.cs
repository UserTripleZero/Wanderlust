using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;    // The target object to follow
    public float distance = 10f;    // The distance from the target
    public float height = 5f;    // The height above the target
    public float damping = 2f;    // The amount of damping to apply to the camera movement
    public float rotationSpeed = 2f;    // The speed at which to rotate the camera

    private float currentDistance;    // The current distance from the target
    private float desiredDistance;    // The desired distance from the target
    private float mouseX;    // The current mouse X position
    private float mouseY;    // The current mouse Y position
    //private float rotationY = 0f;    // The current rotation of the camera around the Y axis
    private RaycastHit hit;    // The raycast hit information

    void Start()
    {
        // Set the current distance to the default distance
        currentDistance = distance;
        desiredDistance = distance;

        // Lock the cursor to the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // If there is a player to follow
        if (player != null)
        {
            // Get the current mouse position
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Clamp the Y rotation so the camera doesn't flip over
            mouseY = Mathf.Clamp(mouseY, -90f, 90f);

            // Rotate the camera around the X and Y axes based on the mouse position
            Quaternion rotationX = Quaternion.Euler(mouseY, mouseX, 0f);

            // Calculate the desired position of the camera
            Vector3 position = player.position - (rotationX * Vector3.forward * currentDistance);
            position.y = player.position.y + height;

            // Check if the camera is colliding with the floor
            if (Physics.Raycast(player.position, -Vector3.up, out hit, height))
            {
                position.y = Mathf.Max(player.position.y + height, hit.point.y);
            }

            // Smoothly move the camera to the desired position and rotation
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationX, Time.deltaTime * damping);

            // Use the scroll wheel to zoom the camera in and out
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
            desiredDistance = Mathf.Clamp(desiredDistance, 2f, 20f);
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * damping);
        }
    }
}
