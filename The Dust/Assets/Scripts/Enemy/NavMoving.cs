using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMoving : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Pursuit,
        Wait
    }

    private List<Vector3> _mainWayPointsList;
    private NavMeshAgent _agent;
    private Vector3 _lastPoint;
    private Transform _playerTransform;
    private float _agentSpeedMin = 2f;
    private float _agentSpeedMax = 5f;
    private float _agentRotationSpeedNormal;
    private float _agentRotationSpeedPersuit = 360;
    private float _minDistanceToPlayer = 2f;
    private float _timeBackToPath = 3f;
    private EnemyState _agentState = EnemyState.Patrol;

    /// <summary>
    /// Navigation state of the agent
    /// </summary>
    public EnemyState AgentState { get { return _agentState; } set { _agentState = value; } }

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
        _agentRotationSpeedNormal = _agent.angularSpeed;

        _playerTransform = GameObject.FindGameObjectWithTag(Storage.PlayerTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_agentState)
        {
            case EnemyState.Patrol:

                if (!_agent.hasPath || _agent.remainingDistance < _agent.stoppingDistance)
                    NextPoint();
                break;
            
            case EnemyState.Pursuit:
                _agent.angularSpeed = _agentRotationSpeedPersuit;
                _lastPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                _agent.destination = _playerTransform.position;

                if (_agent.remainingDistance <= _minDistanceToPlayer)
                    _agent.isStopped = true;
                else
                    _agent.isStopped = false;
                break;
            
            case EnemyState.Wait:
                StartCoroutine(WaitForBackToPath(_timeBackToPath));
                break;
            
            default:
                break;
        }
    }

    private void NextPoint()
    {
        _agent.SetDestination(_mainWayPointsList[Random.Range(0, _mainWayPointsList.Count)]);
    }

    IEnumerator WaitForBackToPath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _agent.isStopped = false;
        _agent.path.ClearCorners();
        _agent.destination = _lastPoint;
        _agent.angularSpeed = _agentRotationSpeedNormal;
        _agentState = EnemyState.Patrol;
    }
}
