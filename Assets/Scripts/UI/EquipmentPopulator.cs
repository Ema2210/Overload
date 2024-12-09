using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPopulator : MonoBehaviour
{
    [SerializeField] Transform entryPrefab;

    private void OnEnable()
    {
        Populate();
    }
    void Populate()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        foreach (var item in PlayerEquipment.equipment)
        {
            Transform entry = Instantiate(entryPrefab, this.transform);
            entry.GetComponentInChildren<Image>().sprite = item.UpgradeArt;
            entry.GetComponentInChildren<Name>().transform.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            entry.GetComponentInChildren<Amount>().transform.GetComponentInChildren<TextMeshProUGUI>().text = "$" + item.Cost.ToString();

        }
    }
}
