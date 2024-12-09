using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plastic Explosive", menuName = "Consumables/Plastic Explosive")]
public class PlasticExplosive : ConsumableSO, IConsumable
{
    public static event Action PlasticExplosiveOnUse;
    PlayerEquipment playerEquipment = null;

    public override void Equip(PlayerEquipment playerEquipment)
    {
        this.playerEquipment = playerEquipment;
        playerEquipment.Consumables.Add(this);
    }

    public override void UseConsumable(InputReader player, ConsumableSO consToUse)
    {
        if (Drill.IsDigging) return;

        playerEquipment = FindObjectOfType<PlayerEquipment>(); //TEMPORARY
        Collider2D collider = playerEquipment.GetComponent<Collider2D>();

        Vector3Int cellPosition = GridSystem.Instance.Tilemap.WorldToCell(collider.transform.position);

        int startingY = cellPosition.y - 3;
        int startingX = cellPosition.x - 3;

        for (int y = startingY; y < startingY + 5; y++)
        {
            for (int x = startingX; x < startingX + 5; x++)
            {
                Vector3Int tilePos = new Vector3Int(x, y);

                GridSystem.Instance.Tilemap.SetTile(((Vector3Int)tilePos), null);
                GridSystem.Instance.Rock.SetTile(((Vector3Int)tilePos), null);
                GridSystem.Instance.TilemapBackground.SetTile(((Vector3Int)tilePos), GridSystem.Instance.EmptyRuleTileBackground);
            }
        }


        PlasticExplosiveOnUse?.Invoke();
        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }
}
