using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMoving : MonoBehaviour
{
    [SerializeField] Transform _wayPoint;
    [SerializeField] NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
