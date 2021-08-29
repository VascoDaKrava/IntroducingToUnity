using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;

    private bool _isPlayerDetected = false;

    private float _distanceForDetection = 18f;
    private float _turretSearchSpeed = 0.10f; // Radian per second
    private float _turretRotateSpeed = 0.75f; // Radian per second
    private float _bulletSpeed = 10f; // Units per second
    private int _bulletDamage = 5;

    private int _rateOfFire = 30; // Shots per second

    private string _fireMethodName = "Fire"; // Name of fire-method for Invoke

    private BulletControllerRay _bulletCloneScript;

    private SphereCollider _turretTriggerDetectorCollider;

    private Transform _turretTransform;
    private Transform _playerTransform;

    private Transform _bulletStartLeft;
    private Transform _bulletStartRight;
    private Transform _bulletParentTransform;

    // Start is called before the first frame update
    void Start()
    {
        _turretTransform = GetComponent<Transform>();
        _turretTriggerDetectorCollider = gameObject.GetComponent<SphereCollider>();
        _turretTriggerDetectorCollider.radius = _distanceForDetection;
        _playerTransform = GameObject.FindGameObjectWithTag(Storage.PlayerTag).transform;
        _bulletParentTransform = GameObject.FindGameObjectWithTag(Storage.DynamicallyCreatedTag).transform;
        _bulletStartLeft = Storage.FindTransformInChildrenWithTag(gameObject, Storage.Bullet1StartPositionTag);
        _bulletStartRight = Storage.FindTransformInChildrenWithTag(gameObject, Storage.Bullet2StartPositionTag);
        
        // Add own trigger-collider to Global List
        GameObject.FindGameObjectWithTag(Storage.GlobalTag).GetComponent<Storage>().TriggerColliderList.Add(_turretTriggerDetectorCollider.GetHashCode());
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
        if (other.gameObject.CompareTag(Storage.PlayerTag))
        {
            Storage.ToLog(this, Storage.GetCallerName(), "Player DETECTED");
            _isPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Storage.PlayerTag))
        {
            Storage.ToLog(this, Storage.GetCallerName(), "Player MISSED");
            _isPlayerDetected = false;
        }
    }

    private void StandbyMode()
    {
        _turretTransform.rotation = Quaternion.Euler(0, _turretTransform.rotation.eulerAngles.y + _turretSearchSpeed, 0);
        if (IsInvoking(_fireMethodName))
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
            if (!IsInvoking(_fireMethodName))
                InvokeRepeating(_fireMethodName, 0, 60 / _rateOfFire);
        }
        else
        {
            if (IsInvoking(_fireMethodName))
                CancelInvoke();
        }
    }

    /// <summary>
    /// Do not forget to change value of variable _fireMethodName with name of thos method
    /// </summary>
    private void Fire()
    {
        Storage.ToLog(this, Storage.GetCallerName(), "Left gun");
        _bulletCloneScript = Instantiate(_bullet, _bulletStartLeft.position, _bulletStartLeft.rotation, _bulletParentTransform).GetComponent<BulletControllerRay>();
        _bulletCloneScript.BulletSpeed = _bulletSpeed;
        _bulletCloneScript.BulletDamage = _bulletDamage;

        Storage.ToLog(this, Storage.GetCallerName(), "Right gun");
        _bulletCloneScript = Instantiate(_bullet, _bulletStartRight.position, _bulletStartLeft.rotation, _bulletParentTransform).GetComponent<BulletControllerRay>();
        _bulletCloneScript.BulletSpeed = _bulletSpeed;
        _bulletCloneScript.BulletDamage = _bulletDamage;
    }
}
