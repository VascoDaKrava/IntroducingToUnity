using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    private bool _isEmpty = false;
    private bool _needOpen = false;
    private bool _isLockOpen = false;
    private bool _isOpen = false;

    private float _openTopSpeed = 60f; // Degree per second
    private float _openLockSpeed = 180f; // Degree per second

    private Transform _lockTransform;
    private int _lockOpenAngle = 250;

    private Transform _topTransform;
    private int _topOpenAngle = 75;

    [SerializeField] private GameObject _lootAsset;

    private GameObject _lootToTransfer;

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
            if (_isOpen && !_isEmpty) TransferContent(other);
        }
    }

    private void OpenBox()
    {
        // Open Lock
        if (!_isLockOpen)
            if (_lockOpenAngle - Storage.ToAngleX(_lockTransform) <= Time.deltaTime * _openLockSpeed)
            {
                _isLockOpen = true;
                _lootToTransfer = Instantiate(_lootAsset, transform);
            }
            else
                _lockTransform.Rotate(Vector3.right * Time.deltaTime * _openLockSpeed, Space.Self);

        // Open Top if Lock is open
        if (_isLockOpen && !_isOpen)
            if (_topOpenAngle - Storage.ToAngleX(_topTransform) <= Time.deltaTime * _openTopSpeed)
                _isOpen = true;

            else
                _topTransform.Rotate(Vector3.right * Time.deltaTime * _openTopSpeed, Space.Self);
    }

    private void TransferContent(Collider other)
    {
        other.GetComponent<PlayerInteraction>().GetLoot(_lootToTransfer);
        Debug.Log("Ammo - Transfered");
        _isEmpty = true;
    }
}
