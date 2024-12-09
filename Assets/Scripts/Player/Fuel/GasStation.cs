using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation : MonoBehaviour
{
    [SerializeField] GameObject gasStationUI;
    [SerializeField] int moneyToFillFullTank;
    [SerializeField] FuelManager fuelManager;
    [SerializeField] Earthquakes earthquakes;

    private void Start()
    {
        gasStationUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (FuelManager.FuelLevel / fuelManager.GetMaxFuel() > 0.97f) return;
        collision.GetComponent<PlayerMovement>().StopMovement();

        Pause.TogglePause();

        gasStationUI.SetActive(true);

    }

    public void ClosePanel()
    {
        gasStationUI.SetActive(false);

        Pause.TogglePause();

        earthquakes.TriggerEarthquake();
    }


    public void FillTank()
    {
        float money = ResourcesManager.Instance.Money;



        float fuelToAdd = fuelManager.GetMaxFuel() - FuelManager.FuelLevel;
        int moneyToSpend = (int)((fuelToAdd / fuelManager.GetMaxFuel()) * moneyToFillFullTank);

        //TO DO: make bigger tanks cost more

        if (moneyToSpend > money)
        {
            return;
        }
        Debug.Log("Money to spend in FillTank: " + moneyToSpend);
        FiniliseRefueling(fuelToAdd, moneyToSpend);
    }
    public void Refuel(int moneySpent)
    {
        float money = ResourcesManager.Instance.Money;
        if (moneySpent > money)
        {
            Debug.Log("Money: " + money);
            return;
        }

        int moneyToSpend = moneySpent;
        float fuelUnitCost = (float)moneyToFillFullTank / (float)fuelManager.GetMaxFuel(); // cost per unit of fuel
        float fuelToAdd = moneySpent / fuelUnitCost; // amount of fuel that can be added with moneySpent
        Debug.Log("Max fuel: " + fuelManager.GetMaxFuel());
        Debug.Log("moneyToFillFullTank: " + moneyToFillFullTank);
        Debug.Log("fuelUnitCost prima: " + fuelUnitCost);

        if (FuelManager.FuelLevel + fuelToAdd > fuelManager.GetMaxFuel())
        {
            fuelToAdd = fuelManager.GetMaxFuel() - FuelManager.FuelLevel;

            Debug.Log("fuelUnitCost dopo: " + fuelUnitCost);
            moneyToSpend = (int)(fuelToAdd * fuelUnitCost);
        }

        Debug.Log("Money to spend in refuel: " + moneyToSpend);
        FiniliseRefueling(fuelToAdd, moneyToSpend);
    }





    private void FiniliseRefueling(float fuelToAdd, int moneyToSpend)
    {
        Debug.Log("Money to spend in FiniliseRefueling: " + moneyToSpend);
        FuelManager.FillTank(fuelToAdd);
        ResourcesManager.Instance.MoveMoney(-moneyToSpend);

        ClosePanel();
    }
}
