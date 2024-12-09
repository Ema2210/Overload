using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : ResourceUI
{
    [SerializeField] TextMeshProUGUI moneyAmountText;

    protected override void Start()
    {
        amountText = moneyAmountText;
        base.Start();
        ResourcesManager.Instance.SubscribeToMoneyMoved(UpdateText);
    }

    protected override void UpdateText()
    {
        if (amountText != null)
        {
            amountText.text = "$" + ResourcesManager.Instance.Money.ToString("N0").Replace(",", "'");
        }       
    }
}
