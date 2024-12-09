using System;
using UnityEngine;

public class ConsumablesShop : MonoBehaviour
{
    public static event Action OnConsumableBuy;

    PlayerEquipment equipment;


    private void Start()
    {
        equipment = FindObjectOfType<PlayerEquipment>();
    }


    public void PurchaseConsumable(ConsumableSO consumableToBuy)
    {
        float money = ResourcesManager.Instance.Money;
        int moneySpent = consumableToBuy.Cost;

        if (moneySpent > money)
        {
            Debug.Log("Money: " + money);
            return;
        }

        int moneyToSpend = moneySpent;

        ResourcesManager.Instance.MoveMoney(-moneyToSpend);
        consumableToBuy.Equip(equipment);
        OnConsumableBuy?.Invoke();
    }

}
