using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject floatingTextPrefabMediumRarity;
    public GameObject floatingTextPrefabRare;


    public void ShowFloatingText(string message, Diggable diggable)
    {
        GameObject floatingTextInstance;

        if(diggable.ValuePerUnit > 499 && diggable.ValuePerUnit < 1001)
        {
            floatingTextInstance = Instantiate(floatingTextPrefabMediumRarity, transform.position, Quaternion.identity);
        }
        else if (diggable.ValuePerUnit > 1000)
        {
            floatingTextInstance = Instantiate(floatingTextPrefabRare, transform.position, Quaternion.identity);
        }

        else
        {
            floatingTextInstance = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        }



        floatingTextInstance.transform.localScale = new Vector3(1, floatingTextInstance.transform.localScale.y, floatingTextInstance.transform.localScale.z);
        floatingTextInstance.GetComponentInChildren<TextMeshPro>().text = message;
        
        Destroy(floatingTextInstance, 3f);

    }
}
