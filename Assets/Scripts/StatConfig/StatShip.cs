using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipStats", menuName = "ScriptableObject/StatShip")]

public class StatShip : ScriptableObject
{
    public string ShipName;
    public int ShipID;
    public int Hp;
    public float WeaponDamage;
    public float WeaponRange;
    public float ShipSpeed;
    public float ShipTurnRate;
    public float CoreDamage;
}
