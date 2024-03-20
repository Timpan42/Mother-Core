using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InsideCore : MonoBehaviour
{
    private PlayerControls playerInputMap;
    private InputAction inputCorePower;
    [SerializeField] private float damage;
    [SerializeField] private float damageTimer;
    [SerializeField] CorePower corePower;
    private bool activeCore = false;
    private float currentTimer;
    private bool collisionObjectTakeDamage = false;

    private void Awake()
    {
        playerInputMap = new PlayerControls();
    }

    private void OnEnable()
    {
        inputCorePower = playerInputMap.PlayerMovement.CorePower;
        inputCorePower.Enable();
    }

    private void Update()
    {
        ActivateTheCore();
        StartDamageObjectTimer();
    }

    private void ActivateTheCore()
    {
        if (inputCorePower.WasPressedThisFrame())
        {
            corePower.ActivateCore();
            activeCore = !activeCore;
        }
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
        if (collisionObject == null || !activeCore)
        {
            return;
        }
        else if (collisionObjectTakeDamage && collisionObject.CompareTag("Enemy"))
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
