using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Depth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] static Transform player;

    public static int DepthValue
    {
        get
        {
            return (int)((player.position.y - 20) * 12.5f);
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (depthText != null)
        {
            depthText.text = "" + DepthValue.ToString() + " ft.";
        }
    }
}
