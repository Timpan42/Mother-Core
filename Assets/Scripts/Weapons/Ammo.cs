using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AmmoConfig")]
public class Ammo : ScriptableObject
{
    public string AmmoName;
    public int AmmoID;

    [Range(1f, 1000f)]
    public float speed = 100f;

    [Range(0f, 100f)]
    public float damage = 0f;
}
