using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "ScriptableObject/EnemyStats")]

public class EnemyStats : ScriptableObject
{
    public string EnemyName;
    public int EnemyID;
    public int scarpValue;
    public int Hp;
    public float WeaponDamage;
    public float WeaponRange;
    public float ShipSpeed;
    public float ShipTurnRate;
    public float CoreDamage;
}
