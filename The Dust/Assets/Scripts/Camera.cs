using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _playerHeadTransform;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag(Storage.MainCameraTag).transform;
        _playerHeadTransform = GameObject.FindGameObjectWithTag(Storage.PlayerHeadTag).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _cameraTransform.position = _playerHeadTransform.position;
        _cameraTransform.rotation = _playerHeadTransform.rotation;
    }
}
