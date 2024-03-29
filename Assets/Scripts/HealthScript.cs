using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public float getMaxHealth { get => maxHealth; }
    public float getHealth { get => currentHealth; }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void RemoveHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            ObjectIsDead();
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    private void ObjectIsDead()
    {
        gameObject.SetActive(false);
    }
}
