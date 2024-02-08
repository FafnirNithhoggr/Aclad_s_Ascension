using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicUp : MonoBehaviour
{
    public float upForce = 5f;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Aclad")) {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        }
    }
}
