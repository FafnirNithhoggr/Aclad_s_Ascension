using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectionManager;
    private GameObject selectedAclad;
    public GameObject[] acladsUp;
    public int acladUpQuantity;
    public GameObject[] acladsRight;
    public int acladRightQuantity;
    public GameObject[] acladsLeft;
    public int acladLeftQuantity;
    public GameObject[] acladsLaser;
    public int acladLaserQuantity;

    // Make a struct with the aclad, time and position
    public struct AcladData
    {
        public GameObject aclad;
        public float timeCount;
        public Vector3 initialPosition;
        public Quaternion initialRotation;
        public Vector3 finalPosition;
        public Quaternion finalRotation;
        public int acladType;
    }
    private List<AcladData> acladsData = new List<AcladData>();

    void Start()
    {
        int selectedAcladModel = PlayerPrefs.GetInt("SelectedAclad", 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U) && selectedAclad != null && acladUpQuantity > 0) {
            acladUpQuantity--;
            selectionManager.GetComponent<SelectionManager>().reticle.SetActive(false);
            acladsData.Add(CreateAcladData(selectedAclad, selectedAclad.transform.position, selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f), 0));
        }

        if (Input.GetKey(KeyCode.R) && selectedAclad != null && acladRightQuantity > 0) {
            acladRightQuantity--;
            selectionManager.GetComponent<SelectionManager>().reticle.SetActive(false);
            acladsData.Add(CreateAcladData(selectedAclad, selectedAclad.transform.position + new Vector3(0, 0.5f, 0), selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f), 1));
        }

        if (Input.GetKey(KeyCode.L) && selectedAclad != null && acladLeftQuantity > 0) {
            acladLeftQuantity--;
            selectionManager.GetComponent<SelectionManager>().reticle.SetActive(false);
            acladsData.Add(CreateAcladData(selectedAclad, selectedAclad.transform.position + new Vector3(0, 0.5f, 0), selectedAclad.transform.rotation * Quaternion.Euler(0f, 180f, 0f), 2));
        }

        if (Input.GetKey(KeyCode.B) && selectedAclad != null && acladLaserQuantity > 0) {
            acladLaserQuantity--;
            selectionManager.GetComponent<SelectionManager>().reticle.SetActive(false);
            acladsData.Add(CreateAcladData(selectedAclad, selectedAclad.transform.position + new Vector3(0, 0.3f, 0), selectedAclad.transform.rotation, 3));
        }

        // Update each aclad
        for (int i = 0; i < acladsData.Count; i++) {
            AcladData acladData = acladsData[i];
            if (acladData.aclad == null) {
                acladsData.RemoveAt(i);
                continue;
            }
            acladData.timeCount += Time.deltaTime;
            acladData.aclad.transform.position = Vector3.Lerp(acladData.initialPosition, acladData.finalPosition, acladData.timeCount);
            acladData.aclad.transform.rotation = Quaternion.Slerp(acladData.initialRotation, acladData.finalRotation, acladData.timeCount);
            if (acladData.timeCount > 1.0f) {
                if (acladData.acladType == 0) {
                    //create an instance of the acladUp prefab on the same position as the selectedAclad
                    int selectedAcladModel = PlayerPrefs.GetInt("SelectedAclad", 0);
                    Instantiate(acladsUp[selectedAcladModel], acladData.finalPosition, acladData.finalRotation);
                } else if (acladData.acladType == 1) {
                    //create an instance of the acladRight prefab on the same position as the selectedAclad
                    int selectedAcladModel = PlayerPrefs.GetInt("SelectedAclad", 0);
                    Instantiate(acladsRight[selectedAcladModel], acladData.finalPosition, acladData.finalRotation);
                } else if (acladData.acladType == 2) {
                    //create an instance of the acladLeft prefab on the same position as the selectedAclad
                    int selectedAcladModel = PlayerPrefs.GetInt("SelectedAclad", 0);
                    Instantiate(acladsLeft[selectedAcladModel], acladData.finalPosition, acladData.finalRotation);
                } else if (acladData.acladType == 3) {
                    //create an instance of the acladLaser prefab on the same position as the selectedAclad
                    int selectedAcladModel = PlayerPrefs.GetInt("SelectedAclad", 0);
                    Instantiate(acladsLaser[selectedAcladModel], acladData.finalPosition, acladData.finalRotation);
                }
                //destroy the selectedAclad
                Destroy(acladData.aclad);
                // Remove the aclad from the list
                acladsData.RemoveAt(i);
            } else {
                acladsData[i] = acladData;
            }
        }

    }

    private AcladData CreateAcladData(GameObject aclad, Vector3 finalPosition, Quaternion finalRotation, int acladType)
    {
        AcladData acladData = new AcladData();
        acladData.aclad = aclad;
        acladData.timeCount = 0.0f;
        acladData.initialPosition = aclad.transform.position;
        acladData.initialRotation = aclad.transform.rotation;
        acladData.finalPosition = finalPosition;
        acladData.finalRotation = finalRotation;
        acladData.acladType = acladType;
        return acladData;
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
