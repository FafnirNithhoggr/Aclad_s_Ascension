using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicRight : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Aclad")) {
            
            Transform otherTransform = other.gameObject.GetComponent<Transform>();
            otherTransform.Rotate(0, 90, 0);

            AcladLogic acladLogic = other.gameObject.GetComponent<AcladLogic>();
            acladLogic.SetDirection(otherTransform.forward);
        }
    }
}
