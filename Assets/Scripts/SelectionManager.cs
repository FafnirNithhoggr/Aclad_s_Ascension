using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject reticle;
    public GameObject mainCamera;

    private Transform targetAclad; // The currently selected Aclad

    
    public static event Action<GameObject> OnAcladSelected;

    // Start is called before the first frame update
    void Start()
    {
        reticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if any aclad was selected using the mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Aclad"))
                {
                    SelectAclad(hit.collider.gameObject);
                }
            }
        }

        if(reticle.activeSelf && targetAclad != null) {
            Debug.Log(targetAclad.position);
            reticle.transform.position = targetAclad.position;
            //make the object face the camera at all times using its y axis
        
            reticle.transform.LookAt(mainCamera.transform);
        }

    }

    void SelectAclad(GameObject aclad) {
        reticle.SetActive(true);
        this.targetAclad = aclad.transform;

        OnAcladSelected?.Invoke(aclad);

    }


}
