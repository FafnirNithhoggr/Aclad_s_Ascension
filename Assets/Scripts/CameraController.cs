using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 focusPoint;
    public float lowerBound;
    public float upperBound;
    public float transitionSpeed = 5f; // Adjust the transition speed as needed
    private Vector3 originalPosition ;
    private Quaternion originalRotation;
    private Vector3 originalFocusPoint;
    private Transform targetAclad; // The currently selected Aclad
    private enum CameraState {
        Main,
        ThirdPerson,
        TransitionToFirst
    }

    private CameraState cameraState;

    void Start()
    {
        // Save the original position and rotation and make sure it is a copy
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        cameraState = CameraState.Main;
    }

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
                    // Set the selected Aclad
                    targetAclad = hit.collider.gameObject.transform;
                    // Start the camera transition
                    StartCameraThirdPerson();
                }
            }
        }

        // If it's esc, then exit the third person view
        if (Input.GetKey(KeyCode.Escape)) {
            EndCameraThirdPerson();
        }

        if (cameraState != CameraState.Main) {return;}

        // Zoom in and out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * 10);

        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(focusPoint, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(focusPoint, Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) && focusPoint.y < upperBound) {
            transform.Translate(0, 0.1f, 0);
            focusPoint.y += 0.1f;
        }

        if (Input.GetKey(KeyCode.S) && focusPoint.y > lowerBound) {
            transform.Translate(0, -0.1f, 0);
            focusPoint.y -= 0.1f;
        }
    }

    private void LateUpdate() {
        // Check if the camera is currently in third person

        if (cameraState == CameraState.ThirdPerson) {
            Vector3 desiredPosition = targetAclad.position - (targetAclad.forward*2) + Vector3.up; // Adjust as needed
            Quaternion targetRotation = Quaternion.LookRotation(targetAclad.position - transform.position);

            // Smooth the transition
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
        } else if (cameraState == CameraState.TransitionToFirst) {
            // Smooth the transition back to the original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * transitionSpeed);

            // Check if the transition is complete
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f && Quaternion.Angle(transform.rotation, originalRotation) < 1.0f) {
                cameraState = CameraState.Main;
            }
        }
    }

    void StartCameraThirdPerson()
    {   
        // If the camera is already in third person, then it shouldn't overwrite the original position
        if (cameraState == CameraState.ThirdPerson) {return;}
        // Save the original
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalFocusPoint = focusPoint;
        cameraState = CameraState.ThirdPerson;
    }

    void EndCameraThirdPerson()
    {
        cameraState = CameraState.TransitionToFirst;
        focusPoint = originalFocusPoint;
    }

}
