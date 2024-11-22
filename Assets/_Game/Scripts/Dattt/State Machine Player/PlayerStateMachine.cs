using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStateMachine 
{
    [SerializeField] private List<IStatePlayer> activePStates = new List<IStatePlayer>();

    private PlayerControl playerControl;

    public List<IStatePlayer> ActivePStates { get => activePStates; set => activePStates = value; }

    public PlayerStateMachine(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
    }

    // include(State): Add, Remove, Change, Update, Exit;

    public void AddState(IStatePlayer newState)
    {
        if (activePStates == null)
        {
            activePStates = new List<IStatePlayer>();
        }

        if (!activePStates.Contains(newState))
        {
            activePStates.Add(newState);
        }
    }

    public void RemoveState(IStatePlayer newState)
    {
        if (activePStates.Contains(newState))
        {
            activePStates.Remove(newState);
        }
    }

    public void ChangeState (IStatePlayer newState)
    {
        if (activePStates.Contains(newState))
        {
            newState.Enter();
        }
    }

    public IStatePlayer GetState (Type stateType)
    {
        foreach (IStatePlayer state in activePStates)
        {
            if (state.GetType() == stateType)
            {
                return state;
            }
        }

        return null;
    }

    public void Update ()
    {
        for (int i = activePStates.Count - 1; i >= 0; i--)
        {
            IStatePlayer state = activePStates[i];

            state.Update();
        }
    }

    public void Exit (IStatePlayer state)
    {
        if (state is PS_Idle)
        {
            RemoveState(GetState(typeof(PS_Idle)));
        }

        if (state is PS_Moving)
        {
            RemoveState(GetState(typeof(PS_Moving)));
        }

        if (state is PS_Jump)
        {
            RemoveState(GetState(typeof(PS_Jump)));
        }

        if (state is PS_Roll)
        {
            RemoveState(GetState(typeof(PS_Roll)));
        }

        if (state is PS_Attack)
        {
            RemoveState(GetState(typeof(PS_Attack)));
        }

        if (state is PS_TakeHit)
        {
            RemoveState(GetState(typeof(PS_TakeHit)));
        }

        if (state is PS_Poison)
        {
            RemoveState(GetState(typeof(PS_Poison)));
        }
    }
}
