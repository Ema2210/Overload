using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineralsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mineralsAmountText;

    void Start()
    {
        ResourcesManager.Instance.MineralsIncreased += Instance_MineralsIncreased;
    }

    private void Instance_MineralsIncreased()
    {        
        UpdateText();
    }

    void UpdateText()
    {
        mineralsAmountText.text = "Amount of Minerals: " + ResourcesManager.Instance.MineralsAmount.ToString();
    }
}
