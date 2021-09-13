using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _playerHeadTransform;
    private float _cameraRotationSensMin = 0.1f;
    private float _cameraRotationSensMax = 3f;
    private float _cameraFlySpeed = 7f;
    private float _distanceToChangeSens = 1f;
    private float _cameraRotationSens;

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GameObject.FindGameObjectWithTag(Storage.MainCameraTag).transform;
        _playerHeadTransform = GameObject.FindGameObjectWithTag(Storage.PlayerHeadTag).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(_cameraTransform.position, _playerHeadTransform.position) > _distanceToChangeSens)
            _cameraRotationSens = _cameraRotationSensMin;
        else
            _cameraRotationSens = _cameraRotationSensMax;

        _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, _playerHeadTransform.position, Time.deltaTime * _cameraFlySpeed);
        _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, _playerHeadTransform.rotation, Time.deltaTime * _cameraRotationSens);

    }
}
