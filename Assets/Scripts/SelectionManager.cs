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
    public static event Action OnAcladDeselected;

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
                    HandleAcladSelected(hit.collider.gameObject);
                }
            }
        }

        if(reticle.activeSelf && targetAclad != null) {
            reticle.transform.position = targetAclad.position + new Vector3(0, 0.08f, 0);
            //make the object face the camera at all times using its y axis
        
            reticle.transform.LookAt(mainCamera.transform);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || (targetAclad == null && reticle.activeSelf)) {
            HandleAcladDeselected();
        }

    }

    void HandleAcladSelected(GameObject aclad) {
        reticle.SetActive(true);
        this.targetAclad = aclad.transform;

        OnAcladSelected?.Invoke(aclad);
    }

    void HandleAcladDeselected() {
        reticle.SetActive(false);
        this.targetAclad = null;

        OnAcladDeselected?.Invoke();
    }


}
