using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeSO : ScriptableObject
{
    [field: SerializeField] public string UpgradeName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Sprite UpgradeArt { get; private set; }

    public abstract void Equip(PlayerEquipment playerEquipment);
}
