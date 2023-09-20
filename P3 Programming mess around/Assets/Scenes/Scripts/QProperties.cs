using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QProperties : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    public LayerMask clickableThings;
    public int projectileSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DestroySelf());
        
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
        {
            print("I'm doing something!");
            Vector3 mousePlayerVec = new Vector3(hitInfo.point.x, hitInfo.point.y + player.transform.position.y, hitInfo.point.z) - player.transform.position;
            rb.velocity = mousePlayerVec.normalized * projectileSpeed; //Lav vektor regning mellem player og mus :)
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
