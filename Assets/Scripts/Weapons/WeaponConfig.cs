using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponXConfig", menuName = "ScriptableObject/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public string WeaponName;
    public int WeaponID;
    public float Damage;
    public float MagazineSize;
    public float ShootCoolDown;
    public float ReloadTimer;
    public Sprite image;
}
