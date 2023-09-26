using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public float maxHealth, maxMana, healthRegen, manaRegen;
    public float currentHealth, currentMana;
    bool regenReady = true;

    public LayerMask clickableThings;
    private NavMeshAgent agent;

    public GameObject moveMarker;

    public GameObject healthText;
    public GameObject manaText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth/2;
        currentMana = maxMana;
    }

    void Update()
    {
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + currentHealth + " / " + maxHealth;
        manaText.GetComponent<TextMeshProUGUI>().text = "Mana: " + currentMana + " / " + maxMana;

        //Health and mana regen
        if (regenReady == true)
        {
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
            StartCoroutine(ManaRegenCoolDown(1));
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
                Instantiate(moveMarker, hitInfo.point, Quaternion.identity);
            }
        }
    }

    IEnumerator ManaRegenCoolDown(int CD)
    {
        regenReady = false;
        yield return new WaitForSeconds(CD);
        regenReady = true;
    }
}
