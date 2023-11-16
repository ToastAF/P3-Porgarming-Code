using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject qProjectile, wProjectile;
    PlayerMove playerScript;

    bool castReady, qReady, wReady;
    public float qManaCost, wManaCost, wCastRange;

    public LayerMask clickableThings;
    NavMeshAgent playerNav;
    public float abilityInterruptTime;

    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
        playerScript = GetComponent<PlayerMove>();
        castReady = true;
        qReady = true;
        wReady = true;
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
                    StartCoroutine(CastCD());

                    playerScript.currentMana -= qManaCost;
                    StartCoroutine(PutOnCooldown(2, 0));
                    StopAndLook();
                    Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                }
            }

            //Cast W ability
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(wReady == true && (playerScript.currentMana >= wManaCost))
                {
                    Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
                    {
                        if(Vector3.Distance(transform.position, hitInfo.point) <= wCastRange)
                        {
                            StartCoroutine(CastCD());

                            playerScript.currentMana -= wManaCost;
                            StartCoroutine(PutOnCooldown(3, 1));
                            StopAndLook();
                            Instantiate(wProjectile, new Vector3(hitInfo.point.x, hitInfo.point.y + 7, hitInfo.point.z), Quaternion.identity);
                        }
                    }
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
    
    void StopAndLook()
    {
        LookAtLocation();
        StartCoroutine(InterruptMovement());
    }

    IEnumerator PutOnCooldown(int CD, int WhichAbility)
    {
        if(WhichAbility == 0)
        {
            qReady = false;
            yield return new WaitForSeconds(CD);
            qReady = true;
        }else if(WhichAbility == 1)
        {
            wReady = false;
            yield return new WaitForSeconds(CD);
            wReady = true;
        } 
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
