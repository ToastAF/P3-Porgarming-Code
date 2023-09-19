using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;

    public LayerMask clickableThings;
    private NavMeshAgent agent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(pointRay, out hitInfo, 100, clickableThings))
            {
                //print("hitInfo: " + hitInfo);
                //print("point: " + hitInfo.point);
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
