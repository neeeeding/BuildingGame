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

    private void Awake()
    {
        playerInput = new KeyInputAction();
        playerInput.PlayerInputAction.Enable();
        playerInput.PlayerInputAction.Jump.performed += Jump;
        playerInput.PlayerInputAction.ClickAction.performed += Click;
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
