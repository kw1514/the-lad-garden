using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnPoint { get; private set; }
    FirstPersonController player;
    CharacterController characterController;

    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        characterController = GetComponent<CharacterController>();
        respawnPoint = characterController.transform.position;
    }

    void Update()
    {
        if (player != null && player.Grounded)
        {
            Vector3 currentPosition = characterController.transform.position;
            // Only update if moved more than 0.1 units
            if (Vector3.Distance(currentPosition, respawnPoint) > 0.1f) 
            {
                respawnPoint = currentPosition;
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallDetector"))
        {
            // Disable the character controller temporarily
            characterController.enabled = false;
            // Set the position
            transform.position = respawnPoint;
            // Re-enable the character controller
            characterController.enabled = true;
        }
    }
}
