using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MinionProjectileScript : MonoBehaviour
{
    GameObject player;
    public GameObject hitPlayerParticle;
    NavMeshAgent agent;
    public float physDmg, magDmg, projectileSpeed, hitRadius;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //agent.SetDestination(player.transform.position);

        float temp = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, temp);

        if(Vector3.Distance(transform.position, player.transform.position) < hitRadius)
        {
            player.GetComponent<PlayerMove>().TakeDamage(physDmg, magDmg);
            Instantiate(hitPlayerParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
