using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerRay : MonoBehaviour
{
    private Transform _bulletTransform;
    private Rigidbody _bulletRigidbody;

    private RaycastHit _hitObj;

    private int _mask = ~0; // All layers
    private float _bulletLength = 0.001f;
    private bool _isRayActive = true;

    public float BulletStartSpeed { get; set; }
    public float BulletLength { get { return _bulletLength; } set { _bulletLength = value; } }
    public int BulletDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _bulletTransform = gameObject.transform;
        _bulletRigidbody = gameObject.GetComponent<Rigidbody>();

        CheckHit(Time.deltaTime * BulletStartSpeed);

        _bulletRigidbody.AddForce(_bulletTransform.forward * BulletStartSpeed, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        CheckHit(Time.deltaTime * BulletStartSpeed);
    }

    private void CheckHit(float maxHitDistance)
    {
        if (!_isRayActive) return;

        if (Physics.Raycast(_bulletTransform.position, _bulletTransform.forward, out _hitObj, maxHitDistance, _mask, QueryTriggerInteraction.Ignore))
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