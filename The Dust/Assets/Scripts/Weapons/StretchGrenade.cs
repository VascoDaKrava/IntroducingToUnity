using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchGrenade : MonoBehaviour
{
    private ExplosionController _grenade;
    
    private int _damage = 50;
    private float _explosionRadius = 8f;
    private float _pushForce = 2000f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        _grenade = Storage.FindTransformInChildrenWithTag(transform.parent.gameObject, Storage.WeaponTag).GetComponent<ExplosionController>();
        _grenade.Damage = _damage;
        _grenade.ExplosionRadius = _explosionRadius;
        _grenade.ExplosionForce = _pushForce;

        _grenade.LetBoom(transform.parent.gameObject);
    }
}
