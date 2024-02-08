using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcladLogic : MonoBehaviour
{

    public float speed;

    private Vector3 direction = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}
