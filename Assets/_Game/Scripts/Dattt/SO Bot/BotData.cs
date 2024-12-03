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
    Cultist = 6,
    EvilTree = 7,
    NightBone = 8,
    MageSkeleton = 9,
    MetalMonster = 10,
    DeathBringer = 11,
}

[Serializable]
public class BotData 
{
    public string name;
    public BotType botType;
    public float maxHealth;
    public float handDmgDeal;
    public float bulletDmgDeal;
}
