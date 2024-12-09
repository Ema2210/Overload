using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    [SerializeField] UpgradeSO upgrade;
    [SerializeField] TextMeshProUGUI Name; 
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI Cost;
    [SerializeField] Image Art;
    [SerializeField] TextMeshProUGUI deployed;
    [SerializeField] Button buttonToBuy;

    PlayerEquipment playerEquipment;

    private void OnEnable()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        Name.text = upgrade.UpgradeName;
        Description.text = upgrade.Description;
        Cost.text = "$" + upgrade.Cost.ToString("N0").Replace(",", "'");

        if (upgrade.UpgradeArt != null)
        {
            Art.sprite = upgrade.UpgradeArt;
        }

        ManageBuyingItemUI();

    }

    public void ManageBuyingItemUI()
    {
        if (upgrade == playerEquipment.equippedCargo ||
            upgrade == playerEquipment.equippedDigger ||
            upgrade == playerEquipment.equippedEngine ||
            upgrade == playerEquipment.equippedHullSO ||
            upgrade == playerEquipment.equippedTank ||
            upgrade == playerEquipment.equippedRadiator
            )
        {
            deployed.text = "EQUIPPED";
            buttonToBuy.interactable = false;
        }
        else
        {
            deployed.text = "";
            buttonToBuy.interactable = true;
        }
    }
}
