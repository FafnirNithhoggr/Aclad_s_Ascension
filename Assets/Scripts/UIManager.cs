using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject selectedAclad;

    public GameObject acladUp; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U)) {
            //create an instance of the acladUp prefab on the same position as the selectedAclad
            Instantiate(acladUp, selectedAclad.transform.position, Quaternion.identity);
            //destroy the selectedAclad
            Destroy(selectedAclad);
            
        }
    }

    private void OnEnable()
    {
        SelectionManager.OnAcladSelected += HandleAcladSelected;
    }

    private void OnDisable()
    {
        SelectionManager.OnAcladSelected -= HandleAcladSelected;
    }

    void HandleAcladSelected(GameObject selectedAclad)
    {
        // Update UI based on the selected NPC
        // For example, display available operations or information about the NPC
    }
}
