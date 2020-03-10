using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    Rigidbody rigidbody;
    Magnet[] magnetList;
    Vector3 movement;
    public int Force=1;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //magnetList = GameObject.FindGameObjectsWithTag("Draggable");
        magnetList = GameObject.FindObjectsOfType<Magnet>();
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < magnetList.Length; i++)
        {
                Vector3 temp = (magnetList[i].transform.position - gameObject.transform.position).normalized ;
                movement = temp * formula(Force, Force, Vector3.Distance(magnetList[i].transform.position, gameObject.transform.position));
                rigidbody.AddForce(movement);
         
            
        }
    }
    float formula(float qFirst, float qSecond, float r)
    {
        // Debug.Log(((qFirst * qSecond) / (4 * Mathf.PI * r * r)) * multiply);
        return ((qFirst * qSecond) / (4 * Mathf.PI * r * r));
    }
}
