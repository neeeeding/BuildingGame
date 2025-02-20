using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateUse : PlayerState
{
    public PlayerStateUse(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
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

        if (_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.climb);
        }

        if (!_player.CheckGround())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Fall);
        }
    }

    private void JumpState()
    {
        _stateMachin.ChangeState(PlayerStateEnum.Jump);
    }

    private void Move(Vector2 value)
    {
        if (value != Vector2.zero)
        {
            _stateMachin.ChangeState(PlayerStateEnum.Walk);
        }
    }

    public override void Exit()
    {
        base.Exit();
        PlayerInput.Instance.OnJump -= JumpState;
        PlayerInput.Instance.OnMove -= Move;
    }
}
