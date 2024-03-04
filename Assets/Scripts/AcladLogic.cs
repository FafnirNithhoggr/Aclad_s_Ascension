using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogic : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float maxSuspensionLength;
    public float suspensionMultiplier;
    public float dampSenstivity;
    public float maxDamp;
    public float timeOffset;
    private float oldDist;
    private Vector3 direction;

    private void Awake()
    {
        oldDist = maxSuspensionLength;
    }

    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity)){
            if(hit.collider != null)
            {
                Vector3 suspensionForce = Mathf.Clamp(maxSuspensionLength - hit.distance, 0, maxSuspensionLength) * suspensionMultiplier * transform.up * Time.deltaTime * timeOffset;

                suspensionForce += transform.up * Mathf.Clamp((oldDist - hit.distance) * dampSenstivity, 0, maxDamp) * Time.deltaTime * timeOffset;

                rb.AddForce(suspensionForce, ForceMode.Force);

                Debug.DrawRay(transform.position, Vector3.down * maxSuspensionLength, Color.red);
                Debug.DrawRay(transform.position, suspensionForce, Color.green);

                Vector3 forwardMovement = speed * direction * Mathf.Clamp01(Vector3.Dot(hit.normal, Vector3.up));
                transform.position += direction * speed * Mathf.Clamp01(Vector3.Dot(hit.normal, Vector3.up)/2) * Time.deltaTime;

                Debug.DrawRay(transform.position, rb.velocity, Color.green);

            }
            oldDist = hit.distance;
        
        } else {
            transform.position += direction * speed * Time.deltaTime;
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 newDirection) {
        direction = newDirection;
    }

    public Vector3 GetDirection() {
        return direction;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Aclad")) {
            Debug.DrawRay(transform.position, other.GetContact(0).normal, Color.red);
            // Invert the direction of the Aclad
            SetDirection(direction * -1);
            // Rotate the Aclad 180 degrees
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

}
