using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : PlayerState
{
    private float _moveSpeed = 8f; //움직임 속도

    private float _walkSpeed = 8f;
    private float _FastSpeed = 15f;
    private Vector3 _moveVector;

    public PlayerStateWalk(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.Acceleration(5);
        PlayerInput.Instance.OnJump += JumpState;
        PlayerInput.Instance.OnMove += Move;
        PlayerInput.Instance.OnFast += Fast;
        PlayerInput.Instance.OffFast += NotFast;
        ToolUseBtn.OnUseTool += UseTool;
        if (!_player.CheckGround())
        {
            _player.Acceleration(0);
            _player.Rigidbody.velocity = _player.transform.TransformDirection(_moveVector) * _moveSpeed + new Vector3(0, _player.Rigidbody.velocity.y/1.1f, 0);
        }
    }

    private void UseTool()
    {
        _stateMachin.ChangeState(PlayerStateEnum.Use);
    }

    private void NotFast()
    {
        _moveSpeed = _walkSpeed;
    }

    private void Fast()
    {
        _moveSpeed = _FastSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.climb);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (!_player.CheckGround())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Fall);
        }
        else
        {
            _player.Rigidbody.velocity = _player.transform.TransformDirection(_moveVector) * _moveSpeed + new Vector3(0, _player.Rigidbody.velocity.y, 0);
        }
        _player.Rigidbody.AddForce(Vector3.down * 9.8f * _player.Rigidbody.drag, ForceMode.Acceleration);
    }
    private void Move(Vector2 value)
    {
        if(value == Vector2.zero)
        {
            _moveVector = Vector3.zero;
            _stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
        _moveVector = new Vector3(value.x, 0, value.y);
    }
    private void JumpState()
    {
        _stateMachin.ChangeState(PlayerStateEnum.Jump);
    }

    public override void Exit()
    {
        base.Exit();
        PlayerInput.Instance.OnJump -= JumpState;
        PlayerInput.Instance.OnMove -= Move;
        ToolUseBtn.OnUseTool -= UseTool;
    }

}
