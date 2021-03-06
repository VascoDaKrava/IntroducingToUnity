using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform _leftDoorTransform;
    private Transform _rightDoorTransform;

    private List<ParticleSystem> _fxList = new List<ParticleSystem>();

    private int _openAngle = 120;

    private float _openSpeed = 60f; // Degree per second

    private bool _isClosed = true;
    private bool _needOpen = false;

    public bool NeedOpen { get { return _needOpen; } set { _needOpen = value; } }

    public bool IsClosed { get { return _isClosed; } }

    public int OpenAnge { get { return _openAngle; } set { _openAngle = value; } }


    // Start is called before the first frame update
    void Start()
    {
        foreach (Component item in gameObject.GetComponentsInChildren(typeof(Transform)))
        {
            if (item.CompareTag(Storage.DoorLeftTag)) _leftDoorTransform = item.transform;
            if (item.CompareTag(Storage.DoorRightTag)) _rightDoorTransform = item.transform;
            if (item.CompareTag(Storage.ParticleSystemTag)) _fxList.Add(item.GetComponent<ParticleSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_needOpen) OpenDoor();
    }
      
    /// <summary>
    /// Rotate doors
    /// </summary>
    private void OpenDoor()
    {
        if (_isClosed)
            if (Mathf.Abs(_openAngle - Storage.ToAngleY(_leftDoorTransform)) <= Time.deltaTime * _openSpeed)
            {
                _isClosed = false;
                _needOpen = false;
                foreach (var item in _fxList) item.Stop();
            }
            else
            {
                _leftDoorTransform.Rotate(Vector3.up * Time.deltaTime * _openSpeed, Space.Self);
                _rightDoorTransform.Rotate(Vector3.down * Time.deltaTime * _openSpeed, Space.Self);
            }
    }

    /// <summary>
    /// Immediately destroy doors
    /// </summary>
    public void DestroyDoors()
    {
        Destroy(_leftDoorTransform);
        Destroy(_rightDoorTransform);
        Storage.ToLog(this, Storage.GetCallerName(), "Doors has been destroyed!");
    }
}
