using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Aclad")) {
            Vector3 direction = other.gameObject.GetComponent<AcladLogic>().GetDirection();

            // Check if the Aclad is moving up the ramp and if so, apply a force to help it move up
            if (Vector3.Dot(direction, transform.forward) < 0) {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 force = ((transform.forward*-1) + transform.up) * 3;
                rb.AddForce(force, ForceMode.Acceleration);
            }
        }
        
    }*/

}
