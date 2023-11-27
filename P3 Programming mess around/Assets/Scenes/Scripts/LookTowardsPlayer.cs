using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    //This scripts rotates the gameobject it is on and points it toward the player. It also rotates a changeable amount, which can be adjusted in the inspector
    GameObject player;
    public float rotateExtraAmountX, rotateExtraAmountY, rotateExtraAmountZ;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        transform.Rotate(rotateExtraAmountX, rotateExtraAmountY, rotateExtraAmountZ);
    }
}
