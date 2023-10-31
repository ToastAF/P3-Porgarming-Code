using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : EnemyScript
{
    public GameObject icosphere;

    //Object to show attack range
    public GameObject rangeCircle;
    Color normal = new(0, 0, 0, 0.15f);
    Color inRange = new(0, 0, 0, 0.55f);

    public GameObject projectile;
    bool attackCD = false;
    public float attackSpeedDelay;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        //Show range
        rangeCircle.transform.localScale = new Vector3(2 * range, 2 * range, 2 * range);

        //Health bar
        hBar.fillAmount = (float)(currentHealth / maxHealth);

        //Attacks player if close enough
        if (Vector3.Distance(transform.position, player.transform.position) < range)
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
            player.GetComponent<PlayerMove>().gold += 50;
            Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    new private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            QProperties newScript = other.gameObject.GetComponent<QProperties>();
            currentHealth -= calculateDamage(newScript.physDmg, newScript.magDmg);

            //print(calculateDamage(newScript.physDmg, newScript.magDmg));
            print("DAMAGE! Current health: " + currentHealth);

            if (currentHealth > 0)
            {
                Instantiate(hitParticles, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.identity);
            }
        }
    }

    IEnumerator AttackCD(float number)
    {
        attackCD = true;
        Instantiate(projectile, icosphere.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(number);
        attackCD = false;
    }
}
