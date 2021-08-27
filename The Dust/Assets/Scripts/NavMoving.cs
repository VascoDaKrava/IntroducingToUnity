using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMoving : MonoBehaviour
{
    private List<Vector3> _mainWayPointsList;
    private NavMeshAgent _agent;
    private float _agentSpeedMin = 2f;
    private float _agentSpeedMax = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag(Storage.NordWayTag))
            _mainWayPointsList = GameObject.FindGameObjectWithTag(Storage.GlobalTag).GetComponent<Storage>().NordWay;
        else if (CompareTag(Storage.UndergroundWayTag))
            _mainWayPointsList = GameObject.FindGameObjectWithTag(Storage.GlobalTag).GetComponent<Storage>().UndeegroundWay;
        else if (CompareTag(Storage.SandWayTag))
            _mainWayPointsList = GameObject.FindGameObjectWithTag(Storage.GlobalTag).GetComponent<Storage>().SandWay;

        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = Random.Range(_agentSpeedMin, _agentSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.hasPath || _agent.remainingDistance < _agent.stoppingDistance)
            NextPoint();
    }

    private void NextPoint()
    {
        _agent.SetDestination(_mainWayPointsList[Random.Range(0, _mainWayPointsList.Count)]);
    }
}
