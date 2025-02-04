using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateJump : PlayerState
{
    private float _JumpSpeed = 5f; //점프 속도

    private Vector2 _playerPosition;
    public PlayerStateJump(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _playerPosition = Vector2.zero;

        _player.Rigidbody.AddForce(Vector3.up * _JumpSpeed, ForceMode.Impulse);
        PlayerInput.Instance.OnMove += Move;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_playerPosition.y > _player.transform.position.y)
        {
            _stateMachin.ChangeState(PlayerStateEnum.Fall);
        }
        _playerPosition = _player.transform.position;
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
