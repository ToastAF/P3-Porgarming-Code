using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    //This script destroys the gameobject it is put on after x seconds, dependant on the destroyDelay variable
    public float destroyDelay;
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
