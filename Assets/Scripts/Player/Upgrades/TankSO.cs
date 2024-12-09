using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTank", menuName = "Upgrades/Tank")]
public class TankSO : UpgradeSO
{
    [field: SerializeField] public int TankCapacity { get; private set; }

    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipTank(this);
    }
}
