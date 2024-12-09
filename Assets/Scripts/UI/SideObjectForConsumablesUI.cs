using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SideObjectForConsumablesUI : MonoBehaviour
{
    [SerializeField] ConsumableSO consumable;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI AmountText;
    [SerializeField] Image Art;

    //[SerializeField] Button buttonToUse; se hai voglia in futuro

    PlayerEquipment playerEquipment;
    int amount;


    private void OnEnable()
    {
        ConsumableSO.OnUse += UpdateUI;
        ConsumablesShop.OnConsumableBuy += UpdateUI;
        playerEquipment = FindObjectOfType<PlayerEquipment>();
    }

    private void Start()
    {
        UseUpConsumable();
        
    }
    private void OnDisable()
    {
        ConsumableSO.OnUse -= UpdateUI;        
        ConsumablesShop.OnConsumableBuy -= UpdateUI;
    }

    void UpdateUI()
    {
        if (!playerEquipment.Consumables.Contains(consumable))
        {
            UseUpConsumable();
            Debug.Log("UpdateUI happened");
            return;
        }

        Name.text = consumable.ConsumableName;
        Description.text = "Press " + consumable.Key;
        amount = 0;

        foreach (ConsumableSO item in playerEquipment.Consumables)
        {

            if(item.name == consumable.name)
            {
                amount++;
            }
        }
        AmountText.text = amount.ToString();

        if (consumable.ConsumableArt != null)
        {
            Art.sprite = consumable.ConsumableArt;
            Art.enabled = true;
        }
    }

    void UseUpConsumable()
    {
        amount = 0;
        Name.text = "";
        Description.text = "";
        AmountText.text = "";
        if (consumable.ConsumableArt != null)
        {
            Art.enabled = false;
        }
    }
}
