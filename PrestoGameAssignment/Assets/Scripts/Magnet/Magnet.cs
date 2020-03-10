using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public enum Polarization { Negative, Positive };
    Magnet[] magnetList;
    Metal[] metalList;
    [SerializeField] int MagnetMagneticForce=1;
    public Polarization polar;
    Rigidbody rigidbody;
    Vector3 movement=new Vector3 (0,0,0);
    
    void Start(){
        /*
         Eng--Line1=> get Rigidbody
              Line2)> get tags by tag called magnet and exclude itself
             
         */
        rigidbody = GetComponent<Rigidbody>();
        // magnetList = GameObject.FindGameObjectsWithTag("Magnet").Where(x=>!x.Equals(this.gameObject)).ToArray();
        //metalList = GameObject.FindGameObjectsWithTag("Metal");
        magnetList = GameObject.FindObjectsOfType<Magnet>().Where(x => !x.Equals(this.gameObject.GetComponent<Magnet>())).ToArray();
        metalList = GameObject.FindObjectsOfType<Metal>();

    }

    // Update is called once per frame
    void Update()
    {
        magnetpolarisation();
        metalPolarisation();

    }
    float formula(float qFirst,float qSecond,float r)
    {        
       // Debug.Log(((qFirst * qSecond) / (4 * Mathf.PI * r * r)) * multiply);
        return ((qFirst*qSecond)/(4*Mathf.PI*r*r));
    }
    void magnetpolarisation()
    {
        for (int i = 0; i < magnetList.Length; i++)
        {

            if (magnetList[i].polar == this.polar)
            {

                Vector3 temp = (magnetList[i].transform.position - gameObject.transform.position).normalized * -1;
                movement = temp * formula(MagnetMagneticForce, MagnetMagneticForce, Vector3.Distance(magnetList[i].transform.position, gameObject.transform.position));
                rigidbody.AddForce(movement);
            }
            else
            {

                Vector3 temp = (magnetList[i].transform.position - gameObject.transform.position).normalized;
                movement = temp * formula(MagnetMagneticForce, MagnetMagneticForce, Vector3.Distance(magnetList[i].transform.position, gameObject.transform.position)); Debug.Log(Vector3.Distance(magnetList[i].transform.position, gameObject.transform.position) + "diff");
                rigidbody.AddForce(movement);
            }
        }

        if (this.polar == Polarization.Negative)
            GetComponent<MeshRenderer>().material.color = Color.blue;
        else
            GetComponent<MeshRenderer>().material.color = Color.red;
    }
    void metalPolarisation()
    {
        for (int i = 0; i < metalList.Length; i++)
        {
            int tempForce = metalList[i].Force;
            Vector3 temp = (metalList[i].transform.position - gameObject.transform.position).normalized;
            movement = temp * formula(tempForce, tempForce, Vector3.Distance(metalList[i].transform.position, gameObject.transform.position));
            
            rigidbody.AddForce(movement);
        }
    }
}
