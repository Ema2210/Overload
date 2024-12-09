using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] GameObject marketUI;
    [SerializeField] int moneyPerMineral = 5;

    private void Start()
    {
        marketUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (Inventory.InventoryEntries.Count == 0)
        {
            TriggerNothingToSellMessage(); //to implement

            return;
        }

        collision.GetComponent<PlayerMovement>().StopMovement();

        Pause.TogglePause();

        marketUI.SetActive(true);

    }

    private void TriggerNothingToSellMessage() //TO DO
    {
        
    }

    public void ClosePanel()
    {
        marketUI.SetActive(false);

        Pause.TogglePause();
    }



    public void TradeMinerals()
    {
        int minerals = ResourcesManager.Instance.MineralsAmount;
        ResourcesManager.Instance.MoveMoney(moneyPerMineral * minerals);
        ResourcesManager.Instance.AddMinerals(-minerals);

        ClosePanel();
    }

    public void SellAll()
    {
        TradeMineralsAndRelics();
        //TradeNFTSAndTokens();
        ClosePanel();
    }

    public void TradeMineralsAndRelics()
    {
        float total = DiggableEntriesPopulator.Total;


        ResourcesManager.Instance.MoveMoney((int)total);
        Inventory.ClearMineralsAndRelics();
        Inventory.ClearTokensAndNFTs();
        UpdateUI();
    }



    public void TradeNFTSAndTokens()
    {
        float total = DiggableEntriesPopulator.Total;


        ResourcesManager.Instance.MoveMoney((int)total);
        Inventory.ClearTokensAndNFTs();
        UpdateUI();

    }

    public void ExportNFTSAndTokens()
    {
        Inventory.ClearTokensAndNFTs();
        UpdateUI();
    }

    private static void UpdateUI()
    {
        DiggableEntriesPopulator[] pops = FindObjectsOfType<DiggableEntriesPopulator>();
        foreach (DiggableEntriesPopulator pop in pops)
        {
            pop.UpdateInventoryUI();
        }
    }
}
