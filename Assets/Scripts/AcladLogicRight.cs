using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicRight : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Aclad")) {
            
            Vector3 direction = other.gameObject.GetComponent<AcladLogic>().direction;
            direction = Quaternion.Euler(0, 90, 0) * direction;
        }
    }
}
