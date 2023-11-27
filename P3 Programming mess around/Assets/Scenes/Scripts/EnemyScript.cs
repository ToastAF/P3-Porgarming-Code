using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //The enemy has a lot of stats
    public double maxHealth;
    public double currentHealth, armor, magicResist;
    public float range;

    public GameObject hitParticles, hitParticlesW, deathParticles;
    public GameObject player;

    //Health bar
    public Image hBar;

    void Start()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        armor = 5;
        magicResist = 5;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Health bar
        hBar.fillAmount = (float)(currentHealth / maxHealth);

        //If the enemys health is reduced to 0, it is destroyed, spawning particles and giving the player gold
        if(currentHealth <= 0)
        {
            player.GetComponent<PlayerMove>().gold += 100;
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //The enemy checks if there is a projectile with the tag "Projectile" hitting it
        if (other.gameObject.CompareTag("Projectile"))
        {
            QProperties newScript = other.gameObject.GetComponent<QProperties>();
            currentHealth -= calculateDamage(newScript.physDmg, newScript.magDmg);
            
            //Debug
            print("DAMAGE! Current health: " + currentHealth);

            //Spawn particles if the enemys health is over 0
            if (currentHealth > 0)
            {
                Instantiate(hitParticles, transform.position, Quaternion.identity);
            }
        }

        if (other.gameObject.CompareTag("WHitbox"))
        {
            //The same as above but with different names
            WStatsCarryOver tempScr = other.gameObject.GetComponent<WStatsCarryOver>();
            currentHealth -= calculateDamage(tempScr.physDmg, tempScr.magDmg);

            //Debug
            print("DAMAGE! Current health: " + currentHealth);

            //Spawn particles if the enemys health is over 0
            if (currentHealth > 0)
            {
                Instantiate(hitParticles, transform.position, Quaternion.identity);
                Instantiate(hitParticlesW, transform.position, Quaternion.identity);
            }
        }
    }

    //Method to calculate the damage taken from given damage from different sources. The two parameters are the damage from sources
    public float calculateDamage(float rawPhysDmg, float rawMagDmg)
    {
        float mitDmg;

        //Calculate armor and magic resist using LoL formula
        mitDmg = (float)(System.Math.Round(rawPhysDmg * (100 / (100 + armor)) + rawMagDmg * (100 / (100 + magicResist)), 2));

        //Return the mitigated damage
        return mitDmg;
    }
}
