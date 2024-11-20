using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState
    {
        get; private set;
        // lay gia tri cua no cong khai, doc duoc tu ben ngoai va thiet lap ben trong 
    }
    // khai bao trang thai ban dau
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
    // khoi tạo trạng thái ban đầu của người chơi 
    public void ChangeState(EnemyState _newState)
    {

        currentState.Exit();
        currentState = _newState;
        currentState.Enter();

    }
}
