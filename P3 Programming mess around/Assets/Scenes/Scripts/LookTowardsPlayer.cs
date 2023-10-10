using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    GameObject player;
    public float rotateExtraAmountX, rotateExtraAmountY, rotateExtraAmountZ;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        transform.Rotate(rotateExtraAmountX, rotateExtraAmountY, rotateExtraAmountZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
