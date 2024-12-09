using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] Transform upgrades;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        collision.GetComponent<PlayerMovement>().StopMovement();

        Pause.TogglePause();

        shopUI.SetActive(true);
    }

    public void PurchaseUpgrade(UpgradeSO upgradeToBuy)
    {
        float money = ResourcesManager.Instance.Money;
        int moneySpent = upgradeToBuy.Cost;

        if (moneySpent > money)
        {
            Debug.Log("Money: " + money);
            return;
        }

        int moneyToSpend = moneySpent;

        ResourcesManager.Instance.MoveMoney(-moneyToSpend);
        upgradeToBuy.Equip(playerEquipment);        
    }

    public void ClosePanel()
    {
        shopUI.SetActive(false);

        Pause.TogglePause();
    }

    public void ToggleIcons(GameObject iconToShow)
    {
        foreach (Transform upgrade in upgrades)
        {
            if(upgrade.gameObject == iconToShow)
            {
                upgrade.gameObject.SetActive(true);
                continue;
            }

            upgrade.gameObject.SetActive(false);
        }
    }

}
