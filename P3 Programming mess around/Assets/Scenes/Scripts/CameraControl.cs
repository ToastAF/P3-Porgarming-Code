using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        transform.position = Player.transform.position + new Vector3(4, 4, -4);
    }
}
