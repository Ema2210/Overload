using UnityEngine;

[CreateAssetMenu(fileName = "MatterTransmitter", menuName = "Consumables/MatterTransmitter")]
public class MatterTransmitter: ConsumableSO
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
        playerEquipment = FindObjectOfType<PlayerEquipment>(); //TEMPORARY
        player.transform.position = new Vector3(7, 22, 0);
        SoundManager.Instance.PlaySound(Sounds.Teleporter);
        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }


}
