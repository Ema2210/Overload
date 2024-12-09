using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] Slider fuelBarSlider;
    [SerializeField] FuelManager fuelManager;

    void Update()
    {
        UpdateFuelBarFillAmount();
    }

    void UpdateFuelBarFillAmount()
    {
        fuelBarSlider.value = FuelManager.FuelLevel / fuelManager.GetMaxFuel();
    }
}
