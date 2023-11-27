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
    //The players stats
    public float attackDamage, abilityPower, moveSpeed, attackRange, armor, magicResist;
    public float maxHealth, maxMana, healthRegen, manaRegen, gold;
    public float currentHealth, currentMana;
    bool regenReady = true;

    //A layermask is set on the ground plane, so the raycast from mouse to world only will hit the ground, even through objects
    public LayerMask clickableThings;
    
    //NavMesh Stuff
    private NavMeshAgent agent;

    //A little green cylinder to show where the player clicked to move
    public GameObject moveMarker;

    //UI elements
    public GameObject healthText, manaText, adText, apText, msText, goldText; 
    public Image healthBar, ManaBar;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Set health and mana to max at the start of the game
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
        //Manipulate all the UI elements based on the characters stats and gold
        healthText.GetComponent<TextMeshProUGUI>().text = "Health: " + (int)currentHealth + " / " + maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        manaText.GetComponent<TextMeshProUGUI>().text = "Mana: " + currentMana + " / " + maxMana;
        ManaBar.fillAmount = currentMana / maxMana;
        adText.GetComponent<TextMeshProUGUI>().text = $"{attackDamage}";
        apText.GetComponent<TextMeshProUGUI>().text = $"{abilityPower}";
        goldText.GetComponent<TextMeshProUGUI>().text = $"Gold: {gold}";

        //Health and mana regen once per second
        if (regenReady == true)
        {
            StartCoroutine(RegenPerSecond(1));
        }

        //When clicking on the right mouse button, a ray is cast from the mouse to the 3D space from the camera.
        if (Input.GetMouseButtonDown(1))
        {
            Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(pointRay, out hitInfo, 100, clickableThings)) //If that ray hits the ground (which it does)
            {
                agent.SetDestination(hitInfo.point); //Set the destination of the navmesh agent to be the point where the ray hits the ground
                transform.LookAt(new Vector3(hitInfo.point.x, hitInfo.point.y + transform.position.y, hitInfo.point.z)); //Makes the player snappily look towards the point where the ray hit
                Instantiate(moveMarker, hitInfo.point, Quaternion.identity); //Spawn move indicator (little green cylinder)
            }
        }
    }

    public void TakeDamage(float physDmg, float magDmg) //Method to make the player take damage, which is is easy to call again and again
    {
            currentHealth -= calculateDamage(physDmg, magDmg); //Subtract damage numbers from health using the calculateDamage() method

            if (currentHealth <= 0) //If the health is reduced to 0, the player is technically dead, but it only prints "Dead!" in the console
            {
                print("Dead!");
            }
    }

    //This is used in a coroutine to regenerate health and mana once per second
    IEnumerator RegenPerSecond(int CD)
    {
        regenReady = false; //If we haven't regened
            if(currentHealth < maxHealth) //And the player doesn't have max health
            {
                currentHealth += healthRegen; //Regen health
                //Health can't regen to over max amount of health
                if(currentHealth > maxHealth)
                {
                    currentHealth = maxHealth; //If the player regens too much, the health is set to max as to not overheal
                }
            }
            if (currentMana < maxMana) //The same goes for mana regen
            {
                currentMana += manaRegen;
                //Mana can't regen to over max amount of mana
                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
            }
        //The player also gains 1 gold per second
        gold += 1;
        yield return new WaitForSeconds(CD); //Wait a bit
        regenReady = true; //Then we can regen again
    }

    //Method to calculate the damage taken based on raw damage numbers and resistances
    public float calculateDamage(float rawPhysDmg, float rawMagDmg) //Takes physical and magical damage input as parameters
    {
        float mitDmg;

        //Calculate armor and magic resist using LoL's formula
        mitDmg = (float)(System.Math.Round(rawPhysDmg * (100 / (100 + armor)) + rawMagDmg * (100 / (100 + magicResist)), 2));

        return mitDmg; //Return the mitigated damage number
    }
}
