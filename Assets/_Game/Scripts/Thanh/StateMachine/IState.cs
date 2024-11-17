using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(Enemy_Zombie enemy);
    void OnExcute(Enemy_Zombie enemy);
    void OnExit(Enemy_Zombie enemy);
}

