using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player _player;
    protected PlayerStateMachin _stateMachin;
    protected int _animationHash;
    public string animationName;

    public PlayerState(string animationName, PlayerStateMachin stateMachin, Player player)
    {
        this.animationName = animationName;
        _animationHash = Animator.StringToHash(animationName);
        _player = player;
        _stateMachin = stateMachin;
    }
    public virtual void Enter()
    {
        _player.Animator.SetBool(_animationHash, true);
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animationHash, false);
    }

    public virtual void UpdateState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }
}

public enum PlayerStateEnum
{
    Idle, Walk, Jump, Fall, climb, PickUp, Use, None
}
