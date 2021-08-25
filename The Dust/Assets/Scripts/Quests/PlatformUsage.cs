using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUsage : MonoBehaviour
{
    private DoorController _door;
    private BoxCollider _platformCollider;
    private Transform _platformTransform;

    private Vector3 _platformCenterCorrector = new Vector3(1, 0, -1);
    private int _platformSizeScale = 4;
    private int _openAngle = 95;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponent<DoorController>();

        _door.OpenAnge = _openAngle;

        foreach (Component item in gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag(Storage.PlatformTag)) _platformTransform = item.transform;
        }

        _platformCollider = gameObject.AddComponent<BoxCollider>();
        _platformCollider.size = _platformTransform.localScale * _platformSizeScale;
        _platformCollider.center = _platformTransform.localPosition + _platformCenterCorrector;
        _platformCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Storage.PlayerTag) && _door.IsClosed)
        {
            _door.NeedOpen = true;
            Destroy(_platformCollider);
            Destroy(_platformTransform.gameObject);
        }
    }
}
