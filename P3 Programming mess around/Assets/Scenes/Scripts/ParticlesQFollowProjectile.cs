using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesQFollowProjectile : MonoBehaviour
{
    public GameObject parent;

    private void Start()
    {
        transform.eulerAngles = new Vector3(parent.transform.eulerAngles.x, parent.transform.eulerAngles.y + 180, parent.transform.eulerAngles.z);
    }

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
