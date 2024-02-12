using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 focusPoint;

    public float lowerBound;

    public float upperBound;
    public float rotationSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -1 * rotationSpeed * Time.deltaTime);
        }

        // Move the camera up and down with the W and S keys
        if (Input.GetKey(KeyCode.W) && focusPoint.y < upperBound)
        {
            transform.Translate(0, 0.1f, 0);
            focusPoint.y += 0.1f;
        }

        // Don't move the camera focus point below the ground
        if (Input.GetKey(KeyCode.S) && focusPoint.y > lowerBound)
        {
            transform.Translate(0, -0.1f, 0);
            focusPoint.y -= 0.1f;
        }

        // Zoom in and out with the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * 10);
        
    }
}
