using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Vector3 _inputMoveDirection;
    private Vector3 _inputLookDirection;
    private Vector3 _grounCheckBoxHalfSize;
    private Rigidbody _playerRigidbody;
    private Transform _playerTransform;
    private Transform _cameraTransform;
    private float _speedWalk = 2f;
    private float _speedRun = 5f;
    private float _yRot;
    private float _jumpCheckBoxY = 0.1f;
    private float _jumpCheckBoxX = 0.4f;
    private float _jumpImpulseScale = 3f;
    private int _jumpCheckLayerMask = 1 << 0;
    private bool _isSpeedUp;
    private bool _isJump;

    // Start is called before the first frame update
    void Start()
    {
        _grounCheckBoxHalfSize = new Vector3(_jumpCheckBoxX / 2, _jumpCheckBoxY / 2, _jumpCheckBoxX / 2);
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerTransform = GetComponent<Transform>();
        _cameraTransform = _camera.transform;
        _inputLookDirection = _cameraTransform.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        CameraRotate();
        PlayerMove();
        PlayerJump();
    }

    /// <summary>
    /// Get inputs
    /// </summary>
    private void GetInput()
    {
        _inputMoveDirection.x = Input.GetAxis("Horizontal");
        _inputMoveDirection.y = 0f;
        _inputMoveDirection.z = Input.GetAxis("Vertical");

        _yRot = Input.GetAxisRaw("Mouse X");

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
        //float deltaX = _inputMoveDirection.x * (_isSpeedUp ? _speedRun : _speedWalk) * Time.deltaTime;
        //float deltaZ = _inputMoveDirection.z * (_isSpeedUp ? _speedRun : _speedWalk) * Time.deltaTime;
        //float alfaRad = Mathf.Deg2Rad * (90 - _playerRigidbody.rotation.y);
        //float Xs = Mathf.Sin(alfaRad) * deltaX;
        //float Zs = Mathf.Cos(Mathf.Deg2Rad * _playerRigidbody.rotation.y);
        //Vector3 As = new Vector3(Xs, 0f, Zs);
        //_playerRigidbody.MovePosition(_playerRigidbody.position + As);

        if (_inputMoveDirection != Vector3.zero)
            _playerRigidbody.MovePosition(_playerRigidbody.position +
                (_playerTransform.right * _inputMoveDirection.x + _playerTransform.forward * _inputMoveDirection.z).normalized *
                (_isSpeedUp ? _speedRun : _speedWalk) * Time.deltaTime);

        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0f, _yRot, 0f));
    }

    private void PlayerJump()
    {
        if (!_isJump) return;

        // Check for groung
        if (!Physics.CheckBox(
            _playerTransform.position + Vector3.down * _jumpCheckBoxY / 2f,
            _grounCheckBoxHalfSize,
            Quaternion.identity,
            _jumpCheckLayerMask,
            QueryTriggerInteraction.Ignore)
            ) return;

        _playerRigidbody.AddForce(Vector3.up * _playerRigidbody.mass * _jumpImpulseScale, ForceMode.Impulse);
        Storage.ToLog(this, Storage.GetCallerName(), "Now Jump!");
    }
}
