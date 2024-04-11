using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrapMoney : MonoBehaviour
{
    private int scrap = 1000;
    public int getScrap { get => scrap; }

    public void AddScrap(int scrapAmount)
    {
        scrap += scrapAmount;
    }
    public void RemoveScrap(int scrapAmount)
    {
        scrap -= scrapAmount;
    }

}
