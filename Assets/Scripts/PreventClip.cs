using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventClip : MonoBehaviour
{
    public float jumpForce = 10f;
    public float maxClipDepth = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 jumpDirection = (collision.contacts[0].point - collision.gameObject.transform.position).normalized;

            // Check if player has clipped too far into the terrain
            float clipDepth = Mathf.Abs(Vector3.Distance(collision.contacts[0].point, collision.gameObject.transform.position) - collision.gameObject.GetComponent<SphereCollider>().radius);
            if (clipDepth > maxClipDepth)
            {
                // Teleport player back out
                Vector3 newPos = collision.gameObject.transform.position - (jumpDirection * clipDepth);
                collision.gameObject.transform.position = newPos;
            }
            else
            {
                // Add impulse force to make player jump back
                playerRb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            }
        }
    }
}


