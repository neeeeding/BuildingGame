using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFall : PlayerState
{
    public PlayerStateFall(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.CheckGround())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
