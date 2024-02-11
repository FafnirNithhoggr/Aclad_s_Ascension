using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChangeDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Aclad")) {
            AcladLogic acladLogic = other.gameObject.GetComponent<AcladLogic>();
            Vector3 direction = acladLogic.GetDirection();
            // Invert the direction
            direction *= -1;
            acladLogic.SetDirection(direction);

            // Rotate the Aclad 180 degrees
            other.gameObject.transform.Rotate(0, 180, 0);
        }
    }
}
