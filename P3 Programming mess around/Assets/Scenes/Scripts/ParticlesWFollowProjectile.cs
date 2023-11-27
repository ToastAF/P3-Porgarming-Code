using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesWFollowProjectile : MonoBehaviour
{
    //The parent object is set to be the particle that spawned the object this script is on
    public GameObject parent;

    void Update()
    {
        //Sets the position of the particle system to be the same as the projectile as long as the projectile is alive
        if (parent != null)
        {
            transform.position = parent.transform.position;
        }
        else //When the projectile no longer exists the particle system stops emitting and destroys itself after a second
        {
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(KillAfterSecs(1));
        }
    }

    //Used to wait and destroy the gameobject
    IEnumerator KillAfterSecs(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
