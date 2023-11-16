using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WProperties : MonoBehaviour
{
    public float downWardForce;
    public GameObject particles;
    GameObject temp;

    void Start()
    {
        temp = Instantiate(particles, transform.position, Quaternion.identity);
        temp.transform.Rotate(new Vector3(-90, 0, 0));
        temp.GetComponent<ParticlesFollowProjectile>().parent = gameObject;
    }

    void Update()
    {
        transform.position += new Vector3(0, -downWardForce * Time.deltaTime, 0);

        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
