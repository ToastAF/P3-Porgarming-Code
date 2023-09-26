using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public double maxHealth;
    double currentHealth, armor, magicResist;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
        armor = 5;
        magicResist = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            QProperties newScript = other.gameObject.GetComponent<QProperties>();
            Destroy(other.gameObject);
            currentHealth -= calculateDamage(newScript.physDmg, newScript.magDmg);
            //print(calculateDamage(newScript.physDmg, newScript.magDmg));
            print("DAMAGE! Current health: " + currentHealth);
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
