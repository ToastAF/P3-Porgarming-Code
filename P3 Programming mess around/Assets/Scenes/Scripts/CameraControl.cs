using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Player;
    Vector3 startPos;
    private void Start()
    {
        //startPos is the camera offset compared to the player
        startPos = transform.position;
    }

    void Update()
    {
        //The camera follows the player from the offset
        transform.position = Player.transform.position + startPos;
    }
}
