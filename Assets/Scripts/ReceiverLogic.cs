using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverLogic : MonoBehaviour
{
    public int acladsNeeded = 3;
    private int acladsEntered = 0;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Aclad")) {
            // Count the number of Aclads that have entered the trigger
            acladsEntered++;
            Destroy(other.gameObject);
            if (acladsEntered == acladsNeeded) {
                // Do something when all Aclads have entered the trigger
                Debug.Log("All Aclads have entered the trigger");

                // Find the main camera and run the WonGame method
                GameObject mainCamera = GameObject.Find("Main Camera");
                mainCamera.GetComponent<CameraController>().WonGame();
            }
        }
    }

    public int GetAcladsEntered() {
        return acladsEntered;
    }

    public int GetAcladsNeeded() {
        return acladsNeeded;
    }
}
