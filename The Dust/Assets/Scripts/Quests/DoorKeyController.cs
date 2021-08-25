using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyController : MonoBehaviour
{
    private Transform _leftDoorTransform;
    private Transform _rightDoorTransform;

    private int _openAngle = 120;

    private float _openSpeed = 60f; // Degree per second

    private bool _isClosed = true;
    private bool _needOpen = false;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Component item in gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag("DoorLeft")) _leftDoorTransform = item.transform;
            if (item.CompareTag("DoorRight")) _rightDoorTransform = item.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_needOpen) OpenDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _isClosed)
        {
            if (other.GetComponent<PlayerInteraction>().IsInInventory(LootClass.LootTypes.Key))
            {
                other.GetComponent<PlayerInteraction>().RemoveFromInventory(LootClass.LootTypes.Key);
                _needOpen = true;
            }
            else
                Storage.ToLog(this, Storage.GetCallerName(), "No keys");
        }
    }

    /// <summary>
    /// Rotate doors
    /// </summary>
    private void OpenDoor()
    {
        if (_isClosed)
            if (_openAngle - Storage.ToAngleY(_leftDoorTransform) <= Time.deltaTime * _openSpeed)
            {
                _isClosed = false;
                _needOpen = false;
            }
            else
            {
                _leftDoorTransform.Rotate(Vector3.up * Time.deltaTime * _openSpeed, Space.Self);
                _rightDoorTransform.Rotate(Vector3.down * Time.deltaTime * _openSpeed, Space.Self);
            }
    }
}
