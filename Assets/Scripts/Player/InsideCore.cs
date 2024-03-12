using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class InsideCore : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageTimer;
    private float currentTimer;
    private bool collisionObjectTakeDamage = false;

    private void Update()
    {
        StartDamageObjectTimer();
    }

    private void StartDamageObjectTimer()
    {
        if (currentTimer <= 0)
        {
            collisionObjectTakeDamage = true;
        }
        else
        {
            collisionObjectTakeDamage = false;
            currentTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider collisionObject)
    {
        if (collisionObjectTakeDamage && collisionObject.CompareTag("Enemy"))
        {
            DamageObject(collisionObject.GetComponent<HealthScript>());
            currentTimer = damageTimer;
        }
    }



    private void DamageObject(HealthScript collisionObjectHealthScript)
    {
        collisionObjectHealthScript.RemoveHealth(damage);
    }
}
