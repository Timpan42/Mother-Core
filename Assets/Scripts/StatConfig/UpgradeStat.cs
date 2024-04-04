using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeStat", menuName = "ScriptableObject/UpgradeStat")]

public class UpgradeStat : ScriptableObject
{
    public List<int> Hp;
    public List<float> WeaponDamage;
    public List<float> WeaponRange;
    public List<float> ShipSpeed;
    public List<float> ShipTurnRate;
    public List<float> CoreDamage;
}
