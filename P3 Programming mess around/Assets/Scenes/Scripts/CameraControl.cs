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
        transform.position = Player.transform.position + startPos;
    }
}
