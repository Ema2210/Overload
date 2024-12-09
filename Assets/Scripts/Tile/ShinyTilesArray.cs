using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShinyTilesArray : MonoBehaviour
{
    public static ShinyTilesArray Instance = null;

    [field: SerializeField] public TileType[] ShinyTileTypes { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
