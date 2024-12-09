using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Teleporter", menuName = "Consumables/Teleporter")]
public class Teleporter : ConsumableSO
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
        playerEquipment =FindObjectOfType<PlayerEquipment>(); //TEMPORARY


        Vector2 position = new Vector2(UnityEngine.Random.Range(5, 20), UnityEngine.Random.Range(22, 26));
        player.transform.position = position;

        Vector2 velocityDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)); 
        float speed = UnityEngine.Random.Range(0, 25.0f);

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = velocityDirection.normalized * speed;



        SoundManager.Instance.PlaySound(Sounds.Teleporter);
        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }


}
