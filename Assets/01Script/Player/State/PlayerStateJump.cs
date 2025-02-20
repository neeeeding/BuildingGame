using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : PlayerState
{
    private float _JumpSpeed = 9f; //점프 속도

    public PlayerStateJump(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.Rigidbody.AddForce(Vector3.up * _JumpSpeed, ForceMode.Impulse);
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

        if (_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.climb);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        if (_player.Rigidbody.velocity.y < 0)
        {
            _stateMachin.ChangeState(PlayerStateEnum.Fall);
        }
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
        PlayerInput.Instance.OnMove -= Move;
        ToolUseBtn.OnUseTool -= UseTool;
    }
}
