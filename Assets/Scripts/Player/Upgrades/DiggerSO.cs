using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDigger", menuName = "Upgrades/Digger")]
public class DiggerSO : UpgradeSO
{
    [field: SerializeField] public int DiggingSpeed { get; private set; }

    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipDigger(this);
    }
}
