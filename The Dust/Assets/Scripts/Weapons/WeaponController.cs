using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _rateOfFire;// 100 Shots per second (600)
    [SerializeField] private float _bulletSpeed;// 50 Units per second (910)
    [SerializeField] private int _bulletDamage;// 10 points
    //[SerializeField] private int _clipCapacity;// 30 bullets - ToDo

    private int _clipFullness = 0;

    private bool _readyForFire = true;

    private Transform _bulletStartTransform;
    private Transform _bulletParentTransform;

    private BulletControllerRay _bulletCloneScript;

    private AudioSource _audioSource;

    public bool ReadyForFire { get { return _readyForFire; } }

    public int ClipFullness { 
        get { return _clipFullness; } 
        set { _clipFullness = value;
            if (value > 0) _readyForFire = true;
        } }

    // Start is called before the first frame update
    void Start()
    {
        _bulletStartTransform = Storage.FindTransformInChildrenWithTag(gameObject, Storage.Bullet1StartPositionTag);
        _bulletParentTransform = GameObject.FindGameObjectWithTag(Storage.DynamicallyCreatedTag).transform;
        _audioSource = Storage.FindTransformInChildrenWithTag(gameObject, Storage.AudioSourceTag).GetComponent<AudioSource>();
    }
     
    public void Fire()
    {
        if (_clipFullness == 0)
        {
            _readyForFire = false;
            Storage.ToLog(this, Storage.GetCallerName(), "Can't fire. Clip is empty");
            StartCoroutine(WaitForNextShot(60f / _rateOfFire));
            return;
        }

        _bulletCloneScript = Instantiate(_bullet, _bulletStartTransform.position, _bulletStartTransform.rotation, _bulletParentTransform).GetComponent<BulletControllerRay>();
        _bulletCloneScript.BulletStartSpeed = _bulletSpeed;
        _bulletCloneScript.BulletDamage = _bulletDamage;

        _audioSource.Play();

        _clipFullness--;

        Storage.ToLog(this, Storage.GetCallerName(), "Fire 1. Clip fullness: " + ClipFullness);

        _readyForFire = false;

        StartCoroutine(WaitForNextShot(60f / _rateOfFire));
    }

    /// <summary>
    /// Wait for "seconds" for set readyForFire to true
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator WaitForNextShot(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _readyForFire = true;
    }
}
