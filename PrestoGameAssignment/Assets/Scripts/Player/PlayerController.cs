using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] LayerMask ignoreLayer;    
    [SerializeField] float draggingForce = 10;
    Vector3 currentMousePoz;
    Vector3 previousMousePoz;
    void Start()
    {
        
    }

    void Update()

    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(offset,Camera.main.transform.forward*1000,Color.red); 
            if (Physics.Raycast(offset, Camera.main.transform.forward, out hit, 1000f, ignoreLayer))
            {                
                Vector3 magnetPos = hit.transform.localPosition;
                previousMousePoz = new Vector3(offset.x, magnetPos.y, offset.z - Camera.main.transform.position.z);                
                
            }
        }
        
        

        if (Input.GetMouseButton(0))
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Physics.Raycast(temp, Camera.main.transform.forward, out hit, 1000f, ignoreLayer))
            {
                Vector3 magnetPos = hit.transform.localPosition;
                currentMousePoz = new Vector3(temp.x, magnetPos.y, temp.z - Camera.main.transform.position.z);
                if (Vector3.Distance(previousMousePoz, currentMousePoz) > 0.05f)
                {
                    Vector3 forceVector = currentMousePoz - previousMousePoz;
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceVector*draggingForce,ForceMode.Force);
                    hit.transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(hit.transform.gameObject.GetComponent<Rigidbody>().velocity, 0.7f);
                    Debug.Log(hit.transform.gameObject.GetComponent<Rigidbody>().velocity);
                }
                                
            }

        }
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
