using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverLogic : MonoBehaviour
{
    public int acladsNeeded = 3;
    private int acladsEntered = 0;
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
            // Count the number of Aclads that have entered the trigger
            acladsEntered++;
            Destroy(other.gameObject);
            if (acladsEntered == acladsNeeded) {
                // Do something when all Aclads have entered the trigger
                Debug.Log("All Aclads have entered the trigger");
            }
        }
    }
}
