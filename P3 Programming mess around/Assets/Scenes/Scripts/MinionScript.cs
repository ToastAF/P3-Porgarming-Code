using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : EnemyScript 
{
    //MinionScript enherits from EnemyScript, so it has all the public variables from there.

    GameObject player;
    public GameObject projectile;
    bool attackCD = false;

    void Start()
    {
        maxHealth = 10;
        currentHealth = maxHealth;
        armor = 20;
        magicResist = 0;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Health bar
        hBar.fillAmount = (float)(currentHealth / maxHealth);

        //Attacks player if close enough
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            if (attackCD == false)
            {
                StartCoroutine(AttackCD(1));
            }
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    IEnumerator AttackCD(int number)
    {
        attackCD = true;
        Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(number);
        attackCD = false;
    }
}
