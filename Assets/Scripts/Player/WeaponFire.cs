using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] Ammo ammo;
    [SerializeField] WeaponConfig weaponConfig;
    [SerializeField] Transform ammoHolder;
    public float getShotCoolDown { get => weaponConfig.ShootCoolDown; }
    private float weaponDamage;
    private float totalDamage;
    private ProjectileLogic bulletScript = null;
    private int ammoMax;
    private int ammoIndex = 0;
    public int getMaxAmmo { get => ammoMax; }
    public int getAmmoIndex { get => ammoIndex; }
    private Collider enemyTarget;
    private WaitForSeconds seconds = new WaitForSeconds(1);
    private bool reloading = false;
    public bool isReloading { get => reloading; }



    private void Start()
    {
        ammoMax = ammoHolder.childCount;
    }

    public void UpdateWeaponDamage(float damage)
    {
        weaponDamage = damage;
    }

    public void ActivateRocket(Collider target)
    {
        if (target == null)
        {
            return;
        }
        enemyTarget = target;
        GetBullet();
        ChangeBulletsAmmoClass();
        CalculateDamage();
        SendTarget();
    }

    private void GetBullet()
    {
        Transform bullet = ammoHolder.GetChild(ammoIndex);
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            if (bullet.GetComponent<ProjectileLogic>() != null)
            {
                bulletScript = bullet.GetComponent<ProjectileLogic>();
                return;
            }
        }
        else
        {
            Debug.Log("No projectile found/active or is just not a projectile");
        }

    }

    private void ChangeBulletsAmmoClass()
    {
        bulletScript.ammo = ammo;
    }

    private void CalculateDamage()
    {
        totalDamage = weaponDamage + ammo.damage + weaponConfig.Damage;
    }

    private void SendTarget()
    {
        bulletScript.FollowTarget(enemyTarget.transform, totalDamage);
        IncreaseAmmoIndex();
    }

    private void IncreaseAmmoIndex()
    {
        ammoIndex++;
        if (ammoIndex >= ammoMax)
        {
            Reload();
        }

    }

    public void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(ReloadTimer());
            reloading = true;
        }
    }

    private IEnumerator ReloadTimer()
    {
        float counter = weaponConfig.ReloadTimer;
        while (counter > 0)
        {
            yield return seconds;
            counter--;
        }

        ammoIndex = 0;
        reloading = false;
        yield return null;
    }

}
