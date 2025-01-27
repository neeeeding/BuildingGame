using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string currentState;

    private Animator animator;
    public Animator Animator => animator;
    private Rigidbody rigid;
    public Rigidbody Rigidbody => rigid;

    private PlayerStateMachin stateMachin;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();

        stateMachin = new PlayerStateMachin();
        stateMachin.AddState(PlayerStateEnum.Idle,new PlayerStateIdle("Idle",stateMachin,this));
        stateMachin.AddState(PlayerStateEnum.Walk,new PlayerStateWalk("Walk", stateMachin, this));
        stateMachin.AddState(PlayerStateEnum.Jump,new PlayerStateJump("Jump", stateMachin, this));
        //stateMachin.AddState(PlayerStateEnum.Fall,new PlayerStateFall("Fall", stateMachin, this));
        //stateMachin.AddState(PlayerStateEnum.PickUp,new PlayerStatePickUp("PickUp", stateMachin, this));
        //stateMachin.AddState(PlayerStateEnum.Use,new PlayerStateUse("Use", stateMachin, this));
        stateMachin.Init(PlayerStateEnum.Idle);
        currentState = stateMachin.currentState.animationName;
    }

    private void Update()
    {
        stateMachin.currentState.UpdateState();
        currentState = stateMachin.currentState.animationName;
    }

    private void FixedUpdate()
    {
        stateMachin.currentState.FixedUpdateState();
    }
}
