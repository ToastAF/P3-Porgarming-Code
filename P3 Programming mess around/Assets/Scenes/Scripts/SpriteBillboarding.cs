using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboarding : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main; //Find the main camera
    }

    void Update()
    {
        transform.rotation = cam.transform.rotation; //Rotate the 2D sprite so it faces the same direction as the camera, as to appear flat
    }
}
