using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject acladPrefab;

    public int spawnAmount = 5; // 5 aclads to spawn
    private int spawned = 0;

    public float spawnTime = 2.0f; // 2 seconds between each spawn

    
    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned >= spawnAmount)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if(elapsedTime > spawnTime )
        {
            elapsedTime = 0.0f;
            SpawnAclad();
            spawned++;
        }
    }

    void SpawnAclad()
    {
        GameObject aclad = Instantiate(acladPrefab, transform.position, transform.rotation);
        //aclad.GetComponent<AcladLogic>().SetDirection(transform.forward);
    }

}
