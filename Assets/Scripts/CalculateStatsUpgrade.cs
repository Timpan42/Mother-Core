using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateStatsUpgrade : MonoBehaviour
{
    [SerializeField] private StatShip shipStats;
    [SerializeField] private UpgradeStat playerStats;

    private int Hp;
    private float WeaponDamage;
    private float WeaponRange;
    private float ShipSpeed;
    private float ShipTurnRate;
    private float CoreDamage;
    private bool hasUpgrade = false;
    public bool getHasUpgrade { get => hasUpgrade; }

    [Space]
    [SerializeField]
    int HpIncrease;
    [SerializeField]
    float WeaponDamageIncrease,
    WeaponRangeIncrease,
    ShipSpeedIncrease,
    ShipTurnRateIncrease,
    CoreDamageIncrease;

    [Space]
    [SerializeField]
    int HpIncreaseCost,
    WeaponDamageIncreaseCost,
    WeaponRangeIncreaseCost,
    ShipSpeedIncreaseCost,
    ShipTurnRateIncreaseCost,
    CoreDamageIncreaseCost;

    // Scripts 
    private HealthScript healthScript;
    private RotatePlayer rotatePlayer;
    private PlayerMovement playerMovement;
    private WeaponFire weaponFire;
    private WeaponController weaponController;
    private InsideCore insideCore;

    public Dictionary<string, List<float>> UpgradeInformation;
    private List<float> HpList, WeaponDamageList, WeaponRangeList, ShipSpeedList, ShipTurnRateList, CoreDamageList = new List<float>(3);
    private void Start()
    {
        healthScript = transform.GetComponent<HealthScript>();
        rotatePlayer = transform.GetComponent<RotatePlayer>();
        playerMovement = transform.GetComponent<PlayerMovement>();
        weaponFire = transform.GetComponentInChildren<WeaponFire>();
        weaponController = transform.GetComponentInChildren<WeaponController>();
        insideCore = transform.GetComponentInChildren<InsideCore>();

        CalculateStats();
        UpgradeInformation = new Dictionary<string, List<float>>
        {
            {"Hp", HpList = new List<float>{playerStats.Hp[0],HpIncrease,HpIncreaseCost}},
            {"WeaponDamage", WeaponDamageList = new List<float>{playerStats.WeaponDamage[0],WeaponDamageIncrease,WeaponDamageIncreaseCost}},
            {"WeaponRange", WeaponRangeList = new List<float>{playerStats.WeaponRange[0],WeaponRangeIncrease,WeaponRangeIncreaseCost}},
            {"ShipSpeed", ShipSpeedList = new List<float>{playerStats.ShipSpeed[0],ShipSpeedIncrease,ShipSpeedIncreaseCost}},
            {"ShipTurnRate", ShipTurnRateList = new List<float>{playerStats.ShipTurnRate[0],ShipTurnRateIncrease,ShipTurnRateIncreaseCost}},
            {"CoreDamage", CoreDamageList = new List<float>{playerStats.CoreDamage[0],CoreDamageIncrease,CoreDamageIncreaseCost}},
        };
        hasUpgrade = true;
    }

    public void CalculateUpgrade(string upgrade)
    {
        bool upgraded = true;
        float stat = 0;
        float increaseStat = 0;
        float cost = 0;
        switch (upgrade)
        {
            case "Hp":
                stat = UpgradeInformation[upgrade][0] + HpIncrease;
                increaseStat = stat + HpIncrease;
                cost = UpgradeInformation[upgrade][2] + HpIncreaseCost;

                Hp = shipStats.Hp + (int)stat;
                UpdateHp();
                break;

            case "WeaponDamage":
                stat = UpgradeInformation[upgrade][0] + WeaponDamageIncrease;
                increaseStat = stat + WeaponDamageIncrease;
                cost = UpgradeInformation[upgrade][2] + WeaponDamageIncreaseCost;

                WeaponDamage = shipStats.WeaponDamage + stat;
                UpdateWeaponDamage();
                break;

            case "WeaponRange":
                stat = UpgradeInformation[upgrade][0] + WeaponRangeIncrease;
                increaseStat = stat + WeaponRangeIncrease;
                cost = UpgradeInformation[upgrade][2] + WeaponRangeIncreaseCost;

                WeaponRange = shipStats.WeaponRange + stat;
                UpdateWeaponRange();
                break;

            case "ShipSpeed":
                stat = UpgradeInformation[upgrade][0] + ShipSpeedIncrease;
                increaseStat = stat + ShipSpeedIncrease;
                cost = UpgradeInformation[upgrade][2] + ShipSpeedIncreaseCost;

                ShipSpeed = shipStats.ShipSpeed + stat;
                UpdateSpeed();
                break;

            case "ShipTurnRate":
                stat = UpgradeInformation[upgrade][0] + ShipTurnRateIncrease;
                increaseStat = stat + ShipTurnRateIncrease;
                cost = UpgradeInformation[upgrade][2] + ShipTurnRateIncreaseCost;

                ShipTurnRate = shipStats.ShipTurnRate + stat;
                UpdateRotationSpeed();
                break;

            case "CoreDamage":
                stat = UpgradeInformation[upgrade][0] + CoreDamageIncrease;
                increaseStat = stat + CoreDamageIncrease;
                cost = UpgradeInformation[upgrade][2] + CoreDamageIncreaseCost;

                CoreDamage = shipStats.CoreDamage + stat;
                UpdateCoreDamage();
                break;

            default:
                upgraded = false;
                Debug.Log("no upgrade");
                break;
        }
        if (upgraded)
        {
            UpgradeInformationUpdate(upgrade, stat, increaseStat, cost);
        }


    }
    private void CalculateStats()
    {
        Hp = shipStats.Hp + playerStats.Hp[0];
        WeaponDamage = shipStats.WeaponDamage + playerStats.WeaponDamage[0];
        WeaponRange = shipStats.WeaponRange + playerStats.WeaponRange[0];
        ShipSpeed = shipStats.ShipSpeed + playerStats.ShipSpeed[0];
        ShipTurnRate = shipStats.ShipTurnRate + playerStats.ShipTurnRate[0];
        CoreDamage = shipStats.CoreDamage + playerStats.CoreDamage[0];
        UpdateHp();
        UpdateSpeed();
        UpdateRotationSpeed();
        UpdateWeaponDamage();
        UpdateWeaponRange();
        UpdateCoreDamage();
    }

    private void UpgradeInformationUpdate(string name, float stat, float increaseStat, float cost)
    {
        UpgradeInformation[name] = new List<float> { stat, increaseStat, cost };
    }

    private void UpdateHp()
    {
        healthScript.UpdateHealth(Hp);
    }
    private void UpdateSpeed()
    {
        playerMovement.UpdateSpeed(ShipSpeed);
    }
    private void UpdateRotationSpeed()
    {
        rotatePlayer.UpdateRotation(ShipTurnRate);
    }
    private void UpdateWeaponDamage()
    {
        weaponFire.UpdateWeaponDamage(WeaponDamage);
    }
    private void UpdateWeaponRange()
    {
        weaponController.UpdateRadius(WeaponRange);
    }
    private void UpdateCoreDamage()
    {
        insideCore.UpdateCoreDamage(CoreDamage);
    }
}

