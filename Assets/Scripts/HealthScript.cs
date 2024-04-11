using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthScript : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    [SerializeField] private ScrapMoney scrapMoney;
    [SerializeField] private EnemyStats enemyStats;
    public float getMaxHealth { get => maxHealth; }
    public float getHealth { get => currentHealth; }

    public void UpdateHealth(float hp)
    {
        maxHealth = hp;
        if (hp - currentHealth <= maxHealth)
        {
            currentHealth += hp - currentHealth;
        }
    }

    public void RemoveHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            ObjectIsDead();
        }
    }

    private void ObjectIsDead()
    {
        if (transform.CompareTag("Enemy"))
        {
            scrapMoney.AddScrap(enemyStats.scarpValue);
        }
        gameObject.SetActive(false);
    }
}
