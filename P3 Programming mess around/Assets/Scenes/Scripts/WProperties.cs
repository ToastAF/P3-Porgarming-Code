using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WProperties : MonoBehaviour
{
    public float downWardForce;
    public GameObject particles, groundSlam, slamParticles;
    public float physDmg, magDmg;

    void Start()
    {
        GameObject temp;
        temp = Instantiate(particles, transform.position, Quaternion.identity);
        temp.transform.Rotate(new Vector3(-90, 0, 0));
        temp.GetComponent<ParticlesWFollowProjectile>().parent = gameObject;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMove playerScript = player.GetComponent<PlayerMove>();
        //Scaling on this ability dependant on the players stats
        physDmg = playerScript.attackDamage * 0.1f;
        magDmg = playerScript.abilityPower * 2f;
    }

    void Update()
    {
        transform.position += new Vector3(0, -downWardForce * Time.deltaTime, 0);

        if(transform.position.y < 0)
        {
            GameObject temp = Instantiate(groundSlam, new Vector3(transform.position.x, transform.position.y+ 0.1f, transform.position.z) , Quaternion.identity);
            temp.transform.Rotate(new Vector3(90, 0, 0));
            WStatsCarryOver tempScr = temp.GetComponent<WStatsCarryOver>();
            tempScr.physDmg = physDmg;
            tempScr.magDmg = magDmg;

            GameObject temp2 = Instantiate(slamParticles, transform.position, Quaternion.identity);
            temp2.transform.Rotate(new Vector3(-90, 0, 0));
            Destroy(gameObject);
        }
    }
}
