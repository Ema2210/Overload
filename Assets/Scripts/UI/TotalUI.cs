using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalUI : ResourceUI
{
    [SerializeField] TextMeshProUGUI totalAmountText;
    [SerializeField] bool isNFTs;

    protected override void Start()
    {
        amountText = totalAmountText;
        base.Start();


    }

    private void Update()
    {
        UpdateText();
    }


    protected override void UpdateText()
    {
        if (amountText == null) return;

        if(isNFTs)
        {
            amountText.text = "$" + DiggableEntriesPopulator.Total.ToString();
        }
        else
        {
            amountText.text = "$" + DiggableEntriesPopulator.Total.ToString();
        }
    }

}
