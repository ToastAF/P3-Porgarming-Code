using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesWFollowProjectile : MonoBehaviour
{
    public GameObject parent;


    void Update()
    {
        if(parent != null)
        {
            transform.position = parent.transform.position;
        }
        else
        {
            //ParticleSystem.MainModule var = GetComponent<ParticleSystem>().main;
            //var.loop = false;
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(KillAfterSecs(1));
        }
    }

    IEnumerator KillAfterSecs(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
