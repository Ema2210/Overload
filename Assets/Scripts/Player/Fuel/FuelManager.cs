using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FuelManager : MonoBehaviour
{
    public event Action TriggerDeath;
    public static float FuelLevel { get; private set; }

    [SerializeField] float fuelStandardDepletionRate = 1;
    [SerializeField] float fuelHighDepletionRate = 1.4f;
    [SerializeField] float fuelVeryHighDepletionRate = 2.2f;
    [SerializeField] float fuelLowDepletionRate = 0.75f;
    [SerializeField] static PlayerEquipment playerEquipment;

    float currentFuelDepletionRate;
    bool fuelLow;
    bool fuelLowCourutineStarted;
    Coroutine fuelLowCoroutine;
    

    void Awake()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
    }
    void Start()
    {
        FuelLevel = playerEquipment.equippedTank.TankCapacity;
        currentFuelDepletionRate = fuelStandardDepletionRate;

    }


    void Update()
    {
        if (HealthManager.IsDead) return;


        if (Drill.IsDigging)
        {
            ChangeDepletionRateScheme(TypeOfFuelDepletionRate.VeryHigh);
        }
        ConsumePassiveFuel();

        if (fuelLow && fuelLowCoroutine == null)
        {
            fuelLowCoroutine = StartCoroutine(FuelLowSound());
        }
        else if (!fuelLow && fuelLowCoroutine != null)
        {
            StopCoroutine(fuelLowCoroutine);
            fuelLowCoroutine = null;
        }


    }

    IEnumerator FuelLowSound()
    {
        while (fuelLow)
        {

            if (Pause.IsPaused) yield return new WaitForSecondsRealtime(2);
            SoundManager.Instance.PlaySound(Sounds.FuelLow);
            currentFuelDepletionRate = fuelLowDepletionRate;
            yield return new WaitForSecondsRealtime(2);
        }
    }

    void ConsumePassiveFuel()
    {
        if(FuelLevel < 0)
        {
            TriggerDeath?.Invoke();
            return;
        }

        else if(FuelLevel/ playerEquipment.equippedTank.TankCapacity < 0.17f)
        {
            fuelLow = true;
        }
        else
        {
            fuelLow = false;
        }
        FuelLevel -= currentFuelDepletionRate * Time.deltaTime;
    }

    public static void FillTank(float amount)
    {
        SetFuelLevel(FuelLevel += amount);
    }

    public static void SetFuelLevel(float value)
    {
        int maxLevel = playerEquipment.equippedTank.TankCapacity;
        FuelLevel = value > maxLevel ? maxLevel : value;
    }

    public void ChangeDepletionRateScheme(TypeOfFuelDepletionRate typeOfFuelDepletionRate)
    {
        if (fuelLow) return;
        switch (typeOfFuelDepletionRate)
        {
            case TypeOfFuelDepletionRate.Standard:
                currentFuelDepletionRate = fuelStandardDepletionRate;
                break;
            case TypeOfFuelDepletionRate.High:
                currentFuelDepletionRate = fuelHighDepletionRate;
                break;
            case TypeOfFuelDepletionRate.VeryHigh:
                currentFuelDepletionRate = fuelVeryHighDepletionRate;
                break;
        }
    }

    public int GetMaxFuel()
    {
        return playerEquipment.equippedTank.TankCapacity;
    }

    public void SetNewCapacity()
    {
        FuelLevel = playerEquipment.equippedTank.TankCapacity;
    }
}
