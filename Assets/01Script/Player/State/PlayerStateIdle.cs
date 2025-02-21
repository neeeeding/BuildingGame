using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public PlayerStateIdle(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerInput.Instance.OnJump += JumpState;
        PlayerInput.Instance.OnMove += Move;
        ToolUseBtn.OnUseTool += UseTool;
    }

    private void UseTool()
    {
        _stateMachin.ChangeState(PlayerStateEnum.Use);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_player.CheckGround())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Fall);
        }
        if (_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.climb);
        }
    }

    private void Move(Vector2 value)
    {
        if(value != Vector2.zero)
        {
            _stateMachin.ChangeState(PlayerStateEnum.Walk);
        }
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
