using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public float windForce = 5f;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Aclad")) {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * windForce, ForceMode.Impulse);
        }
    }
}
