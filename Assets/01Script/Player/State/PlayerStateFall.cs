using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFall : PlayerState
{
    public PlayerStateFall(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.Acceleration(0);
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

        _player.Acceleration(0);

        if (_player.CheckGround())
        {
            _player.Acceleration(5);
            _stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
        if (_player.CheckLadder())
        {
            _player.Acceleration(5);
            _stateMachin.ChangeState(PlayerStateEnum.climb);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        _player.Rigidbody.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
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
