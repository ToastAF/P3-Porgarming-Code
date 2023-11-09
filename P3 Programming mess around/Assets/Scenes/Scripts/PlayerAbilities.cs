using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject qProjectile;
    PlayerMove playerScript;

    bool castReady, qReady, wReady;
    public float qManaCost, wManaCost;

    public LayerMask clickableThings;
    NavMeshAgent playerNav;
    public float abilityInterruptTime;

    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
        playerScript = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if(castReady == true)
        {
            //Cast Q ability
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(qReady == true && (playerScript.currentMana >= qManaCost))
                {
                    CastCD();

                    playerScript.currentMana -= qManaCost;
                    StartCoroutine(PutOnCooldown(2));
                    StartCoroutine(InterruptMovement());
                    LookAtLocation();
                    Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
            }

            //Cast W ability
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(wReady == true && (playerScript.currentMana >= wManaCost))
                {
                    CastCD();
                }
            }
        }
    }

    void LookAtLocation()
    {
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
        {
            transform.LookAt(new Vector3(hitInfo.point.x, hitInfo.point.y + transform.position.y, hitInfo.point.z));
        }
    }

    IEnumerator PutOnCooldown(int CD)
    {
        qReady = false;
        yield return new WaitForSeconds(CD);
        qReady = true;
    }

    IEnumerator CastCD()
    {
        castReady = false;
        yield return new WaitForSeconds(1);
        castReady = true;
    }

    IEnumerator InterruptMovement()
    {
        playerNav.isStopped = true;
        yield return new WaitForSeconds(abilityInterruptTime);
        playerNav.isStopped = false;
    }
}
