using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogicLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserSpeed;
    [SerializeField] private float maxLength;
    [SerializeField] private Transform laserOrigin;
    [SerializeField] private GameObject explosionPrefab;
    private GameObject target;
    private Vector3 hitPoint;
    private float distanceToTarget;
    private float currentDistance = 0.0f;
    private bool reachedTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        // Send a raycast to the target
        RaycastHit[] hits = Physics.RaycastAll(laserOrigin.position, laserOrigin.forward, maxLength);
        foreach (RaycastHit h in hits) {
            // Get the grandparent of the collider
            if (h.collider.transform.parent != null && h.collider.transform.parent.parent != null) {
                if (h.collider.transform.parent.parent.gameObject.tag == "Terrain") {
                    target = h.collider.transform.parent.parent.gameObject;
                    hitPoint = h.point;
                    distanceToTarget = h.distance;
                    return;
                }
            }
        }

        // If no target is found, set the hit point to the end of the laser
        target = null;
        hitPoint = laserOrigin.position + laserOrigin.forward * maxLength;
        distanceToTarget = maxLength;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // If the line renderer is not enabled, return
        if (lineRenderer == null || !lineRenderer.enabled) {
            return;
        }

        // If the target is reached, move the laser forward
        if (reachedTarget) {
            currentDistance -= laserSpeed * Time.fixedDeltaTime;
            if (currentDistance <= 0) {
                lineRenderer.enabled = false;
                return;
            }
            lineRenderer.SetPosition(0, hitPoint - laserOrigin.forward * currentDistance);
            lineRenderer.SetPosition(1, hitPoint);
            return;
        }
        
        // If the target is not reached, move the laser to the target
        currentDistance += laserSpeed * Time.fixedDeltaTime;
        if (currentDistance >= distanceToTarget) {
            reachedTarget = true;
            if (target != null) {
                Instantiate(explosionPrefab, hitPoint + target.transform.up * 0.1f, Quaternion.identity);
                Destroy(target);
            }
        } else {
            lineRenderer.SetPosition(0, laserOrigin.position);
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * currentDistance);
        }
        
    }
}
