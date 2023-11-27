using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesQFollowProjectile : MonoBehaviour
{
    //The parent object is set to be the particle that spawned the object this script is on
    public GameObject parent;

    private void Start()
    {
        //Rotate the object to align properly with the direction the projectile is flying
        transform.eulerAngles = new Vector3(parent.transform.eulerAngles.x, parent.transform.eulerAngles.y + 180, parent.transform.eulerAngles.z);
    }

    void Update()
    {
        //Sets the position of the particle system to be the same as the projectile as long as the projectile is alive
        if(parent != null)
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
