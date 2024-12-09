using UnityEngine;

[CreateAssetMenu(fileName = "NewCargoBay", menuName = "Upgrades/CargoBay")]
public class CargoSO : UpgradeSO
{
    [field: SerializeField] public int MaxCapacity { get; private set; }

    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipCargoBay(this);
    }
}
