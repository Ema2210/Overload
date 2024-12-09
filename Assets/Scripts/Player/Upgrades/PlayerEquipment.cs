using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [field: SerializeField] public HullSO equippedHullSO { get; private set; }
    [field: SerializeField] public DiggerSO equippedDigger { get; private set; }
    [field: SerializeField] public EngineSO equippedEngine { get; private set; }
    [field: SerializeField] public TankSO equippedTank { get; private set; }
    [field: SerializeField] public CargoSO equippedCargo { get; private set; }
    [field: SerializeField] public RadiatorSO equippedRadiator { get; private set; }
    [field: SerializeField] public static List<UpgradeSO> equipment { get; private set; } = new List<UpgradeSO>();

    [SerializeField] HealthManager healthManager;
    [SerializeField] FuelManager fuelManager;

    [field: SerializeField] public List<ConsumableSO> Consumables { get; private set; }

    private void Start()
    {
        equipment.Clear();  
        HandleHull();
        Teleporter teleporter = new Teleporter();
        teleporter.Equip(this);
        Refueler refueler = new Refueler();
        refueler.Equip(this);
    }

    void Update() //for testing
    {
        /*if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("hello");
            EquipHull(newHullSO);
        }*/
    }

    void HandleHull()
    {
        if (equippedHullSO == null) return;

        Debug.Log($"Equipped hull: {equippedHullSO.UpgradeName} with health: {equippedHullSO.Health}");

        healthManager.SetMaxHealth(equippedHullSO.Health);
        HealthManager.SetHealthToMaxValue();
    }

    void HandleTank()
    {
        if (equippedTank == null) return;

        //Debug.Log($"Equipped hull: {equippedHullSO.UpgradeName} with health: {equippedHullSO.Health}");

        fuelManager.SetNewCapacity();
    }

    public void EquipHull(HullSO hullToEquip)
    {
        if (equippedHullSO != null)
        {
            equipment.Remove(equippedHullSO);
        }
        GenericEquip(hullToEquip);
        equippedHullSO = hullToEquip;
        HandleHull();
    }



    public void EquipTank(TankSO tankToEquip)
    {
        if (equippedTank != null)
        {
            equipment.Remove(equippedTank);
        }
        GenericEquip(tankToEquip);
        equippedTank = tankToEquip;
        HandleTank();
    }

    public void EquipEngine(EngineSO engineToEquip)
    {
        if (equippedEngine != null)
        {
            equipment.Remove(equippedEngine);
        }
        GenericEquip(engineToEquip);
        equippedEngine = engineToEquip;
    }

    public void EquipDigger(DiggerSO diggerToEquip)
    {
        if (equippedDigger != null)
        {
            equipment.Remove(equippedDigger);
        }
        GenericEquip(diggerToEquip);
        equippedDigger = diggerToEquip;
    }

    public void EquipCargoBay(CargoSO cargoBayToEquip)
    {
        if (equippedCargo != null)
        {
            equipment.Remove(equippedCargo);
        }
        GenericEquip(cargoBayToEquip);
        equippedCargo = cargoBayToEquip;
    }

    private void GenericEquip(UpgradeSO toEquip)
    {
        equipment.Add(toEquip);
    }
    public void UseConsumable(ConsumableSO consToUse)
    {
        Consumables.Remove(consToUse);
    }

    internal void EquipRadiator(RadiatorSO radiatorSO)
    {
        if (equippedRadiator != null)
        {
            equipment.Remove(equippedRadiator);
        }

        GenericEquip(radiatorSO);
        equippedRadiator = radiatorSO;
    }
}
