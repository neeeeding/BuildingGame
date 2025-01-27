using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateWalk : PlayerState
{
    private float _moveSpeed = 5f; //움직임 속도
    private Vector3 _moveVector;

    public PlayerStateWalk(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerInput.Instance.OnJump += JumpState;
        PlayerInput.Instance.OnMove += Move;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _player.Rigidbody.velocity = _player.transform.TransformDirection(_moveVector) * _moveSpeed;
    }
    private void Move(Vector2 value)
    {
        if(value == Vector2.zero)
        {
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
    }

}
