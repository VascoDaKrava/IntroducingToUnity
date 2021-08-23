using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    private bool _isEmpty = false;
    private bool _needOpen = false;
    private bool _isLockOpen = false;
    private bool _isOpen = false;

    private float _openSpeed = 60f; // Degree per second

    private Transform _lockTransform;
    private int _lockOpenAngle = 250;

    private Transform _topTransform;
    private int _topOpenAngle = 75;

    //private Storage

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
        // Open Lock
        if (!_isLockOpen)
            if (_lockOpenAngle - ToAngleX(_lockTransform) <= Time.deltaTime * _openSpeed)
                _isLockOpen = true;
            else
                _lockTransform.Rotate(Vector3.right * Time.deltaTime * _openSpeed, Space.Self);

        // Open Top if Lock is open
        if (_isLockOpen && !_isOpen)
            if (_topOpenAngle - ToAngleX(_topTransform) <= Time.deltaTime * _openSpeed)
                _isOpen = true;
            else
                _topTransform.Rotate(Vector3.right * Time.deltaTime * _openSpeed, Space.Self);
    }

    private void TransferContent()
    {
        Debug.Log("Ammo - Transfered");
        _isEmpty = true;
    }

    /// <summary>
    /// Translate X-rotation angle of transform to humanity-like variant (0-360 degree)
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private float ToAngleX(Transform transform)
    {
        return transform.eulerAngles.x > 90 ?
                Mathf.Abs(transform.eulerAngles.z * 3 - transform.eulerAngles.x) :
                Mathf.Abs(transform.eulerAngles.z - transform.eulerAngles.x);
    }
}
