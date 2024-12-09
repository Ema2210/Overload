using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] int startingMoney = 000;
    public static ResourcesManager Instance { get; private set; }
    public event Action MineralsIncreased;
    public static event Action MoneyMoved;

    public int MineralsAmount { get; private set; }
    public int Money { get; private set; }

    void Awake()
    {
        Instance = this;
        Money = startingMoney;
    }

    public void AddMinerals(int amount)
    {
        MineralsAmount += amount;
        MineralsIncreased?.Invoke();
    }

    public void MoveMoney(int amount)
    {
        Money += amount;
        MoneyMoved?.Invoke();
    }

    public void SubscribeToMoneyMoved(Action handler)
    {
        MoneyMoved += handler;
    }

    public void UnsubscribeFromMoneyMoved(Action handler)
    {
        MoneyMoved -= handler;
    }

    public void SubscribeToMineralsIncreased(Action handler)
    {
        MineralsIncreased += handler;
    }

    public void UnsubscribeFromMineralsIncreased(Action handler)
    {
        MineralsIncreased -= handler;
    }
}
