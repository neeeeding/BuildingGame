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
    public Action OnClickMouse;
    public Action OnFast;
    public Action OffFast;

    private void Awake()
    {
        playerInput = new KeyInputAction();
        playerInput.PlayerInputAction.Enable();
        playerInput.PlayerInputAction.Jump.performed += Jump;
        playerInput.PlayerInputAction.ClickAction.performed += Click;
        playerInput.PlayerInputAction.Speed.performed += Fast;
        playerInput.PlayerInputAction.Speed.canceled += NotFast;
    }

    private void NotFast(InputAction.CallbackContext obj)
    {
        OffFast?.Invoke();
    }

    private void Fast(InputAction.CallbackContext obj)
    {
        OnFast?.Invoke();
    }

    private void Click(InputAction.CallbackContext obj)
    {
        OnClickMouse?.Invoke();
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
