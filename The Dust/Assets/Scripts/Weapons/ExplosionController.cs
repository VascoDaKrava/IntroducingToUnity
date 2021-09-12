using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosionController : MonoBehaviour
{
    private GameObject _bomb;

    private Vector3 _directionToObjInRaius = Vector3.zero;

    private int _mask = ~0; // All layers for damaging and pushing
    private int _damage;
    private float _explosionRadius;
    private float _timeToExplosion = 0f;
    private float _force;

    private string _boomMethodName = "MakeExplosion";

    private AudioSource _audioSource;
    private ParticleSystem _explosionFX;
    private float _timeForFX = 1.7f;// Length of Audio and Partical system clip to play, before object was destroyed

    /// <summary>
    /// Damage of explosion
    /// </summary>
    public int Damage { get { return _damage; } set { _damage = value; } }

    /// <summary>
    /// Raduis for push and damage of explosion
    /// </summary>
    public float ExplosionRadius { get { return _explosionRadius; } set { _explosionRadius = value; } }

    /// <summary>
    /// Time to explosion
    /// </summary>
    public float TimeToExplosion { get { return _timeToExplosion; } set { _timeToExplosion = value; } }

    /// <summary>
    /// Push power
    /// </summary>
    public float ExplosionForce { get { return _force; } set { _force = value; } }


    /// <summary>
    /// Make explosion "bomb" with pause if need
    /// </summary>
    /// <param name="bomb">GameObject to explosion</param>
    public void LetBoom(GameObject bomb)
    {
        _bomb = bomb;
        Invoke(_boomMethodName, _timeToExplosion);
    }

    /// <summary>
    /// Make explosion
    /// Do not forget to change value of variable _boomMethodName with name of this method
    /// </summary>
    private void MakeExplosion()
    {
        foreach (Collider item in
            Physics.OverlapSphere(
                transform.position,
                ExplosionRadius,
                _mask,
                QueryTriggerInteraction.Ignore))
        {
            // If collider is enemy with navigation, disable navigation and add Rigidbody
            //if (item.CompareTag(Storage.EnemyNavigatedTag))
            //{
            //    item.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            //    item.gameObject.AddComponent(typeof(Rigidbody));
            //    item.gameObject.GetComponent<Rigidbody>().mass = 90f;
            //}

            // Check for Push
            if (item.attachedRigidbody != null) // If have rigidbody - pushing
            {
                _directionToObjInRaius = item.attachedRigidbody.centerOfMass + item.attachedRigidbody.position - transform.position;

                item.attachedRigidbody.AddForce(_directionToObjInRaius.normalized * (_explosionRadius - _directionToObjInRaius.magnitude) / _explosionRadius * _force, ForceMode.Impulse);
            }
            else _directionToObjInRaius = item.transform.position - transform.position; // If do not have rigidbody - do not pushing, but calculate percent of damage

            // Apply damage, if posible
            item.GetComponent<HealthController>()?.ChangeHealth(-Mathf.CeilToInt(Damage * (_explosionRadius - _directionToObjInRaius.magnitude) / _explosionRadius));

            //if (item.CompareTag(Storage.EnemyNavigatedTag))
            //{
            //    Destroy(item.gameObject.GetComponent(typeof(Rigidbody)));
            //    item.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            //}
        }
        Storage.ToLog(this, Storage.GetCallerName(), "B O O M!");
        _audioSource.Play();
        _explosionFX.Play();
        Destroy(_bomb, _timeForFX);
    }

    private void Start()
    {
        _audioSource = Storage.FindTransformInChildrenWithTag(gameObject, Storage.AudioSourceTag).GetComponent<AudioSource>();
        _explosionFX = Storage.FindTransformInChildrenWithTag(gameObject, Storage.ParticleSystemTag).GetComponent<ParticleSystem>();
    }
}