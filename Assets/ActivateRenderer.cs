using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActivateRenderer : MonoBehaviour
{
    TilemapRenderer tilemapRenderer;
    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }
    void Start()
    {
        tilemapRenderer.enabled = false;
        Invoke(nameof(ActivateRendererMethod), 1);
        
    }

    void ActivateRendererMethod()
    {
        tilemapRenderer.enabled = true;
    }

}
