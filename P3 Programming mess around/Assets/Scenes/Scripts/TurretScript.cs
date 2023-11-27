using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : EnemyScript
{
    //The object from where the attack projectile is spawned
    public GameObject icosphere;

    //Object to show attack range, with two different colors for indicating whether the player is in range or not
    public GameObject rangeCircle;
    Color normal = new(0, 0, 0, 0.15f);
    Color inRange = new(0, 0, 0, 0.55f);

    public GameObject projectile;
    bool attackCD = false;
    public float attackSpeedDelay;

    void Start()
    {
        //Set health to max and locate player by tag
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        //Show range
        rangeCircle.transform.localScale = new Vector3(2 * range, 2 * range, 2 * range);

        //Show health bar on a canvas above the turrets head
        hBar.fillAmount = (float)(currentHealth / maxHealth);

        //Attacks player if close enough
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            //Change the range circles color to show player is in range
            rangeCircle.GetComponent<SpriteRenderer>().color = inRange;
            if (attackCD == false) //Doesn't attack if it is on cooldown
            {
                StartCoroutine(AttackCD(attackSpeedDelay));
            }
        }
        else
        {
            rangeCircle.GetComponent<SpriteRenderer>().color = normal;
        }

        //If the turrets health reaches 0, the player is awarded gold, and the turret is destroyed (with particles!)
        if (currentHealth <= 0)
        {
            player.GetComponent<PlayerMove>().gold += 300;
            Instantiate(deathParticles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    new private void OnTriggerEnter(Collider other)
    {
        //The turret checks if there is a projectile with the tag "Projectile" hitting it
        if (other.gameObject.CompareTag("Projectile"))
        {
            //The projectiles QProperties script is yoinked and the turrets health is reduced by the damage stats from the projectile
            QProperties newScript = other.gameObject.GetComponent<QProperties>();
            currentHealth -= calculateDamage(newScript.physDmg, newScript.magDmg);

            //Debug
            print("DAMAGE! Current health: " + currentHealth);

            //Spawn particles if the turrets health is over 0
            if (currentHealth > 0)
            {
                Instantiate(hitParticles, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.identity);
            }
        }

        if (other.gameObject.CompareTag("WHitbox"))
        {
            //The same as above but with different names
            WStatsCarryOver tempScr = other.gameObject.GetComponent<WStatsCarryOver>();
            currentHealth -= calculateDamage(tempScr.physDmg, tempScr.magDmg);

            //Debug
            print("DAMAGE! Current health: " + currentHealth);

            //Spawn particles if the turrets health is over 0
            if (currentHealth > 0)
            {
                Instantiate(hitParticles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            }
        }
    }

    //This is used in a coroutine to count seconds and wait to attack the player, so as to not machine gun the player down
    IEnumerator AttackCD(float number)
    {
        attackCD = true;
        Instantiate(projectile, icosphere.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(number);
        attackCD = false;
    }
}
