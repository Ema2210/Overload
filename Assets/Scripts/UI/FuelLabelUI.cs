using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelLabelUI : ResourceUI
{
    [SerializeField] TextMeshProUGUI fuelAmountText;

    protected override void Start()
    {
        amountText = fuelAmountText;
        base.Start();
    }

    void Update()
    {
        UpdateText();
    }

    protected override void UpdateText()
    {
        if (amountText != null)
        {
            amountText.text = ((int)FuelManager.FuelLevel).ToString() + "%\nfull";

        }
    }
}
