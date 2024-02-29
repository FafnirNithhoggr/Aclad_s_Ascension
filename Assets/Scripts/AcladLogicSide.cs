using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicSide : MonoBehaviour
{

    public bool isRight;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Aclad")) {
            
            Transform otherTransform = other.gameObject.GetComponent<Transform>();

            if(isRight){
                otherTransform.LookAt(otherTransform.position + (-transform.right)); //-tranform.right is actually the right direction
            }else{
                otherTransform.LookAt(otherTransform.position + transform.right);
            }

            AcladLogic acladLogic = other.gameObject.GetComponent<AcladLogic>();
            
            if(isRight){
                acladLogic.SetDirection(-transform.right);
            }else{
                acladLogic.SetDirection(transform.right);
            }
        }
    }
}
