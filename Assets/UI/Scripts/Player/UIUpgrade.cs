using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    [SerializeField] private CalculateStatsUpgrade statsUpgrade;
    [SerializeField] private Transform upgradeBackground;
    private bool continueScript = false;
    private Transform parentTransformHolder;
    private Transform statTransformHolder;
    private List<float> temporaryList = new List<float>();
    private TextMeshProUGUI statText;
    private TextMeshProUGUI increaseText;
    private TextMeshProUGUI costText;


    // get text 1 and 3 

    void Start()
    {
        StartCoroutine(StartUpgradeWindow());
    }

    private IEnumerator StartUpgradeWindow()
    {
        int index = 0;
        while (index < upgradeBackground.childCount)
        {
            if (!continueScript)
            {
                continueScript = statsUpgrade.getHasUpgrade;
                yield return null;
            }
            else
            {
                parentTransformHolder = upgradeBackground.GetChild(index);
                string name = parentTransformHolder.name;
                statTransformHolder = parentTransformHolder.GetChild(1);

                statText = statTransformHolder.GetChild(1).GetComponent<TextMeshProUGUI>();
                increaseText = statTransformHolder.GetChild(3).GetComponent<TextMeshProUGUI>();
                costText = parentTransformHolder.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();

                Debug.Log(statsUpgrade.UpgradeInformation);
                temporaryList = statsUpgrade.UpgradeInformation[name];

                statText.text = "" + temporaryList[0];
                increaseText.text = "" + temporaryList[1];
                costText.text = "SC: " + temporaryList[2];
                index++;
            }
            yield return null;
        }
    }

    public void UpgradeButtonPressed(GameObject upgradeHolder)
    {

    }
    /*
        private TextMeshProUGUI GetCostText(){

        }
        private TextMeshProUGUI GetStatText(){

        }
        private TextMeshProUGUI GetIncresText(){


        }
    */

}
