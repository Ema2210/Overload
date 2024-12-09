using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConsumableSO : ScriptableObject
{
    public static event Action OnUse;
    [field: SerializeField] public string ConsumableName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Sprite ConsumableArt { get; private set; }
    [field: SerializeField] public string Key { get; private set; }

    public abstract void Equip(PlayerEquipment playerEquipment);
    public abstract void UseConsumable(InputReader player, ConsumableSO consToUse);

    protected void InvokeOnUse()
    {
        Debug.Log("InvokeOnUse happened");
        OnUse?.Invoke();
    }

}
