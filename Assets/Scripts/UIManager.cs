using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject selectedAclad;

    public GameObject acladUp;
    public GameObject acladRight;

    public GameObject acladLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U) && selectedAclad != null) {
            //create an instance of the acladUp prefab on the same position as the selectedAclad
            Instantiate(acladUp, selectedAclad.transform.position, selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            //destroy the selectedAclad
            Destroy(selectedAclad);
        }

        if (Input.GetKey(KeyCode.R) && selectedAclad != null) {
            //create an instance of the acladRight prefab on the same position as the selectedAclad
            Instantiate(acladRight, selectedAclad.transform.position + new Vector3(0, 0.5f, 0), selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            //destroy the selectedAclad
            Destroy(selectedAclad);
        }

        if (Input.GetKey(KeyCode.L) && selectedAclad != null) {
            //create an instance of the acladLeft prefab on the same position as the selectedAclad
            Instantiate(acladLeft, selectedAclad.transform.position + new Vector3(0, 0.5f, 0), selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
            //destroy the selectedAclad
            Destroy(selectedAclad);
        }


    }

    private void OnEnable()
    {
        SelectionManager.OnAcladSelected += HandleAcladSelected;
        SelectionManager.OnAcladDeselected += HandleAcladDeselected;
    }

    private void OnDisable()
    {
        SelectionManager.OnAcladSelected -= HandleAcladSelected;
        SelectionManager.OnAcladDeselected -= HandleAcladDeselected;
    }

    void HandleAcladSelected(GameObject selectedAclad)
    {
        this.selectedAclad = selectedAclad;
    }

    void HandleAcladDeselected()
    {
        this.selectedAclad = null;
    }
}
