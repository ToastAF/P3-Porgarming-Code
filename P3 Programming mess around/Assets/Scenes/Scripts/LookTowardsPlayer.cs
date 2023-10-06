using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        transform.Rotate(0, -45, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
