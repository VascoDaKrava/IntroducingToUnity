using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Controller : MonoBehaviour
{
    [SerializeField] private GameObject _bombC4;
    private ExplosionController _C4;

    private bool _bombPlanted = false;

    private int _damage = 100;
    private float _radiusOfExplosion = 15f;
    private float _timeToExplosion = 10f; // In seconds
    private float _pushForce = 2000f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_bombPlanted)
        {
            if (other.GetComponent<PlayerInteraction>().IsInInventory(LootClass.WeaponNames.C4))
            {
                other.GetComponent<PlayerInteraction>().RemoveFromInventory(LootClass.WeaponNames.C4);
                _C4 = Instantiate(_bombC4, other.transform.position, Quaternion.identity, this.transform).GetComponent<ExplosionController>();
                _C4.Damage = _damage;
                _C4.ExplosionRadius = _radiusOfExplosion;
                _C4.TimeToExplosion = _timeToExplosion;
                _C4.ExplosionForce = _pushForce;

                Storage.ToLog(this, Storage.GetCallerName(), "Bomb has been planted!");

                _bombPlanted = true;

                _C4.LetBoom(gameObject);
            }
            else
                Storage.ToLog(this, Storage.GetCallerName(), "No bomb for planted");
        }
    }
}
