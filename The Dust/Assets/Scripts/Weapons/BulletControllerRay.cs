using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerRay : MonoBehaviour
{
    private Transform _bulletTransform;
    private Rigidbody _bulletRigidbody;

    private RaycastHit _hitObj;

    private int _mask = 1 << 0; // Layer 0 = Default
    private float _bulletLength = 0.001f;
    private float _magicNumber = 4f;
    private bool _isRayActive = true;

    public float BulletStartSpeed { get; set; }
    public float BulletLength { get { return _bulletLength; } set { _bulletLength = value; } }
    public int BulletDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //BulletStartSpeed = 1f;

        _bulletTransform = gameObject.transform;
        _bulletRigidbody = gameObject.GetComponent<Rigidbody>();

        CheckHit();

        _bulletRigidbody.AddForce(_bulletTransform.forward * BulletStartSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        CheckHit();
        //_bulletTransform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    private void CheckHit()
    {
        if (!_isRayActive) return;

        if (Physics.Raycast(_bulletTransform.position, _bulletTransform.forward, out _hitObj, _magicNumber, _mask, QueryTriggerInteraction.Ignore))
        {
            Storage.ToLog(this, Storage.GetCallerName(), "Hit " + _hitObj.collider.transform.name + ". Distance : " + _hitObj.distance);
            _isRayActive = false;
            StartCoroutine(WaitForContact((_hitObj.distance - BulletLength) / _bulletRigidbody.velocity.magnitude));
        }
    }

    IEnumerator WaitForContact(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Apply Damage
        _hitObj.transform.GetComponent<HealthController>()?.ChangeHealth(-BulletDamage);
        Destroy(gameObject);
    }
}