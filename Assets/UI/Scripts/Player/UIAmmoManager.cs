using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIAmmoManager : MonoBehaviour
{
    [SerializeField] private WeaponFire playerWeaponFire;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private GameObject player;
    private bool isAlive = true;


    private void Start()
    {
        StartCoroutine(TrackAmmo());
    }
    private IEnumerator TrackAmmo()
    {
        float maxAmmo = playerWeaponFire.getMaxAmmo;
        float currentAmmoIndex = playerWeaponFire.getAmmoIndex;
        float currentAmmo;
        bool continueScript = false;

        ammoText.text = "";

        while (isAlive)
        {
            if (!continueScript)
            {
                maxAmmo = playerWeaponFire.getMaxAmmo;
                currentAmmoIndex = playerWeaponFire.getAmmoIndex;
                currentAmmo = maxAmmo - currentAmmoIndex;
                ammoText.text = currentAmmo + " / " + maxAmmo;

                if (maxAmmo > 0)
                {
                    continueScript = true;
                }
            }
            else if (continueScript)
            {
                if (maxAmmo != playerWeaponFire.getMaxAmmo)
                {
                    maxAmmo = playerWeaponFire.getMaxAmmo;
                }

                if (currentAmmoIndex != playerWeaponFire.getAmmoIndex)
                {
                    currentAmmoIndex = playerWeaponFire.getAmmoIndex;
                    currentAmmo = maxAmmo - currentAmmoIndex;
                    ammoText.text = currentAmmo + " / " + maxAmmo;
                }

                if (!player.activeSelf || player == null)
                {
                    isAlive = false;
                }
            }
            yield return isAlive;

        }
    }
}
