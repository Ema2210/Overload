using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiggableEntriesPopulator : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryItemPrefab;

    [SerializeField] List<DiggableCatergoryEnum> allowedCats;
    [SerializeField] bool isVariant;

    //public static float NFTSAndTokensTotal {get; private set;}
    //public static float MineralsAndRelicsTotal { get; private set; }
    public static float Total { get; private set; }

    void OnEnable()
    {
        UpdateInventoryUI();
    }

 

    public void UpdateInventoryUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Total = 0; 




        foreach (KeyValuePair<TileType, int> entry in Inventory.InventoryEntries)
        {

            string resourceName = entry.Key.ToString();
            string resourcePath = $"Diggables/{resourceName}";
            Debug.Log(resourcePath + " " + resourceName);

            if (resourceName == TileType.None.ToString()) continue;



            Diggable diggable = Resources.Load<Diggable>(resourcePath);
            if (!allowedCats.Contains(diggable.Category)) continue;

            var inventoryItemGO = Instantiate(inventoryItemPrefab, this.transform);

            if (diggable != null)
            {
                inventoryItemGO.GetComponentInChildren<Image>().sprite = diggable.UISprite;
                if(!isVariant)
                {
                    inventoryItemGO.GetComponentInChildren<Price>().transform.GetComponent<TextMeshProUGUI>().text = "$" + diggable.ValuePerUnit.ToString();
                    float total = ((diggable.ValuePerUnit) * (entry.Value));
                    inventoryItemGO.GetComponentInChildren<Total>().transform.GetComponent<TextMeshProUGUI>().text = "$" + total.ToString();

                    UpdateRelevantTotal(total);
                }
                inventoryItemGO.GetComponentInChildren<Amount>().transform.GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();

                inventoryItemGO.GetComponentInChildren<Name>().transform.GetComponent<TextMeshProUGUI>().text = diggable.Name;
                var dropComponent = inventoryItemGO.GetComponentInChildren<Drop>();
                if (dropComponent != null)
                {
                    dropComponent.tileTypeToDrop = diggable.TileType;
                }





            }
            else
            {
                Debug.LogWarning($"Diggable ScriptableObject not found for {resourceName} at path {resourcePath}");
            }


        }
    }

    private void TooManyItems()
    {
        throw new NotImplementedException();
    }

    private void UpdateRelevantTotal(float total)
    {
        Total += total;
    }
}
