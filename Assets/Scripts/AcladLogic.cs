using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogic : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Slope") {
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Slope") {
            rb.useGravity = true;
        }
    }

    public void SetDirection(Vector3 newDirection) {
        direction = newDirection;
    }


}