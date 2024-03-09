using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcladsSelect : MonoBehaviour
{

    public float rotateSpeed = 10.0f;
    public float oscillateSpeed = 1.0f;
    public float oscillateHeight = 0.5f;


    public GameObject[] aclads;
    public int selectedAclad = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * oscillateSpeed) * oscillateHeight, transform.position.z);

    }


    public void NextAclad()
    {
        aclads[selectedAclad].SetActive(false);
        selectedAclad = (selectedAclad + 1) % aclads.Length;
        aclads[selectedAclad].SetActive(true);
    }

    public void PreviousAclad()
    {
        aclads[selectedAclad].SetActive(false);
        selectedAclad--;
        if (selectedAclad < 0)
        {
            selectedAclad += aclads.Length;
        }
        aclads[selectedAclad].SetActive(true);
    }

    public void GoLevelSelect()
    {
        PlayerPrefs.SetInt("SelectedAclad", selectedAclad);
        SceneManager.LoadScene("LevelSelection");
    }
}
