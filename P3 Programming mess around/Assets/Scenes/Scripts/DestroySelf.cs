using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
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
