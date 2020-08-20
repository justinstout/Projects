using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAgent : MonoBehaviour
{
    public GameObject Destination;
    private NavMeshAgent Agent;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Destination.transform.position);
    }
}
