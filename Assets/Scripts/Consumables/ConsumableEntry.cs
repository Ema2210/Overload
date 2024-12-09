using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConsumableEntry : MonoBehaviour
{
    [SerializeField] ConsumableSO consumable;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] TextMeshProUGUI Cost;
    [SerializeField] Image Art;

    [SerializeField] Button buttonToBuy;

    PlayerEquipment playerEquipment;

    private void OnEnable()
    {
        playerEquipment = FindObjectOfType<PlayerEquipment>();
        Name.text = consumable.ConsumableName;
        Description.text = consumable.Description;
        Cost.text = consumable.Cost.ToString();

        if (consumable.ConsumableArt != null)
        {
            Art.sprite = consumable.ConsumableArt;
        }
    }
}
