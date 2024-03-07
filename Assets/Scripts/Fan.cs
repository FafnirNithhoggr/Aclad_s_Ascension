using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public float windForce;

    private float forceDivisions = 5f;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Aclad")) {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * windForce, ForceMode.Impulse);
            rb.AddForce(transform.forward * windForce, ForceMode.Force);
            Debug.DrawRay(transform.position, transform.forward * windForce, Color.black);
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Exited");
        if (other.gameObject.CompareTag("Aclad")) {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            //rb.AddForce(-transform.forward * windForce, ForceMode.Force);
            StartCoroutine(ApplyOppositeForce(rb));
        }
    }

    private IEnumerator ApplyOppositeForce(Rigidbody rb) {

        float timesApplied = 0f;

        while (timesApplied < forceDivisions) {
            rb.AddForce(-transform.forward * windForce / forceDivisions, ForceMode.Force);
            Debug.DrawRay(transform.position, -transform.forward * windForce, Color.black);
            timesApplied++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
