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
        PlayerInput.Instance.OnMove += Move;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.CheckGround())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
        if (_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.climb);
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
    }
}
