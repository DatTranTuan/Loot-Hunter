using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    [SerializeField] private List<IStateNormal> activeStates = new List<IStateNormal>();

    private BotControl_dattt botControl_dattt;

    public List<IStateNormal> ActiveStates { get => activeStates; set => activeStates = value; }

    public StateMachine(BotControl_dattt _botControl)
    {
        botControl_dattt = _botControl;
    }

    public void AddState(IStateNormal newState)
    {
        if (activeStates == null)
        {
            activeStates = new List<IStateNormal>();
        }

        if (!activeStates.Contains(newState))
        {
            activeStates.Add(newState);
            //newState.Enter();
        }
    }

    public void RemoveState(IStateNormal state)
    {
        if (activeStates.Contains(state))
        {
            //state.Exit();
            activeStates.Remove(state);
            //activeStates.Clear();
        }
    }

    public void ChangeState(IStateNormal newState)
    {
        if (activeStates.Contains(newState))
        {
            //activeStates.Add(newState);
            newState.Enter();
        }
    }

    private IStateNormal CreateState(Type stateType)
    {
        if (stateType == typeof(S_Patrol))
        {
            return new S_Patrol(botControl_dattt);
        }

        if (stateType == typeof(S_Wait))
        {
            return new S_Wait(botControl_dattt);
        }

        return null;
    }

    public IStateNormal GetState(Type stateType)
    {
        foreach (IStateNormal state in activeStates)
        {
            if (state.GetType() == stateType)
            {
                return state;
            }
        }

        return null;
    }

    public void Update()
    {
        for (int i = activeStates.Count - 1; i >= 0; i--)
        {
            IStateNormal state = activeStates[i];

            state.Update();
        }
    }

    public void Exit(IStateNormal state)
    {
        if (state is S_Wait)
        {
            RemoveState(GetState(typeof(S_Wait)));
        }

        if (state is S_Patrol)
        {
            RemoveState(GetState(typeof(S_Patrol)));
        }

        if (state is S_Attack)
        {
            RemoveState(GetState(typeof(S_Attack)));
        }

        if (state is S_RangeAttack)
        {
            RemoveState(GetState(typeof(S_RangeAttack)));
        }

        if (state is S_Laser)
        {
            RemoveState(GetState(typeof(S_Laser)));
        }

        if (state is S_TakeHit)
        {
            RemoveState(GetState(typeof(S_TakeHit)));
        }

        if (state is S_Guard)
        {
            RemoveState(GetState(typeof(S_Guard)));
        }

        if (state is S_Death)
        {
            //Debug.Log(activeStates);
            RemoveState(GetState(typeof(S_Death)));
        }

        if (state is S_Shield)
        {
            RemoveState(GetState(typeof(S_Shield)));
        }

        if (state is S_Tele)
        {
            RemoveState(GetState(typeof(S_Tele)));
        }
    }
   
}
