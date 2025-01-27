using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachin
{
    public Dictionary<PlayerStateEnum, PlayerState> PStateD = new Dictionary<PlayerStateEnum, PlayerState>();
    public PlayerState currentState;


    public void Init(PlayerStateEnum state)
    {
        ChangeState(state);
    }

    public void ChangeState(PlayerStateEnum state)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        currentState = PStateD[state];
        currentState.Enter();
    }

    public void AddState(PlayerStateEnum type, PlayerState state)
    {
        PStateD.Add(type, state);
    }
}
