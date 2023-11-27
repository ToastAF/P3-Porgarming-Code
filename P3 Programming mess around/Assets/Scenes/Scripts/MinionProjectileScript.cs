using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MinionProjectileScript : MonoBehaviour
{
    GameObject player;
    public GameObject hitPlayerParticle;
    public float physDmg, magDmg, projectileSpeed, hitRadius;

    void Start()
    {
        //Find the Player in the scene by the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Move towards the player at a set speed
        float temp = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, temp);

        //When the projectile gets close to the player, the TakeDamage method is called on the player, particles are spawned and the projectile is destroyed, thus having ended its purpose
        if(Vector3.Distance(transform.position, player.transform.position) < hitRadius)
        {
            player.GetComponent<PlayerMove>().TakeDamage(physDmg, magDmg);
            Instantiate(hitPlayerParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
