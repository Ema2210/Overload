using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public TileType tileTypeToDrop;

    public void DropMineral()
    {
        Inventory.RemoveItem(tileTypeToDrop);

        FindObjectOfType<DiggableEntriesPopulator>().UpdateInventoryUI();
    }

}
