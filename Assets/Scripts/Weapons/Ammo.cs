using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ammo : ScriptableObject
{
    [Range(1f, 1000f)]
    public float speed = 100f;
    
    [Range(1f,100f)]
    public float damage = 25f;
}
