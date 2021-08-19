using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _bulletStartLeft;
    [SerializeField] private Transform _bulletStartRight;
    [SerializeField] private GameObject _bullet;

    private bool _isPlayerDetected = false;
    private float _distanceForDetection = 5f;
    private float _turretSearchSpeed = 0.10f; // Radian per second
    private float _turretRotateSpeed = 0.75f; // Radian per second
    private SphereCollider _turretCollider;
    private Transform _turretTransform;
    private Transform _playerTransform;
    private Vector3 _turretPivot;

    // Start is called before the first frame update
    void Start()
    {
        _turretTransform = GetComponent<Transform>();
        _turretCollider = gameObject.GetComponent<SphereCollider>();
        _turretCollider.radius = _distanceForDetection;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _turretPivot = _turretTransform.Find("Center").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDetected)
            aimingMode();
        else
            standbyMode();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TurrelController - Player DETECTED");
            _isPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TurrelController - Player MISSED");
            _isPlayerDetected = false;
        }
    }

    private void standbyMode()
    {
        //_turrelTransform.RotateAround(_turrelPivot, Vector3.up, Time.deltaTime * 60);
        _turretTransform.rotation = Quaternion.Euler(0, _turretTransform.rotation.eulerAngles.y + _turretSearchSpeed, 0);
    }

    private void aimingMode()
    {
        _turretTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_turretTransform.forward,
            _playerTransform.position - _turretTransform.position,
            _turretRotateSpeed * Time.deltaTime,
            0));
        if (Vector3.Angle(_turretTransform.forward, _playerTransform.position - _turretTransform.position) < 30) fire();
    }

    private void fire()
    {
        Debug.Log("TurrelController - F I R E");
    }
}
