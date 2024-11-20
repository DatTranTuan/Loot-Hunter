using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BotType
{
    Zombie = 0,
    Skeleton = 1,
    Goblin = 2,
    Mushroom = 3,
    FlyingEye = 4,
    GolemBoss = 5,
}

[Serializable]
public class BotData 
{
    public string name;
    public BotType botType;
    public float maxHealth;
    public float dmgDeal;
}
