using UnityEngine;

[CreateAssetMenu(fileName = "Radiator", menuName = "Upgrades/Radiator")]
public class RadiatorSO : UpgradeSO
{
    [field: SerializeField, Range(0f, 1f)] public float DamageEffectiveness { get; private set; }


    public override void Equip(PlayerEquipment playerEquipment)
    {
        playerEquipment.EquipRadiator(this);
    }
}