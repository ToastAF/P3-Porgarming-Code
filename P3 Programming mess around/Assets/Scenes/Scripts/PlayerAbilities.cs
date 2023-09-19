using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject qProjectile;
    private bool QReady = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(QReady == true)
            {
                Instantiate(qProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }

    IEnumerator PutOnCooldown(int CD)
    {
        QReady = false;
        yield return new WaitForSeconds(CD);
        QReady = true;
    }
}
