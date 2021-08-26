using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMoving : MonoBehaviour
{
    private Stack<Vector3> _wayPointsStack;
    private Stack<Vector3> _wayPointsStack1;
    [SerializeField] private Transform _target;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _wayPointsStack = new Stack<Vector3>(GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().NordWay);
        _agent = GetComponent<NavMeshAgent>();
        //_agent.SetDestination(_target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.hasPath)
            NextPoint();
    }

    private void NextPoint()
    {
        _agent.SetDestination(_wayPointsStack.Peek());
        Debug.Log("_wayPointsStack.Peek() = " + _wayPointsStack.Peek());
        foreach (Vector3 item in _wayPointsStack)
        {
            Debug.Log("item = " + item);
        }
        Vector3 cur = _wayPointsStack.Pop();
        //_wayPointsStack.Push(cur);
        

    }
}
