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

    public void SetDirection(Vector3 newDirection) {
        direction = newDirection;
    }

    public Vector3 GetDirection() {
        return direction;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Aclad")) {
            // Invert the direction of the Aclad
            SetDirection(direction * -1);
            // Rotate the Aclad 180 degrees
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

}
