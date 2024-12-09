using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIEntry : MonoBehaviour
{
    Image image;
    string name;
    int cost;

    private void OnEnable()
    {
        image = GetComponentInChildren<Image>();
        name = GetComponentInChildren<string>();
        cost = GetComponentInChildren<int>();
    }


}
