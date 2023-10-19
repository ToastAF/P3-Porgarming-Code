using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class PlayerMove : MonoBehaviour
{
    public float attackDamage, abilityPower, moveSpeed, attackRange, armor, magicResist;
    public float maxHealth, maxMana, healthRegen, manaRegen, gold;
    public float currentHealth, currentMana;
    bool regenReady = true;

    //NavMesh Stuff
    public LayerMask clickableThings;
    private NavMeshAgent agent;

    public GameObject moveMarker;

    //UI Stuff
    public GameObject healthText, manaText, adText, apText, msText, goldText; 
    public Image healthBar, ManaBar;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
        //UI Stuff
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + (int)currentHealth + " / " + maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        manaText.GetComponent<TextMeshProUGUI>().text = "Mana: " + currentMana + " / " + maxMana;
        ManaBar.fillAmount = currentMana / maxMana;
        adText.GetComponent<TextMeshProUGUI>().text = $"{attackDamage}";
        apText.GetComponent<TextMeshProUGUI>().text = $"{abilityPower}";
        goldText.GetComponent<TextMeshProUGUI>().text = $"Gold: {gold}";

        //Health and mana regen
        if (regenReady == true)
        {
            StartCoroutine(RegenPerSecond(1));
        }
        //print(currentMana + " / " + maxMana);

        if (Input.GetMouseButtonDown(1))
        {
            Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
            {
                //print("hitInfo: " + hitInfo);
                //print("point: " + hitInfo.point);
                agent.SetDestination(hitInfo.point);
                transform.LookAt(new Vector3(hitInfo.point.x, hitInfo.point.y + transform.position.y, hitInfo.point.z));
                //Spawn move indicator (Little green cirle)
                Instantiate(moveMarker, hitInfo.point, Quaternion.identity);
            }
        }
    }

    public void TakeDamage(float physDmg, float magDmg)
    {
            currentHealth -= calculateDamage(physDmg, magDmg);
            print("Player damage!");

            if (currentHealth <= 0)
            {
                print("Dead!");
            }
    }

    IEnumerator RegenPerSecond(int CD)
    {
        regenReady = false;
            if(currentHealth < maxHealth)
            {
                currentHealth += healthRegen;
                //Health can't regen to over max amount of health
                if(currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
            if (currentMana < maxMana)
            {
                currentMana += manaRegen;
                //Mana can't regen to over max amount of mana
                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }
        //Gain gold per second
        gold += 1;
        yield return new WaitForSeconds(CD);
        regenReady = true;
    }

    public float calculateDamage(float rawPhysDmg, float rawMagDmg)
    {
        float mitDmg;

        //Calculate armor and magic resist
        mitDmg = (float)(System.Math.Round(rawPhysDmg * (100 / (100 + armor)) + rawMagDmg * (100 / (100 + magicResist)), 2));

        return mitDmg;
    }
}
