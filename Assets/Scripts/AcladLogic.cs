using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogic : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private Vector3 direction;
    // Start is called before the first frame update


    public float maxSuspensionLength;
    public float suspensionMultiplier;
    public float dampSenstivity;
    public float maxDamp;

    private float oldDist;


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
        transform.position += direction * speed * Time.deltaTime;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity)){
            if(hit.collider != null)
            {
                

                Vector3 suspensionForce = Mathf.Clamp(maxSuspensionLength - hit.distance, 0, maxSuspensionLength) * suspensionMultiplier * transform.up;

                suspensionForce += transform.up * Mathf.Clamp((oldDist - hit.distance) * dampSenstivity, 0, maxDamp);

                rb.AddForce(suspensionForce, ForceMode.Force);

                Debug.DrawRay(transform.position, Vector3.down * maxSuspensionLength, Color.red);
                Debug.DrawRay(transform.position, suspensionForce, Color.green);
                
                //rb.AddForceAtPosition((Mathf.Clamp(maxSuspensionLength - hit.distance, 0, maxSuspensionLength) * suspensionMultiplier * transform.up + transform.up * Mathf.Clamp((oldDist- hit.distance) * dampSenstivity, 0, maxDamp)) * Time.deltaTime, transform.position);
            }
            else
            {

            }
            oldDist = hit.distance;
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
            // Invert the direction of the Aclad
            SetDirection(direction * -1);
            // Rotate the Aclad 180 degrees
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

}
