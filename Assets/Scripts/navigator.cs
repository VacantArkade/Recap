using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class navigator : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int patrolPointIndex = 0;

    NavMeshPath path;

    Rigidbody rb;

    [SerializeField] float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = new NavMeshPath();
        rb = GetComponent<Rigidbody>();

        currentPatrolPoint = patrolPoints[0];
        if(agent.CalculatePath(currentPatrolPoint.position, path))
        {
            Debug.Log("Path is valid");
        }
        else
        {
            Debug.Log("Path is invalid");
        }
        //agent.SetDestination(currentPatrolPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;

        float distance = Vector3.Distance(transform.position, currentPatrolPoint.position);

        if(distance < 1)
        {
            patrolPointIndex++;

            if(patrolPointIndex >= patrolPoints.Length)
            {
                patrolPointIndex = 0;
            }

            currentPatrolPoint = patrolPoints[patrolPointIndex];
            //agent.SetDestination(currentPatrolPoint.position);
        }

        Debug.DrawLine(transform.position, currentPatrolPoint.position, Color.green);

        
    }
    private void FixedUpdate()
    {
        Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnDrawGizmos()
    {
        if(path == null)
        {
            return;
        }

        foreach (var corner in path.corners)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(corner, 2);
        }
    }
}
