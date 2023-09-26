using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public float maxHealth, maxMana, manaRegen;
    public float currentHealth, currentMana;
    bool manaRegenReady = true;

    public LayerMask clickableThings;
    private NavMeshAgent agent;

    public GameObject moveMarker;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }*/

        //Regen mana
        if (manaRegenReady == true)
        {
            if(currentMana < (maxMana - manaRegen))
            {
                currentMana += manaRegen;
            }
            StartCoroutine(ManaRegenCoolDown(1));
        }
        print(currentMana + " / " + maxMana);

        if (Input.GetMouseButtonDown(1))
        {
            Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
            {
                //print("hitInfo: " + hitInfo);
                //print("point: " + hitInfo.point);
                agent.SetDestination(hitInfo.point);
                Instantiate(moveMarker, hitInfo.point, Quaternion.identity);
            }
        }
    }

    IEnumerator ManaRegenCoolDown(int CD)
    {
        manaRegenReady = false;
        yield return new WaitForSeconds(CD);
        manaRegenReady = true;
    }
}
