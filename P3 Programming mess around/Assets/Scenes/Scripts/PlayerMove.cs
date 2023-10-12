using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float attackDamage, abilityPower, moveSpeed, attackRange;
    public float maxHealth, maxMana, healthRegen, manaRegen;
    public float currentHealth, currentMana;
    bool regenReady = true;

    //NavMesh Stuff
    public LayerMask clickableThings;
    private NavMeshAgent agent;

    public GameObject moveMarker;

    //UI Stuff
    public GameObject healthText, manaText, adText, apText, msText; 
    public Image healthBar, ManaBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth/2;
        currentMana = maxMana;
    }

    void Update()
    {
        //UI Stuff
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + currentHealth + " / " + maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        manaText.GetComponent<TextMeshProUGUI>().text = "Mana: " + currentMana + " / " + maxMana;
        ManaBar.fillAmount = currentMana / maxMana;
        adText.GetComponent<TextMeshProUGUI>().text = $"{attackDamage}";
        apText.GetComponent<TextMeshProUGUI>().text = $"{abilityPower}";


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
                transform.LookAt(hitInfo.point);
                //Spawn move indicator (Little green cirle)
                Instantiate(moveMarker, hitInfo.point, Quaternion.identity);
            }
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
        yield return new WaitForSeconds(CD);
        regenReady = true;
    }
}
