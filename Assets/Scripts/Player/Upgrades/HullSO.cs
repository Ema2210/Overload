using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHull", menuName = "Upgrades/Hull")]
public class HullSO : UpgradeSO
{
    [field: SerializeField] public int Health { get; private set; }

    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipHull(this);
    }
}


