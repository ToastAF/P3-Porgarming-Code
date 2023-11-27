using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WProperties : MonoBehaviour
{
    public float downWardForce; //Speed at which the projectile moves downwards
    public GameObject particles, groundSlam, slamParticles; //Things the projectile spawns
    public float physDmg, magDmg; //Damage stats

    void Start()
    {
        GameObject temp;
        temp = Instantiate(particles, transform.position, Quaternion.identity); //Spawn the particles which follow the projectile downward
        temp.transform.Rotate(new Vector3(-90, 0, 0)); //Rotate to align correctly
        temp.GetComponent<ParticlesWFollowProjectile>().parent = gameObject; //Set the parent variable on the particle system to be the projectile

        //Find the player and yoink the PlayerMove script with all the stats
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMove playerScript = player.GetComponent<PlayerMove>();

        //Scaling on this ability dependant on the players stats
        physDmg = playerScript.attackDamage * 0.1f;
        magDmg = playerScript.abilityPower * 2f;
    }

    void Update()
    {
        transform.position += new Vector3(0, -downWardForce * Time.deltaTime, 0); //Move the projectile downwards

        if(transform.position.y < 0) //If the projectile reaches the ground
        {
            GameObject temp = Instantiate(groundSlam, new Vector3(transform.position.x, transform.position.y+ 0.1f, transform.position.z) , Quaternion.identity); //A crater sprite is spawned
            temp.transform.Rotate(new Vector3(90, 0, 0)); //Sprite is rotated to align correctly
            WStatsCarryOver tempScr = temp.GetComponent<WStatsCarryOver>(); //The damage stats are transfered to the sprite object, which has the hitbox to hit enemies
            tempScr.physDmg = physDmg;
            tempScr.magDmg = magDmg;

            GameObject temp2 = Instantiate(slamParticles, transform.position, Quaternion.identity); //An explosion of particles is spawned
            temp2.transform.Rotate(new Vector3(-90, 0, 0)); //Also rotated properly
            Destroy(gameObject); //Destroy the projectile, because BOOM!
        }
    }
}
