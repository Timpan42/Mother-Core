using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class UIHealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private HealthScript objectHealth;
    private bool isAlive = true;

    private void Start()
    {
        StartCoroutine(TrackHealth());
    }

    private IEnumerator TrackHealth()
    {

        float maxHealth = objectHealth.getMaxHealth;
        float currentHealth = objectHealth.getHealth;
        float barHealth;
        bool continueScript = false;

        while (isAlive)
        {

            if (!continueScript)
            {
                if (currentHealth == 0)
                {
                    currentHealth = objectHealth.getHealth;
                }
                else
                {
                    continueScript = true;
                }
            }
            else if (continueScript)
            {
                if (currentHealth != objectHealth.getHealth)
                {
                    currentHealth = objectHealth.getHealth;
                    barHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                    healthBar.fillAmount = barHealth / maxHealth;
                }

                if (objectHealth.getHealth <= 0)
                {
                    isAlive = false;
                }
            }
            yield return isAlive;
        }
    }
}
