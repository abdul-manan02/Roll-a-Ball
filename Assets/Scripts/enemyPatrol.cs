using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyPatrol : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform[] destinations;
    int destPoint = 0;

    void Start()
    {
        foreach (Transform destination in destinations)
        {
            destination.gameObject.SetActive(false);   
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (destinations.Length == 0)
            return;
        navMeshAgent.destination = destinations[destPoint].position;
        destPoint = (destPoint + 1) % destinations.Length;
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}

