using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QProperties : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    PlayerMove playerScript;
    public LayerMask clickableThings;
    public int projectileSpeed;

    Vector3 playerStartPos;
    Vector3 mousePlayerVec;
    float tempRange;
    public float setRange;

    public float physDmg;
    public float magDmg;

    public GameObject hitParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMove>();
        playerStartPos = player.transform.position;
        //StartCoroutine(DestroySelf());

        physDmg = playerScript.attackDamage * 0.5f;
        magDmg = playerScript.abilityPower * 1.5f;
        
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
        {
            //print("I'm doing something!");
            mousePlayerVec = new Vector3(hitInfo.point.x, hitInfo.point.y + player.transform.position.y, hitInfo.point.z) - player.transform.position;
            transform.LookAt(hitInfo.point);
            rb.velocity = mousePlayerVec.normalized * projectileSpeed; //Lav vektor regning mellem player og mus :)
        }  
    }

    void Update()
    {
        tempRange = (transform.position - playerStartPos).magnitude;
        if(tempRange > mousePlayerVec.normalized.magnitude*setRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelf()
    {
        //print("I'm alive!");
        yield return new WaitForSeconds(2);

        //print("I'm gone!");
        Destroy(gameObject);
    }
}
