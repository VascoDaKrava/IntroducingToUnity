using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private Vector2 _input;
    private Vector3 _position;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _position = _rigidbody.position;
    }

    // Update is called once per frame
    void Update()
    {
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");

        //_rigidbody.MovePosition(_rigidbody.position + _input);
        _position.x += _input.x;
        _position.y = 0;
        _position.z += _input.y;
        _rigidbody.MovePosition(_position);
    }
}
