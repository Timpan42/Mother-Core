using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UIToggleManager : MonoBehaviour
{
    [SerializeField] GameObject upgradeMenu = null;
    [SerializeField] GameObject escMenu = null;

    public void ToggleUpgradeMenu()
    {
        if (upgradeMenu.activeSelf)
        {
            upgradeMenu.SetActive(false);
        }
        else
        {
            upgradeMenu.SetActive(true);
        }
    }
    public void ToggleEscMenu()
    {
        if (escMenu.activeSelf)
        {
            escMenu.SetActive(false);
        }
        else
        {
            escMenu.SetActive(true);
        }
    }
}
