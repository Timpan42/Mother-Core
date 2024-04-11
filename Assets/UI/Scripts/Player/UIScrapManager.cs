using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScrapManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private ScrapMoney scrapMoney;
    [SerializeField] private GameObject player;
    private bool isAlive = true;

    private void Start()
    {
        StartCoroutine(TrackScarp());
    }
    private IEnumerator TrackScarp()
    {
        float scrapAmount = -1f;
        bool continueScript = false;

        moneyText.text = "0";

        while (isAlive)
        {
            if (!continueScript)
            {
                scrapAmount = scrapMoney.getScrap;
                if (scrapAmount >= 0)
                {
                    moneyText.text = "" + scrapAmount;
                    continueScript = true;
                }
            }
            else if (continueScript)
            {
                if (scrapAmount != scrapMoney.getScrap)
                {
                    scrapAmount = scrapMoney.getScrap;
                    moneyText.text = "" + scrapAmount;
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
