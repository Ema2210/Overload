using UnityEngine;

[CreateAssetMenu(fileName = "NewEngine", menuName = "Upgrades/Engine")]
public class EngineSO : UpgradeSO
{
    [field: SerializeField] public float MovementSpeed { get; private set; }

    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipEngine(this);
    }
}
