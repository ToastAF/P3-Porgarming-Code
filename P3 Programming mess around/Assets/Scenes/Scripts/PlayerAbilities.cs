using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject qProjectile;
    PlayerMove playerScript;

    bool qReady = true;
    public float qManaCost;

    NavMeshAgent playerNav;
    public float abilityInterruptTime;

    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
        playerScript = GetComponent<PlayerMove>();
    }

    void Update()
    {
        

        //Cast Q ability
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(qReady == true && (playerScript.currentMana >= qManaCost))
            {
                playerScript.currentMana -= qManaCost;
                StartCoroutine(PutOnCooldown(3));
                StartCoroutine(InterruptMovement());
                Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }

    IEnumerator PutOnCooldown(int CD)
    {
        qReady = false;
        yield return new WaitForSeconds(CD);
        qReady = true;
    }

    IEnumerator InterruptMovement()
    {
        playerNav.isStopped = true;
        yield return new WaitForSeconds(abilityInterruptTime);
        playerNav.isStopped = false;
    }
}
