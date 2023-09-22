using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    double health, armor, magicResist;
    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        armor = 5;
        magicResist = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
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
            health -= calculateDamage(newScript.physDmg, newScript.magDmg);
            //print(calculateDamage(newScript.physDmg, newScript.magDmg));
            print("DAMAGE! Current health: " + health);
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
