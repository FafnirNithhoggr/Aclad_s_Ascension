using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogic : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private Vector3 direction = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Slope") {
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Exited");
        if (other.gameObject.tag == "Slope") {
            rb.useGravity = true;
        }
    }
}
