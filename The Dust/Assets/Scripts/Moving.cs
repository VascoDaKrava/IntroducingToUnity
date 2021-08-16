using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Vector3 _inputMoveDirection;
    private Vector3 _inputLookDirection;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private int _speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerTransform = GetComponent<Transform>();
        _cameraTransform = _camera.transform;
        _inputLookDirection = _cameraTransform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        cameraRotate();
        playerMoving();
    }

    /// <summary>
    /// Get inputs
    /// </summary>
    private void getInput()
    {
        _inputMoveDirection.x = Input.GetAxis("Horizontal");
        _inputMoveDirection.z = Input.GetAxis("Vertical");
        _inputMoveDirection.Normalize();

        _inputLookDirection.x += Input.GetAxis("Mouse X");
        _inputLookDirection.y -= Input.GetAxis("Mouse Y");
    }

    /// <summary>
    /// Change cameras angles
    /// </summary>
    private void cameraRotate()
    {
        _cameraTransform.rotation = Quaternion.Euler(_inputLookDirection.y, _inputLookDirection.x, 0f);
    }

    /// <summary>
    /// Move and rotate player
    /// </summary>
    private void playerMoving()
    {
        _playerTransform.rotation = Quaternion.Euler(0f, _inputLookDirection.x, 0f);

        _playerTransform.Translate(_inputMoveDirection * Time.deltaTime * _speed);
    }
}
