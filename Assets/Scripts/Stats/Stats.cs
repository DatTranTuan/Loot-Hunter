using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats 
{

    // Stats thong ke
    [SerializeField] private int baseValue;
    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    // tao ham luu suc manh co ban cua player
    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
    }
    //Modifire sua doi
    public void AddModifire(int _modifire)
    {
        modifiers.Add(_modifire);
    }
    public void RemoveModifier(int _modifire)
    {
        modifiers.RemoveAt(_modifire);
    }
}
