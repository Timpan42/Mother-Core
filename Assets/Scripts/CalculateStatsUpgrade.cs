using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateStatsUpgrade : MonoBehaviour
{
    [SerializeField] private StatShip shipStats;
    [SerializeField] private UpgradeStat playerStats;

    [HideInInspector] public int Hp;
    [HideInInspector] public float WeaponDamage;
    [HideInInspector] public float WeaponRange;
    [HideInInspector] public float ShipSpeed;
    [HideInInspector] public float ShipTurnRate;
    [HideInInspector] public float CoreDamage;
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

    public Dictionary<string, List<float>> UpgradeInformation;
    private List<float> HpList, WeaponDamageList, WeaponRangeList, ShipSpeedList, ShipTurnRateList, CoreDamageList = new List<float>(3);
    private void Start()
    {
        CalculateStats();
        UpgradeInformation = new Dictionary<string, List<float>>
        {
            {"Hp", HpList = new List<float>{Hp,HpIncrease,HpIncreaseCost}},
            {"WeaponDamage", WeaponDamageList = new List<float>{WeaponDamage,WeaponDamageIncrease,WeaponDamageIncreaseCost}},
            {"WeaponRange", WeaponRangeList = new List<float>{WeaponRange,WeaponRangeIncrease,WeaponRangeIncreaseCost}},
            {"ShipSpeed", ShipSpeedList = new List<float>{ShipSpeed,ShipSpeedIncrease,ShipSpeedIncreaseCost}},
            {"ShipTurnRate", ShipTurnRateList = new List<float>{ShipTurnRate,ShipTurnRateIncrease,ShipTurnRateIncreaseCost}},
            {"CoreDamage", CoreDamageList = new List<float>{CoreDamage,CoreDamageIncrease,CoreDamageIncreaseCost}},
        };
        hasUpgrade = true;
    }

    public void CalculateUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Hp":
                Hp = shipStats.Hp + playerStats.Hp[0];
                Hp = Hp + HpIncrease;
                playerStats.Hp[1] += HpIncreaseCost;
                break;

            case "WeaponDamage":
                WeaponDamage = shipStats.WeaponDamage + playerStats.WeaponDamage[0];
                WeaponDamage = WeaponDamage + WeaponDamageIncrease;
                playerStats.WeaponDamage[1] += WeaponDamageIncreaseCost;
                break;

            case "WeaponRange":
                WeaponRange = shipStats.WeaponRange + playerStats.WeaponRange[0];
                WeaponRange = WeaponRange + WeaponRangeIncrease;
                playerStats.WeaponRange[1] += WeaponRangeIncreaseCost;
                break;

            case "ShipSpeed":
                ShipSpeed = shipStats.ShipSpeed + playerStats.ShipSpeed[0];
                ShipSpeed = ShipSpeed + ShipSpeedIncrease;
                playerStats.ShipSpeed[1] += ShipSpeedIncreaseCost;
                break;

            case "ShipTurnRate":
                ShipTurnRate = shipStats.ShipTurnRate + playerStats.ShipTurnRate[0];
                ShipTurnRate = ShipTurnRate + ShipTurnRateIncrease;
                playerStats.ShipTurnRate[1] += ShipTurnRateIncreaseCost;
                break;

            case "CoreDamage":
                CoreDamage = shipStats.CoreDamage + playerStats.CoreDamage[0];
                CoreDamage = CoreDamage + CoreDamageIncrease;
                playerStats.CoreDamage[1] += CoreDamageIncreaseCost;
                break;

            default:
                Debug.Log("no upgrade");
                break;
        }
    }

    public void CalculateStats()
    {
        Hp = shipStats.Hp + playerStats.Hp[0];
        WeaponDamage = shipStats.WeaponDamage + playerStats.WeaponDamage[0];
        WeaponRange = shipStats.WeaponRange + playerStats.WeaponRange[0];
        ShipSpeed = shipStats.ShipSpeed + playerStats.ShipSpeed[0];
        ShipTurnRate = shipStats.ShipTurnRate + playerStats.ShipTurnRate[0];
        CoreDamage = shipStats.CoreDamage + playerStats.CoreDamage[0];
    }
}

