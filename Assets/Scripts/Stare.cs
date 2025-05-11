using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stare : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    // Vector3 lookAt;

    void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform;
        }
        // Vector3 lookAt = new Vector3(-target.position.x, -target.position.y, -target.position.z);
        // lookAt = new Vector3(-target.position.x, -target.position.y, -target.position.z);
    }

    void Update()
    {
        // lookAt = new Vector3(-target.position.x, -target.position.y, -target.position.z);
        // Vector3 lookAt = (-target.position.x, -target.position.y, -target.position.z);
        // Rotate the camera every frame so it keeps looking at the target
        //transform.LookAt(target.position);
        //Vector3 direction = target;
        //Vector3 rotation = target - transform.position;
        //float rot = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rot);
        if(target != null) {
            // transform.LookAt(transform.position + target.transform.rotation * Vector3.left,
            // target.transform.rotation * Vector3.up);

            //transform.LookAt(target);
            transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        }

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        // transform.LookAt(target, Vector3.left);
    }
}
