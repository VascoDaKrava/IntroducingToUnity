using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Vector3 _inputMoveDirection;
    private Vector3 _inputLookDirection;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private int _speedWalk = 2;
    private int _speedRun = 5;
    private bool _isSpeedUp;
    private bool _isJump;
    
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
        GetInput();
        CameraRotate();
        PlayerMove();
    }

    /// <summary>
    /// Get inputs
    /// </summary>
    private void GetInput()
    {
        _inputMoveDirection.x = Input.GetAxis("Horizontal");
        _inputMoveDirection.z = Input.GetAxis("Vertical");
        _inputMoveDirection.Normalize();

        _inputLookDirection.x += Input.GetAxis("Mouse X");
        _inputLookDirection.y -= Input.GetAxis("Mouse Y");

        _isSpeedUp = Input.GetButton("SpeedUp");

        _isJump = Input.GetButton("Jump");
    }

    /// <summary>
    /// Change cameras angles
    /// </summary>
    private void CameraRotate()
    {
        if (_inputLookDirection.y > 65) _inputLookDirection.y = 65;
        if (_inputLookDirection.y < -65) _inputLookDirection.y = -65;
        
        _cameraTransform.rotation = Quaternion.Euler(_inputLookDirection.y, _inputLookDirection.x, 0f);
    }

    /// <summary>
    /// Move and rotate player
    /// </summary>
    private void PlayerMove()
    {
        _playerTransform.rotation = Quaternion.Euler(0f, _inputLookDirection.x, 0f);

        _playerTransform.Translate(_inputMoveDirection * Time.deltaTime * (_isSpeedUp ? _speedRun : _speedWalk) );
    }
}
