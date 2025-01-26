using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _moveSpeed = 5f; //움직임 속도
    [SerializeField]private float _JumpSpeed = 10f; //점프 속도
    private Vector3 _moveVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerInput.Instance.OnMove += Move;
        PlayerInput.Instance.OnJump += Jump;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveVector * _moveSpeed;
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _JumpSpeed,ForceMode.Impulse);
    }

    private void Move(Vector2 value)
    {
        _moveVector = new Vector3(value.x, 0, value.y);
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnMove -= Move;
        PlayerInput.Instance.OnJump -= Jump;
    }
}
