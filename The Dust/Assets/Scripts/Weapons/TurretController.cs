using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    private bool _isPlayerDetected = false;

    private float _distanceForDetection = 7f;
    private float _turretSearchSpeed = 0.10f; // Radian per second
    private float _turretRotateSpeed = 0.75f; // Radian per second
    private float _bulletSpeed = 0.5f;
    private int _bulletDamage = 10;

    private int _rateOfFire = 6; // 30 Shots per second

    private BulletController _bulletCloneScript;

    private SphereCollider _turretTriggerCollider;

    private Transform _turretTransform;
    private Transform _playerTransform;

    private Transform _bulletStartLeft;
    private Transform _bulletStartRight;
    private Transform _bulletParentTransform;

    // Start is called before the first frame update
    void Start()
    {
        _turretTransform = GetComponent<Transform>();
        _turretTriggerCollider = gameObject.GetComponent<SphereCollider>();
        _turretTriggerCollider.radius = _distanceForDetection;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _bulletStartLeft = GameObject.Find("BulletStartLeft").transform;
        _bulletStartRight = GameObject.Find("BulletStartRight").transform;
        _bulletParentTransform = GameObject.FindGameObjectWithTag("DynamicallyCreatedTag").transform;
        GameObject.FindGameObjectWithTag("GlobalScript").GetComponent<Storage>().TriggerColliderList.Add(_turretTriggerCollider.GetHashCode());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDetected)
            AimingMode();
        else
            StandbyMode();
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

    private void StandbyMode()
    {
        _turretTransform.rotation = Quaternion.Euler(0, _turretTransform.rotation.eulerAngles.y + _turretSearchSpeed, 0);
        if (IsInvoking("fire"))
            CancelInvoke();
    }

    private void AimingMode()
    {
        _turretTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_turretTransform.forward,
            _playerTransform.position - _turretTransform.position,
            _turretRotateSpeed * Time.deltaTime,
            0));
        if (Vector3.Angle(_turretTransform.forward, _playerTransform.position - _turretTransform.position) < 30)
        {
            if (!IsInvoking("fire"))
                InvokeRepeating("fire", 0, 60 / _rateOfFire);
        }
        else
        {
            if (IsInvoking("fire"))
                CancelInvoke();
        }
    }

    private void fire()
    {
        Debug.Log("TurrelController - Fire left gun ");
        _bulletCloneScript = Instantiate(_bullet, _bulletStartLeft.position, _bulletStartLeft.rotation, _bulletParentTransform).GetComponent<BulletController>();
        _bulletCloneScript.BulletSpeed = _bulletSpeed;
        _bulletCloneScript.BulletDamage = _bulletDamage;

        Debug.Log("TurrelController - Fire right gun ");
        _bulletCloneScript = Instantiate(_bullet, _bulletStartRight.position, _bulletStartLeft.rotation, _bulletParentTransform).GetComponent<BulletController>();
        _bulletCloneScript.BulletSpeed = _bulletSpeed;
        _bulletCloneScript.BulletDamage = _bulletDamage;
    }
}
