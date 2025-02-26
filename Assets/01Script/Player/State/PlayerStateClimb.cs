using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateClimb : PlayerState
{
    private float _moveSpeed = 5f; //움직임 속도
    private Vector3 _moveVector;
    public PlayerStateClimb(string animationName, PlayerStateMachin stateMachin, Player player) : base(animationName, stateMachin, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.Rigidbody.useGravity = false;

        PlayerInput.Instance.OnJump += JumpState;
        PlayerInput.Instance.OnMove += Move;
        ToolUseBtn.OnUseTool += UseTool;
    }

    private void UseTool(ToolSO tool)
    {
        _stateMachin.ChangeState(PlayerStateEnum.Use);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_player.CheckLadder())
        {
            _stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        _player.Rigidbody.velocity = _player.transform.TransformDirection(_moveVector) * _moveSpeed;
    }
    private void Move(Vector2 value)
    {
        if (value == Vector2.zero)
        {
            _moveVector = Vector3.zero;
            //_stateMachin.ChangeState(PlayerStateEnum.Idle);
        }
        _moveVector = new Vector3(0, value.y,0);
    }

    private void JumpState()
    {
        _stateMachin.ChangeState(PlayerStateEnum.Jump);
    }

    public override void Exit()
    {
        base.Exit();

        _player.Rigidbody.useGravity = true;

        PlayerInput.Instance.OnJump -= JumpState;
        PlayerInput.Instance.OnMove -= Move;
        ToolUseBtn.OnUseTool -= UseTool;
    }
}
