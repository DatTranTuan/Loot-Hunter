using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState
    {
        get; private set;
        // lay gia tri cua no cong khai, doc duoc tu ben ngoai va thiet lap ben trong 
    }
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    // khoi tạo trạng thái ban đầu của người chơi 
    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
