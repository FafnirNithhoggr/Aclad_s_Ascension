using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float transitionMoveSpeed;
    [SerializeField] private float transitionRotationSpeed;
    [SerializeField] private float mouseRotationSpeed;
    [SerializeField] private float lowerBound;
    [SerializeField] private float upperBound;
    [SerializeField] private float timeToWait;
    [SerializeField] private float xBoundary;
    [SerializeField] private float zBoundary;
    public GameObject Spawner;
    public GameObject Receiver;
    private Vector3 mainCameraPosition;
    private Quaternion mainCameraRotation;
    private Transform targetAclad; // The currently selected Aclad
    private Vector3 offsetToAclad;
    private float elapsedTime = 0.0f;

    private enum CameraState {
        Begin,
        InitialTransition,
        Main,
        MainToThird,
        ThirdPerson,
        ThirdToThird,
        ThirdToMain,
        GameWonTransition,
        GameWon
    }

    private CameraState cameraState;

    private void Awake() {
        // Subscribe to the events
        SelectionManager.OnAcladSelected += HandleAcladSelected;
        SelectionManager.OnAcladDeselected += HandleAcladDeselected;
    }

    void Start()
    {
        mainCameraPosition = transform.position;
        mainCameraRotation = transform.rotation;
        cameraState = CameraState.Begin;
        transform.position = Spawner.transform.position + new Vector3(-0.1f, 0.9f, 0.6f);
        transform.rotation = Spawner.transform.rotation;
    }

    void Update()
    {   // Maybe we can use a switch statement here
        if (cameraState == CameraState.Begin) {
            HandleBegin();
        } else if (cameraState == CameraState.InitialTransition) {
            HandleInitialTransition();
        } else if (cameraState == CameraState.Main) {
            HandleMain();
        } else if (cameraState == CameraState.MainToThird) {
            HandleMainToThird();
        } else if (cameraState == CameraState.ThirdPerson) {
            HandleThirdPerson();
        } else if (cameraState == CameraState.ThirdToThird) {
            HandleThirdToThirdPerson();
        } else if (cameraState == CameraState.ThirdToMain) {
            HandleThirdToMain();
        } else if (cameraState == CameraState.GameWonTransition) {
            HandleGameWonTransition();
        }

    }

    private void HandleBegin() {
        // Just wait for a few seconds before starting the initial transition
        if (Time.time > timeToWait) {
            cameraState = CameraState.InitialTransition;
        }
    }

    private void HandleInitialTransition() {
        // Make the camera move slow in the beginning, then speed up, and then slow down again
        elapsedTime += Time.deltaTime;
        float factor = Mathf.SmoothStep(0, 1, elapsedTime / 2.0f);
        transform.position = Vector3.Lerp(transform.position, mainCameraPosition, transitionMoveSpeed * factor);
        transform.rotation = Quaternion.Lerp(transform.rotation, mainCameraRotation, transitionRotationSpeed * factor);
        if (Vector3.Distance(transform.position, mainCameraPosition) < 0.05f && Quaternion.Angle(transform.rotation, mainCameraRotation) < 1.0f) {
            cameraState = CameraState.Main;
            elapsedTime = 0.0f;
        }
    }

    private void HandleMain() {
        // Read keys wsad to move the camera in the global xz plane, but in the direction of the camera
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        // Project the camera forward vector to the xz plane
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        Vector3 deltaVertical = forward * vertical * moveSpeed * Time.deltaTime;
        Vector3 deltaHorizontal = transform.right * horizontal * moveSpeed * Time.deltaTime;
        Vector3 newPositionXY = transform.position + deltaVertical + deltaHorizontal;
        if (IsInBounds(newPositionXY)) {
            transform.position = newPositionXY;
        }

        // Scroll to zoom in and out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) {
            Vector3 newPosition = transform.position + transform.forward * scroll * 3.0f;
            if (newPosition.y > lowerBound && newPosition.y < upperBound && IsInBounds(newPosition)) {
                transform.position = newPosition;
            }
        }

        // The camera should rotate on the global y axis when the mouse is at the edge of the screen
        if (Input.mousePosition.x <= 0) {
            transform.Rotate(Vector3.up, -mouseRotationSpeed * Time.deltaTime, Space.World);
        } else if (Input.mousePosition.x >= Screen.width - 1) {
            transform.Rotate(Vector3.up, mouseRotationSpeed * Time.deltaTime, Space.World);
        }

        // The camera should rotate on the local x axis when the mouse is at the edge of the screen
        // if (Input.mousePosition.y <= 0) {
        //     transform.Rotate(transform.right, mouseRotationSpeed * Time.deltaTime, Space.World);
        // } else if (Input.mousePosition.y >= Screen.height - 1) {
        //     transform.Rotate(transform.right, -mouseRotationSpeed * Time.deltaTime, Space.World);
        // }

        if (Input.GetKeyDown(KeyCode.V) && targetAclad != null) {
            cameraState = CameraState.MainToThird;
            mainCameraRotation = transform.rotation;
            mainCameraPosition = transform.position;
            elapsedTime = 0.0f;
        }
        // Move the camera up and down using the q and e keys
        if (Input.GetKey(KeyCode.Q)) {
            Vector3 targetPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
            if (targetPosition.y < upperBound) {
                transform.position = targetPosition;
            }
        }

        if (Input.GetKey(KeyCode.E)) {
            Vector3 targetPosition = transform.position - Vector3.up * moveSpeed * Time.deltaTime;
            if (targetPosition.y > lowerBound) {
                transform.position = targetPosition;
            }
        }
    }

    private void HandleMainToThird() {
        if (targetAclad == null) {
            cameraState = CameraState.Main;
            elapsedTime = 0.0f;
            return;
        }
        elapsedTime += Time.deltaTime;
        float factor = Mathf.SmoothStep(0, 1, elapsedTime / 2.0f);
        Vector3 targetPosition = targetAclad.position - targetAclad.forward * 2.0f + Vector3.up * 1.0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetAclad.position - targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionMoveSpeed * factor);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionRotationSpeed * factor);
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f && Quaternion.Angle(transform.rotation, targetRotation) < 1.0f) {
            cameraState = CameraState.ThirdPerson;
            offsetToAclad = transform.position - targetAclad.position;
            elapsedTime = 0.0f;
        }
    }

    private void HandleThirdPerson() {
        if (Input.GetKeyDown(KeyCode.V) || targetAclad == null) {
            cameraState = CameraState.ThirdToMain;
            elapsedTime = 0.0f;
            return;
        }

        // Read keys wsad to move the camera around the aclad using the offset
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        // Calculate the new offset using the direction and the offsetToAclad
        offsetToAclad = Quaternion.AngleAxis(-horizontal * rotationSpeed * Time.deltaTime, Vector3.up) * offsetToAclad;
        offsetToAclad = Quaternion.AngleAxis(vertical * rotationSpeed * Time.deltaTime, transform.right) * offsetToAclad;
        transform.position = targetAclad.position + offsetToAclad;

        // Make the camera look at the targetAclad
        transform.LookAt(targetAclad);
    }

    private void HandleThirdToThirdPerson() {
        if (targetAclad == null) {
            cameraState = CameraState.Main;
            elapsedTime = 0.0f;
            return;
        }
        elapsedTime += Time.deltaTime;
        float factor = Mathf.SmoothStep(0, 1, elapsedTime / 2.0f);
        Vector3 targetPosition = targetAclad.position + offsetToAclad;
        Quaternion targetRotation = Quaternion.LookRotation(targetAclad.position - targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionMoveSpeed * factor);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionRotationSpeed * factor);
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f && Quaternion.Angle(transform.rotation, targetRotation) < 1.0f) {
            cameraState = CameraState.ThirdPerson;
            elapsedTime = 0.0f;
        }
    }

    private void HandleThirdToMain() {
        elapsedTime += Time.deltaTime;
        float factor = Mathf.SmoothStep(0, 1, elapsedTime / 2.0f);
        transform.position = Vector3.Lerp(transform.position, mainCameraPosition, transitionMoveSpeed * factor);
        transform.rotation = Quaternion.Lerp(transform.rotation, mainCameraRotation, transitionRotationSpeed * factor);
        if (Vector3.Distance(transform.position, mainCameraPosition) < 0.1f && Quaternion.Angle(transform.rotation, mainCameraRotation) < 1.0f) {
            cameraState = CameraState.Main;
            elapsedTime = 0.0f;
        }
    }

    private void HandleGameWonTransition() {
        elapsedTime += Time.deltaTime;
        float factor = Mathf.SmoothStep(0, 1, elapsedTime / 2.0f);
        Vector3 targetPosition = Receiver.transform.position + new Vector3(0, 1, 0);
        // The target rotation is in the direction of the receiver
        Quaternion targetRotation = Quaternion.LookRotation(-Receiver.transform.forward);
        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionMoveSpeed * factor);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionRotationSpeed * factor);
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f && Quaternion.Angle(transform.rotation, targetRotation) < 1.0f) {
            cameraState = CameraState.GameWon;
            GameObject.Find("WinCanvas").GetComponent<Canvas>().enabled = true;
            elapsedTime = 0.0f;
        }
    }
    

    public void WonGame() {
        cameraState = CameraState.GameWonTransition;
    }

    private void HandleAcladSelected(GameObject aclad) {
        if (this.targetAclad != null && this.targetAclad != aclad.transform && cameraState == CameraState.ThirdPerson) {
            cameraState = CameraState.ThirdToThird;
            elapsedTime = 0.0f;
        }
        this.targetAclad = aclad.transform;
    }

    private void HandleAcladDeselected() {
        this.targetAclad = null;
    }

    private bool IsInBounds(Vector3 position) {
        return position.x > -xBoundary && position.x < xBoundary && position.z > -zBoundary && position.z < zBoundary;
    }
}
