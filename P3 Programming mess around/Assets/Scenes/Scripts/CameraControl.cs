using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Player;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        //Potentially for camera manual movement
        /*if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }*/

        transform.position = Player.transform.position + startPos;
    }
}
