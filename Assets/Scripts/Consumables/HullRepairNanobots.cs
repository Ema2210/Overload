using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HullRepairNanobots", menuName = "Consumables/HullRepairNanobots")]
public class HullRepairNanobots : ConsumableSO, IConsumable
{
    PlayerEquipment playerEquipment;

    public override void Equip(PlayerEquipment playerEquipment)
    {
        this.playerEquipment = playerEquipment;
        playerEquipment.Consumables.Add(this);
    }

    public override void UseConsumable(InputReader player, ConsumableSO consToUse)
    {
        if (Drill.IsDigging) return;

        Debug.Log("SI va!");
        playerEquipment = FindObjectOfType<PlayerEquipment>(); //TEMPORARY


        playerEquipment.GetComponent<HealthManager>().ChangeHealth(240);


        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }
}
