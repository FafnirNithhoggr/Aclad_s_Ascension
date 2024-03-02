using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float maxLength;
    private GameObject target;
    private Vector3 hitPoint;
    private float distanceToTarget;
    private float currentDistance;

    // Start is called before the first frame update
    void Start()
    {
        // Send a raycast to the target
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength)) {
            target = hit.collider.gameObject;
            hitPoint = hit.point;
            distanceToTarget = hit.distance;
        } else {
            target = null;
            hitPoint = transform.position + transform.forward * maxLength;
            distanceToTarget = maxLength;
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Move the laser to the target
        if (lineRenderer != null && lineRenderer.enabled) {
            currentDistance += laserSpeed * Time.fixedDeltaTime;
            if (currentDistance >= distanceToTarget) {
                //Deactivate();
                if (target != null) {
                    Destroy(target);
                }
            } else {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * currentDistance);
            }
        }
    }
}
