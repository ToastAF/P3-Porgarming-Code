using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : EnemyScript 
{
    //MinionScript enherits from EnemyScript, so it has all the public variables from there.

    //Object to show attack range
    public GameObject rangeCircle;
    Color normal = new(0, 0, 0, 0.15f);
    Color inRange = new(0, 0, 0, 0.55f);

    GameObject player;
    public GameObject projectile;
    bool attackCD = false;
    public float attackSpeedDelay;

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
        //Show range
        rangeCircle.transform.localScale = new Vector3(2*range, 2*range, 2*range);

        //Health bar
        hBar.fillAmount = (float)(currentHealth / maxHealth);

        //Attacks player if close enough
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            rangeCircle.GetComponent<SpriteRenderer>().color = inRange;
            if (attackCD == false)
            {
                StartCoroutine(AttackCD(attackSpeedDelay));
            }
        }
        else
        {
            rangeCircle.GetComponent<SpriteRenderer>().color = normal;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    IEnumerator AttackCD(float number)
    {
        attackCD = true;
        Instantiate(projectile, transform.position, Quaternion.identity);
        LookAtPlayer();
        yield return new WaitForSeconds(number);
        attackCD = false;
    }

    void LookAtPlayer()
    {
        Vector3 temp = player.transform.position;
        transform.LookAt(new Vector3(temp.x, transform.position.y, temp.z));
    }
}
