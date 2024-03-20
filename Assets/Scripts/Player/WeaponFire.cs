using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] Ammo ammo;
    [SerializeField] Transform ammoHolder;
    [SerializeField] private float reloadTimer;
    [SerializeField] private float weaponDamage;
    private float totalDamage;
    private ProjectileLogic bulletScript = null;
    private int ammoHolderCounter;
    private int ammoIndex = 0;
    private Collider enemyTarget;
    private WaitForSeconds seconds = new WaitForSeconds(1);
    private bool reloading = false;



    private void Start()
    {
        ammoHolderCounter = ammoHolder.childCount;
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
        totalDamage = weaponDamage;
    }

    private void SendTarget()
    {
        bulletScript.FollowTarget(enemyTarget.transform, totalDamage);
        IncreaseAmmoIndex();
    }

    private void IncreaseAmmoIndex()
    {
        ammoIndex++;
        if (ammoIndex >= ammoHolderCounter)
        {
            Reload();
        }

    }

    public void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(ReloadTimer());
            ammoIndex = 0;
            reloading = true;
        }
    }

    private IEnumerator ReloadTimer()
    {
        float counter = reloadTimer;
        while (counter > 0)
        {
            yield return seconds;
            counter--;
        }
        reloading = false;
        yield return null;
    }

}
