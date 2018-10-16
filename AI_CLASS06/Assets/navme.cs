using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class navme : MonoBehaviour
{

    public NavMeshAgent[] tanks;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (NavMeshAgent tank in tanks)
        {
            tank.destination = transform.position;
        }

    }
}

