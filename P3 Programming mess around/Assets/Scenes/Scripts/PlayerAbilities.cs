using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject qProjectile;
    private bool QReady = true;

    NavMeshAgent playerNav;
    public float abilityInterruptTime;

    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(QReady == true)
            {
                StartCoroutine(PutOnCooldown(3));
                StartCoroutine(InterruptMovement());
                Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }

    IEnumerator PutOnCooldown(int CD)
    {
        QReady = false;
        yield return new WaitForSeconds(CD);
        QReady = true;
    }

    IEnumerator InterruptMovement()
    {
        playerNav.isStopped = true;
        yield return new WaitForSeconds(abilityInterruptTime);
        playerNav.isStopped = false;
    }
}
