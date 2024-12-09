using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Refueler", menuName = "Consumables/Refueler")]
public class Refueler : ConsumableSO,  IConsumable 
{

    PlayerEquipment playerEquipment;

    public override void Equip(PlayerEquipment playerEquipment)
    {
        this.playerEquipment = playerEquipment;
        playerEquipment.Consumables.Add(this);
    }

    public override void UseConsumable(InputReader player, ConsumableSO consToUse)
    {
        if(Drill.IsDigging) return;
        playerEquipment = FindObjectOfType<PlayerEquipment>(); //TEMPORARY
        FuelManager.FillTank(50);
        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }


}
