using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponFire : MonoBehaviour
{
    [SerializeField] Ammo ammo;
    [SerializeField] Transform ammoHolder;
    private GameObject ammoObject = null;
    private Collider enemyTarget;
    private bool activeRocket = false;

    private void Update()
    {
        if (activeRocket)
        {
            SendTargetPosition();
        }
    }

    public void ActivateRocket(Collider target)
    {

        if (target == null)
        {
            Debug.Log("No target");
            return;
        }

        enemyTarget = target;

        foreach (GameObject ammunition in ammoHolder)
        {
            if (ammunition.activeSelf == true)
            {
                ammoObject = ammunition;
                break;
            }
        }

        if (ammoObject != null)
        {
            ammoObject.GetComponent<ProjectileLogic>().ammo = ammo;
            activeRocket = true;
        }
        else
        {
            Debug.Log("No rocket found/active");
        }
    }

    private void SendTargetPosition()
    {
        // send information to ProjectileLogic so the rocket can move to target 
    }

    private void Reload()
    {
        // reload when ammoHolders children is all inactive or when reload button is pressed
    }

}
