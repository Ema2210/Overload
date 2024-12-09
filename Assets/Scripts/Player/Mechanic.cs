using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] GameObject mechanicUI;
    [SerializeField] int moneyToFullyRepairHull;

    private void Start()
    {
        mechanicUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        collision.GetComponent<PlayerMovement>().StopMovement();

        Pause.TogglePause();

        mechanicUI.SetActive(true);

    }

    public void ClosePanel()
    {
        mechanicUI.SetActive(false);

        Pause.TogglePause();
    }



    public void Repair() //fai in mode che si ripari il massimo possibile
    {
        float money = ResourcesManager.Instance.Money;



        float hullToAdd = HealthManager.GetMaxHealth() - HealthManager.PlayerHealth;
        int moneyToSpend = (int)((hullToAdd / HealthManager.GetMaxHealth()) * moneyToFullyRepairHull);

        if (moneyToSpend > money)
        {
            return;
        }

        FiniliseRepair(hullToAdd, moneyToSpend);
    }

    private void FiniliseRepair(float hullToAdd, int moneyToSpend)
    {
        HealthManager.FullyRepair(hullToAdd);
        ResourcesManager.Instance.MoveMoney(-moneyToSpend);

        ClosePanel();
    }
}

