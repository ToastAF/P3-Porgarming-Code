using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public double maxHealth;
    double currentHealth, armor, magicResist;

    public GameObject hitParticles, deathParticles;

    void Start()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        armor = 5;
        magicResist = 5;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            QProperties newScript = other.gameObject.GetComponent<QProperties>();
            currentHealth -= calculateDamage(newScript.physDmg, newScript.magDmg);

            //print(calculateDamage(newScript.physDmg, newScript.magDmg));
            print("DAMAGE! Current health: " + currentHealth);

            if(currentHealth > 0)
            {
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            }
        }
    }

    double calculateDamage(float rawPhysDmg, float rawMagDmg)
    {
        double mitDmg;

        //Calculate armor and magic resist
        mitDmg = System.Math.Round(rawPhysDmg * (100 / (100 + armor)) + rawMagDmg * (100 / (100 + magicResist)), 2) ;

        return mitDmg;
    }
}
