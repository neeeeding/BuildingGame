using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Singleton<PlayerInput>
{
    private KeyInputAction playerInput;
    public Action<Vector2> OnMove;
    public Action OnJump;
    public Action<bool> OnRotate;
    public Action<bool> OnFast;

    private void Awake()
    {
        playerInput = new KeyInputAction();
        playerInput.PlayerInputAction.Enable();
        playerInput.PlayerInputAction.Jump.performed += Jump;

        playerInput.PlayerInputAction.noRatate.performed += context => OnRotate?.Invoke(false);
        playerInput.PlayerInputAction.noRatate.canceled += context => OnRotate?.Invoke(true) ;

        playerInput.PlayerInputAction.Speed.performed += context => OnFast?.Invoke(true);
        playerInput.PlayerInputAction.Speed.canceled += context => OnFast?.Invoke(false);
    }

    private void Update()
    {
        Vector2 inputVector = playerInput.PlayerInputAction.Move.ReadValue<Vector2>();
        OnMove?.Invoke(inputVector);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
