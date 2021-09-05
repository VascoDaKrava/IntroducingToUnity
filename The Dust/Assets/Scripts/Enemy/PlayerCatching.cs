using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatching : MonoBehaviour
{
    private NavMoving _navigationScript;
    private RaycastHit _hit;
    private float _seeDistance = 20f;
    private float _seeSphereRadius = 5f;
    private float _shootingDistance = 15f;
    private int _playerLayer = 1 << 6; // Player have layer #6
    private bool _isPlayerOnPursuit = false;

    // Start is called before the first frame update
    void Start()
    {
        _navigationScript = GetComponent<NavMoving>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Physics.SphereCast(transform.position + Vector3.up * 1.8f, // Move cast start position to eye
            _seeSphereRadius,
            transform.forward,
            out _hit,
            _seeDistance,
            _playerLayer,
            QueryTriggerInteraction.Ignore))
        {
            if (_hit.distance < _shootingDistance) Fire();
            _navigationScript.AgentState = NavMoving.EnemyState.Pursuit;
            _isPlayerOnPursuit = true;
        }
        else
        {
            if (_isPlayerOnPursuit)
            {
                _isPlayerOnPursuit = false;
                _navigationScript.AgentState = NavMoving.EnemyState.Wait;
            }
        }
    }

    private void Fire()
    {
        Debug.Log("Fire to player!!!");
    }
}
