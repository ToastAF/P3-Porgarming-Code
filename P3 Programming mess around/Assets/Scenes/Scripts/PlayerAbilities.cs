using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAbilities : MonoBehaviour
{
    //Projectile gameobjects to be spawned
    public GameObject qProjectile, wProjectile;
 
    PlayerMove playerScript;

    //Variables to control how and when abilities can be cast
    bool castReady, qReady, wReady;
    public float qManaCost, wManaCost, wCastRange;

    //A layermask is set on the ground plane, so the raycast from mouse to world only will hit the ground, even through objects
    public LayerMask clickableThings;

    NavMeshAgent playerNav;
    public float abilityInterruptTime;

    void Start()
    {
        playerNav = GetComponent<NavMeshAgent>();
        playerScript = GetComponent<PlayerMove>();
        castReady = true;
        qReady = true;
        wReady = true;
    }

    void Update()
    {
        //Abilites have a global cooldown, which determines if any abilites can be cast at all
        if(castReady == true)
        {
            //Cast Q ability
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(qReady == true && (playerScript.currentMana >= qManaCost)) //If q is off cooldown and the player has enough mana
                {
                    StartCoroutine(CastCD()); //Set global cast cooldown

                    playerScript.currentMana -= qManaCost; //Mana is deducted
                    StartCoroutine(PutOnCooldown(2, 0)); //Set q cooldown
                    StopAndLook(); //Makes the player stop and look towards where the mouse is
                    Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity); //Spawn a new projectile on the player
                }
            }

            //Cast W ability
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(wReady == true && (playerScript.currentMana >= wManaCost)) //If w is off cooldown and the player has enough mana
                {
                    Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition); //Cast a ray from the mouse to the 3D space from the camera
                    RaycastHit hitInfo;

                    if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings)) //If the ray hits the ground (clickableThing layermask)
                    {
                        if(Vector3.Distance(transform.position, hitInfo.point) <= wCastRange) //And the mouse is within casting distance
                        {
                            StartCoroutine(CastCD()); //Set global cast cooldown

                            playerScript.currentMana -= wManaCost; //Deduct mana
                            StartCoroutine(PutOnCooldown(3, 1)); //Put w on cooldown
                            StopAndLook(); //Makes the player stop and look towards where the mouse is
                            Instantiate(wProjectile, new Vector3(hitInfo.point.x, hitInfo.point.y + 7, hitInfo.point.z), Quaternion.identity); //Spawn the projectile above where the mouse is pointed
                        }
                    }
                }
            }
        }
    }

    //A method that casts a ray from the mouse to the ground and makes the player look at the location of the mouse
    void LookAtLocation()
    {
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
        {
            transform.LookAt(new Vector3(hitInfo.point.x, hitInfo.point.y + transform.position.y, hitInfo.point.z));
        }
    }
    
    //Method that combines two other things to simplify calling it
    void StopAndLook()
    {
        LookAtLocation();
        StartCoroutine(InterruptMovement());
    }

    //Method that takes two arguments for cooldown time, and which ability should be put on cooldown
    IEnumerator PutOnCooldown(int CD, int WhichAbility)
    {
        if(WhichAbility == 0) //This is for the q ability
        {
            qReady = false;
            yield return new WaitForSeconds(CD);
            qReady = true;
        }else if(WhichAbility == 1) //This is for the w ability
        {
            wReady = false;
            yield return new WaitForSeconds(CD);
            wReady = true;
        } 
    }
    
    //Sets the global cast cooldown
    IEnumerator CastCD()
    {
        castReady = false;
        yield return new WaitForSeconds(1);
        castReady = true;
    }

    //Makes the player stop for a short while and then continues movement after using the .isStopped variable on the navmesh agent
    IEnumerator InterruptMovement()
    {
        playerNav.isStopped = true;
        yield return new WaitForSeconds(abilityInterruptTime);
        playerNav.isStopped = false;
    }
}
