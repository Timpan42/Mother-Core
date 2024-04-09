using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCalculateStats : MonoBehaviour
{
    [SerializeField] private StatShip shipStats;
    [SerializeField] private EnemyStats enemyStats;

    [HideInInspector] public int Hp;
    [HideInInspector] public float WeaponDamage;
    [HideInInspector] public float WeaponRange;
    [HideInInspector] public float ShipSpeed;
    [HideInInspector] public float ShipTurnRate;

    private HealthScript healthScript;
    private RotatePlayer rotatePlayer;
    private PlayerMovement playerMovement;
    private WeaponFire weaponFire;
    private WeaponControllerEnemy weaponControllerEnemy;

    private void Start()
    {
        healthScript = transform.GetComponent<HealthScript>();
        weaponFire = transform.GetComponentInChildren<WeaponFire>();
        weaponControllerEnemy = transform.GetComponentInChildren<WeaponControllerEnemy>();

        CalculateStats();
    }

    private void CalculateStats()
    {
        Hp = shipStats.Hp + enemyStats.Hp;
        WeaponDamage = shipStats.WeaponDamage + enemyStats.WeaponDamage;
        WeaponRange = shipStats.WeaponRange + enemyStats.WeaponRange;
        UpdateHp();
        UpdateWeaponDamage();
        UpdateWeaponRange();
    }

    private void UpdateHp()
    {
        healthScript.UpdateHealth(Hp);
    }
    private void UpdateSpeed()
    {
        //playerMovement.UpdateSpeed(ShipTurnRate);
    }
    private void UpdateRotationSpeed()
    {
        //rotatePlayer.UpdateRotation(ShipTurnRate);
    }
    private void UpdateWeaponDamage()
    {
        weaponFire.UpdateWeaponDamage(WeaponDamage);
    }
    private void UpdateWeaponRange()
    {
        weaponControllerEnemy.SetWeaponRange(WeaponRange);
    }
}
