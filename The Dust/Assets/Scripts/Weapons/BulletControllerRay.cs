using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerRay : MonoBehaviour
{
    private Transform _bulletTransform;

    private RaycastHit _hitObj;

    private bool _isRayDetectSome = false;
    private int _mask = 1 << 0; // Layer 0 = Default
    private float _bulletLength = 0.04f;

    public float BulletSpeed { get; set; }
    public int BulletDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _bulletTransform = gameObject.transform;
    }


    private void FixedUpdate()
    {
        CheckHit();
        _bulletTransform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }

    private void CheckHit()
    {
        _isRayDetectSome = Physics.Raycast(_bulletTransform.position, _bulletTransform.forward, out _hitObj, _bulletLength, _mask, QueryTriggerInteraction.Ignore);
        if (_isRayDetectSome)
        {
            Storage.ToLog(this, Storage.GetCallerName(), "Hit " + _hitObj.collider.transform.name);
            
            // Apply Damage
            _hitObj.transform.GetComponent<HealthController>()?.ChangeHealth(-BulletDamage);
            Destroy(gameObject);
        }
    }
}