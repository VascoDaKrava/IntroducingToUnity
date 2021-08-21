using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private bool _isEmpty = false;
    private bool _needOpen = false;
    private bool _isOpen = false;

    private float _openSpeed = 0.1f; // Radian per second

    private Transform _lockTransform;
    private Transform _topTransform;
            
    // Start is called before the first frame update
    void Start()
    {
        foreach (Component item in gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag("AmmoBoxLockTag")) _lockTransform = item.transform;
            if (item.CompareTag("AmmoBoxTopTag")) _topTransform = item.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_needOpen) OpenBox();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isEmpty)
        {
            Debug.Log("Ammo - " + other.isTrigger + " / " + other);
            if (!_isOpen)
            {
                _needOpen = true;
                return;
            }
            if (_isOpen && !_isEmpty) TransferContent();
        }
    }

    private void OpenBox()
    {
        Debug.Log("Ammo - Opened");
        Debug.Log("Ammo - lock " + _lockTransform.tag);
        Debug.Log("Ammo - top " + _topTransform.tag);
        _lockTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_lockTransform.forward, gameObject.transform.up - _lockTransform.position, _openSpeed * Time.deltaTime, 0));
        _isOpen = true;
        _needOpen = false;
    }

    private void TransferContent()
    {
        Debug.Log("Ammo - Transfered");
        _isEmpty = true;
    }
}
