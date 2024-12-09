using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ResourceUI : MonoBehaviour 
{
    protected TextMeshProUGUI amountText;


    protected virtual void Start()
    {
        UpdateText();
    }

    protected abstract void UpdateText();

}
