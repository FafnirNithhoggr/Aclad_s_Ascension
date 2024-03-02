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
    private float currentDistance;
    private bool reachedTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        // Send a raycast to the target
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, maxLength)) {
            target = hit.collider.gameObject;
            hitPoint = hit.point;
            distanceToTarget = hit.distance;
        } else {
            target = null;
            hitPoint = laserOrigin.position + laserOrigin.forward * maxLength;
            distanceToTarget = maxLength;
        }
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
            if (target != null) {
                reachedTarget = true;
                Instantiate(explosionPrefab, hitPoint + target.transform.up * 0.1f, Quaternion.identity);
                Destroy(target);
            }
        } else {
            lineRenderer.SetPosition(0, laserOrigin.position);
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * currentDistance);
        }
        
    }
}
