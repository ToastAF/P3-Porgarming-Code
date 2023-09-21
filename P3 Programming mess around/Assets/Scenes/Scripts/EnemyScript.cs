using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    float health, armor, magicResist;
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
            health -= newScript.physDmg + newScript.magDmg;
            print("DAMAGE! Current health: " + health);
        }
    }

    void calculateDamage()
    {

    }
}
