using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPanelHolder : InfoPanelHolder
{
    [Tooltip("Cost to purchase turret each time")]
    [SerializeField] private int[] purchaseCost ;

    private int currentCost { get { return purchaseCost[panels.Count]; } }
    private Text turretCostText;

    private void OnValidate()
    {
        if (purchaseCost.Length != maxPanelCount) 
            Debug.LogWarning("Purchase cost length must be equal to max panel count");
    }

    private void Awake()
    {
        turretCostText = addButton.GetComponentInChildren<Text>();
    }

    protected override void CheckCurrentPanelCount()
    {
        base.CheckCurrentPanelCount();
        if(panels.Count < maxPanelCount)
            turretCostText.text = purchaseCost[panels.Count].ToString();
    }


    protected override bool CanAddNewPanel()
    {
        return IngameManager.instance.DecreaseMoney(currentCost);
    }
}
