using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 respawnPoint { get; private set; }
    FirstPersonController player;

    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        respawnPoint = transform.position;
    }


    void Update()
    {
        // if (player.Grounded)
        // {
        //     respawnPoint = transform.position;
        //     Debug.Log(respawnPoint);
        // }
    }

    protected void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.tag == "FallDetector")
        // {
        //     Debug.Log("Fall");
        //     player.transform.position = respawnPoint;
        // }
    }
}
