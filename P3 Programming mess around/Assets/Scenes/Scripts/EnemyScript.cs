using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public double maxHealth;
    public double currentHealth, armor, magicResist;
    public float range;

    public GameObject hitParticles, deathParticles;
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

        if(currentHealth <= 0)
        {
            player.GetComponent<PlayerMove>().gold += 150;
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
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

    public float calculateDamage(float rawPhysDmg, float rawMagDmg)
    {
        float mitDmg;

        //Calculate armor and magic resist
        mitDmg = (float)(System.Math.Round(rawPhysDmg * (100 / (100 + armor)) + rawMagDmg * (100 / (100 + magicResist)), 2));

        return mitDmg;
    }
}
