using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesFollowProjectile : MonoBehaviour
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
            transform.position += new Vector3(0, -15 * Time.deltaTime, 0);
            StartCoroutine(KillAfterSecs(1));
        }
    }

    IEnumerator KillAfterSecs(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
