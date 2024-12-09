using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public static Dictionary<TileType, int> InventoryEntries { get; private set; }
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] SpriteRenderer cargoFullImage;
    [SerializeField] FloatingText floatingText;
    [SerializeField] GameObject cargoFullPrefab;


    void Awake()
    {
        InventoryEntries = new Dictionary<TileType, int>();
    }

    public void AddToInventory(TileType tileType)
    {
        int mineralsInCargoBay = 0;
        foreach ( KeyValuePair<TileType, int> entry in InventoryEntries)
        {
            mineralsInCargoBay += entry.Value;
        }

        if(mineralsInCargoBay >= playerEquipment.equippedCargo.MaxCapacity - 1)
        {
            CargoFull();

            if (mineralsInCargoBay >= playerEquipment.equippedCargo.MaxCapacity)
            {
                return;
            }

        }

        if (InventoryEntries.ContainsKey(tileType))
        {
            InventoryEntries[tileType]++;
        }
        else
        {
            InventoryEntries.Add(tileType, 1);
        }


        string resourceName = tileType.ToString();
        string resourcePath = $"Diggables/{resourceName}";
        Debug.Log(resourcePath + " " + resourceName);



        Diggable diggable = Resources.Load<Diggable>(resourcePath);
        if (diggable != null)
        {
            string toShow = "1 " + diggable.Name + " collected!";
            floatingText.ShowFloatingText(toShow, diggable);
        }
    }

    private void CargoFull()
    {
        SoundManager.Instance.PlaySound(Sounds.CargoFull);

        GameObject cargoFullImage = Instantiate(cargoFullPrefab, transform.position, Quaternion.identity, transform.parent);


        Destroy(cargoFullImage, 4);
    }

    public static void ClearMineralsAndRelics()
    {
        List<TileType> keysToRemove = new List<TileType>();

        foreach (KeyValuePair<TileType, int> entry in InventoryEntries)
        {
            if ((((int)entry.Key) >= 19 && ((int)entry.Key) <= 22) || ((int)entry.Key) >= 26 && ((int)entry.Key) <= 32 || ((int)entry.Key) >= 34 && ((int)entry.Key) <= 38)
            {
                keysToRemove.Add(entry.Key);
            }


        }

        foreach (TileType key in keysToRemove)
        {
            InventoryEntries.Remove(key);
        }
    }
    public static void ClearTokensAndNFTs()
    {
        List<TileType> keysToRemove = new List<TileType>();


        foreach (KeyValuePair<TileType, int> entry in InventoryEntries)
        {
            if ((((int)entry.Key) >= 1 && ((int)entry.Key) <= 18) || ((int)entry.Key) >= 23 && ((int)entry.Key) <= 25)
            {
                keysToRemove.Add(entry.Key);
            }

        }

        foreach (TileType key in keysToRemove)
        {
            InventoryEntries.Remove(key);
        }
    }
    public static void PrintInventory()
    {
        Debug.Log("Inventory Contents:");
        foreach (var entry in InventoryEntries)
        {
            Debug.Log($"{entry.Key}: {entry.Value}");
        }
    }

    public static void RemoveItem(TileType tileType)
    {
        InventoryEntries[tileType]--;

        if (InventoryEntries[tileType] <= 0)
        {
            InventoryEntries.Remove(tileType);

        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PrintInventory();
        }
    }

    public static int GetCargoWeight()
    {
        return InventoryEntries.Count;
    }
}
