using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QProperties : MonoBehaviour //This script is put on the projectile for the q ability
{
    Rigidbody rb;
    GameObject player;
    PlayerMove playerScript;
    //Layermask for the ground
    public LayerMask clickableThings;
    //Changeable variable to determine projectile speed
    public int projectileSpeed;

    //Vector points to calculate distance between player and mouse
    Vector3 playerStartPos;
    Vector3 mousePlayerVec;

    float tempRange;
    public float setRange;

    //Damage stats
    public float physDmg, magDmg;

    public GameObject hitParticles;
    public GameObject particles;
    GameObject temp;

    void Start()
    {
        //Spawn the particles that will follow the projectile
        temp = Instantiate(particles, transform.position, Quaternion.identity);
        temp.transform.Rotate(new Vector3(0, 180, 0)); //Rotate to align correctly
        temp.GetComponent<ParticlesQFollowProjectile>().parent = gameObject; //Set the parent varibale on the particle system to be the projectile

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player"); //Find the player by tag!
        playerScript = player.GetComponent<PlayerMove>(); //Yoink the script from the player!
        playerStartPos = player.transform.position; //The projectiles start position is on the player

        //Scaling on this ability dependant on the players stats
        physDmg = playerScript.attackDamage * 1f;
        magDmg = playerScript.abilityPower * 0.1f;
        
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition); //Cast a ray from mouse to 3D world from camera
        RaycastHit hitInfo;
        
        if (Physics.Raycast(pointRay, out hitInfo, 100, clickableThings)) //Ray hits ground
        {
            Vector3 v = new Vector3(hitInfo.point.x, hitInfo.point.y + player.transform.position.y, hitInfo.point.z); //Point right above where the mouse ray hit the ground
            mousePlayerVec = v - player.transform.position; //Vector from player to the point v
            transform.LookAt(v); //Turn the projectile towards the point
            rb.velocity = mousePlayerVec.normalized * projectileSpeed; //Vector calculation to make the projectile fly towards mouse position :)
        }  
    }

    void Update()
    {
        tempRange = (transform.position - playerStartPos).magnitude; //Checks how far the projectile has traveled
        if(tempRange > mousePlayerVec.normalized.magnitude*setRange) //If it exceeds the max range,
        {
            Destroy(gameObject); //it is destroyed!
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) //If the projectile hits an enemy object
        {
            //Spawns small sparkles when hitting an enemy
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(gameObject); //And destroy projectile
        }
    }
}
