using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicSide : MonoBehaviour
{

    public bool isLeft = false;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Aclad")) {
            
            Transform otherTransform = other.gameObject.GetComponent<Transform>();
            if (isLeft){
                otherTransform.Rotate(0, -90, 0);
            }else{
                otherTransform.Rotate(0, 90, 0);
            }
            AcladLogic acladLogic = other.gameObject.GetComponent<AcladLogic>();
            acladLogic.SetDirection(otherTransform.forward);
        }
    }
}
