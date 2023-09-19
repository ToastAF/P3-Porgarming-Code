using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QProperties : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask clickableThings;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroySelf());
        
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
        {
            print("I'm doing something!");
            rb.velocity = hitInfo.point; //Lav vektor regning mellem player og mus :)
        }  
    }

    void Update()
    {

            


    }

    IEnumerator DestroySelf()
    {
        //print("I'm alive!");
        yield return new WaitForSeconds(2);

        //print("I'm gone!");
        Destroy(gameObject);
    }
}
