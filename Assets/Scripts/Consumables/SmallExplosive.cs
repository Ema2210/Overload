using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Small Explosive", menuName = "Consumables/Small Explosive")]
public class SmallExplosive : ConsumableSO, IConsumable
{
    public static event Action SmallExplosiveOnUse;
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

        int startingY = cellPosition.y - 1;
        int startingX = cellPosition.x - 1;

        for (int y = startingY; y < startingY + 3; y++)
        {
            for (int x = startingX; x < startingX + 3; x++)
            {
                Vector3Int tilePos = new Vector3Int(x, y);

                GridSystem.Instance.Tilemap.SetTile(((Vector3Int)tilePos), null);
                GridSystem.Instance.Rock.SetTile(((Vector3Int)tilePos), null);
                GridSystem.Instance.TilemapBackground.SetTile(((Vector3Int)tilePos), GridSystem.Instance.EmptyRuleTileBackground);
            }
        }


        SmallExplosiveOnUse?.Invoke();
        playerEquipment.UseConsumable(consToUse);
        InvokeOnUse();
    }


}
