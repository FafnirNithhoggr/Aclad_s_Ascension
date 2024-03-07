using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{
    public float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the fan blades
        transform.Rotate(-spinSpeed * Time.deltaTime, 0, 0);
    }
}
