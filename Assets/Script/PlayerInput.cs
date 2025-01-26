using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private KeyInputAction playerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerInput = new KeyInputAction();
        playerInput.PlayerInputAction.Enable();
        playerInput.PlayerInputAction.Jump.performed += Jump;
    }

    private void Update()
    {
        Vector2 inputVector = playerInput.PlayerInputAction.Move.ReadValue<Vector2>();
        float speed = 20f;
        _rigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
