using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField] GameObject inventoryUI;
    void Awake()
    {
        inventoryUI.SetActive(false);
    }


    public void ToggleInventoryUI()
    {
        if ((Pause.IsPaused && !inventoryUI.activeInHierarchy)) return;

        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        Pause.SetPause(inventoryUI.activeInHierarchy);
    }
}
