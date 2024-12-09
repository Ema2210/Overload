using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    [SerializeField] GameObject collidersParent;
    public static GridSystem Instance { get; private set; }
    public GridObject[,] GridObjects { get; private set; }
    [field: SerializeField] public Tilemap Tilemap { get; private set; }
    [field: SerializeField] public Tilemap TilemapBackground { get; private set; }
    //[field: SerializeField] public Tilemap TilemapNoncollidable { get; private set; }
    [field: SerializeField] public Tilemap Rock { get; private set; }
    [field: SerializeField] public TilemapCollider2D Collider { get; private set; }
    [SerializeField] Tile[] tiles;

    [SerializeField] RuleTile ruleTileBackground;
    [field: SerializeField] public RuleTile EmptyRuleTileBackground { get; private set; }
    [SerializeField] Vector2Int size = new Vector2Int(30, 200);
    [SerializeField] GameObject shinyPrefab;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitializeGrid(size);
    }



    private void InitializeGrid(Vector2Int gridSize)
    {
        GridObjects = new GridObject[gridSize.x, gridSize.y];

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                TileTypeObject tileTypeObject = PickRandomTile(j);

                if(j == 0) //j = Y = profondita`
                {
                    tileTypeObject = new TileTypeObject(TileType.None, tiles[0]);
                }

                int x = i; int y = -j + 39;

                GridObjects[i, j] = new GridObject(x, y, tileTypeObject.TileType/*, Rock*/);
                Tilemap.SetTile(new Vector3Int(x, y), tileTypeObject.tile);

                if (tileTypeObject.TileType == TileType.Rock)
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), ruleTileBackground);
                    SetTileCollider(x, y);
                    //TilemapNoncollidable.SetTile(new Vector3Int(x, y), ruleTileBackground);
                    Rock.SetTile(new Vector3Int(x, y), tileTypeObject.tile);
                    Tilemap.SetTile(new Vector3Int(x, y), null);

                }
                else if (tileTypeObject.TileType != TileType.Hole)
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), ruleTileBackground);

                    SetTileCollider(x, y);

                    //TilemapNoncollidable.SetTile(new Vector3Int(x, y), ruleTileBackground);

                    if (ShinyTilesArray.Instance.ShinyTileTypes.ToList().Contains(tileTypeObject.TileType))
                    {
                        SpawnShinyEffect(x, y);
                    }
                }
                else
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), EmptyRuleTileBackground);
                    //TilemapNoncollidable.SetTile(new Vector3Int(x, y), null);
                }
            }
        }
    }

    private void SetTileCollider(int x, int y)
    {
        if(collidersParent.transform == null) return;
        TileBase tile = TilemapBackground.GetTile(new Vector3Int(x, y));
        if (tile == EmptyRuleTileBackground) return;

        // Convert tile position to world position
        Vector3 worldPosition = GridSystem.Instance.Tilemap.CellToWorld(new Vector3Int(x, y));

        // Create a new GameObject for the collider
        GameObject colliderObject = new GameObject("Collider");

        colliderObject.transform.SetParent(collidersParent.transform);


        colliderObject.layer = LayerMask.NameToLayer("Ground");

        // Position the collider in the world (use an offset if necessary)
        colliderObject.transform.position = worldPosition + new Vector3(0.5f, 0.5f, 0); // Offset by half to center the collider


        BoxCollider2D boxCollider = colliderObject.AddComponent<BoxCollider2D>();



        boxCollider.size = new Vector2(0.95f, 0.95f);


        // Create the child GameObject
        GameObject childObject = new GameObject("ChildObject");
        BoxCollider2D childCollider = childObject.AddComponent<BoxCollider2D>();
        childCollider.size = new Vector2(0.15f, 0.15f);
        // Set the child as a child of the colliderObject
        childObject.transform.SetParent(colliderObject.transform);

        // Position the child at the same position as the parent collider (no offset in this case)
        childObject.transform.localPosition = Vector3.zero + new Vector3(0, -0.3f, 0);

        // Attach the DestroyOnContact script to the child object
        childObject.AddComponent<DestroyOnContact>();
        if(childObject == null) Destroy(colliderObject);
    }

    void SpawnShinyEffect(int x, int y)
    {
        Vector3 cellWorldPosition = Tilemap.CellToWorld(new Vector3Int(x, y)) + new Vector3(0.5f, 0.5f, 0);

        GameObject shyny = Instantiate(shinyPrefab, cellWorldPosition, Quaternion.identity, collidersParent.transform);
    }

    private void InitializeGridForEarthquake(Vector2Int gridSize)
    {
        
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                bool isSkipping = false;
                System.Random random = new System.Random();
                TileTypeObject tileTypeObject = new TileTypeObject(TileType.None, tiles[0]);
                if (random.NextDouble() < 0.5f) isSkipping = true;

                if(!isSkipping)
                {
                    tileTypeObject = PickRandomTile(j);

                    if (j == 0) //j = Y = profondita`
                    {
                        tileTypeObject = new TileTypeObject(TileType.None, tiles[0]);
                    }
                }
                else
                {
                    tileTypeObject = new TileTypeObject(GridObjects[i, j].TileType, tiles[0]);
                }

                int x = i; int y = -j + 39;

                if (!isSkipping)
                {

                    GridObjects[i, j] = new GridObject(x, y, tileTypeObject.TileType);
                    Tilemap.SetTile(new Vector3Int(x, y), tileTypeObject.tile);

                }


                if (tileTypeObject.TileType == TileType.Rock && !isSkipping)
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), ruleTileBackground);
                    SetTileCollider(x, y);

                    Rock.SetTile(new Vector3Int(x, y), tileTypeObject.tile);
                    Tilemap.SetTile(new Vector3Int(x, y), null);

                }
                else if (tileTypeObject.TileType != TileType.Hole && !isSkipping)
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), ruleTileBackground);

                    SetTileCollider(x, y);
                    //TilemapNoncollidable.SetTile(new Vector3Int(x, y), ruleTileBackground);
                    Rock.SetTile(new Vector3Int(x, y), null);

                    if (ShinyTilesArray.Instance.ShinyTileTypes.ToList().Contains(tileTypeObject.TileType))
                    {
                        SpawnShinyEffect(x, y);
                    }
                }
                else if (!isSkipping)
                {
                    TilemapBackground.SetTile(new Vector3Int(x, y), EmptyRuleTileBackground);
                    //TilemapNoncollidable.SetTile(new Vector3Int(x, y), null);
                    Rock.SetTile(new Vector3Int(x, y), null);
                }

                if(isSkipping && tileTypeObject.TileType != TileType.Hole)
                {
                    SetTileCollider(x, y);
                }
            }
        }
    }

    private TileTypeObject PickRandomTile(int depth)
    {
        int randomNumber = UnityEngine.Random.Range(-120, 136);

        switch (randomNumber)
        {
            case int n when (n >= -160 && n <= 20):
                return new TileTypeObject(TileType.None, tiles[0]);

            case int n when (n >= 21 && n <= 80):
                return new TileTypeObject(TileType.Hole, null);

            case int n when (n >= 81):

                {
                    return HandleNonEmptyTile(depth);
                }

            default:
                return new TileTypeObject(TileType.None, tiles[0]);
        }
    }

    TileTypeObject HandleNonEmptyTile(int depth)
    {
        int random9d8 = UnityEngine.Random.Range(0, depth);
        if (random9d8 > 80) //pauah
        {
            int someValue = UnityEngine.Random.Range(2, 132);
            switch (someValue)
            {
                case int n when (n >= 2 && n <= 6):
                    return new TileTypeObject(TileType.Gas, tiles[0]);
                case int n when (n >= 7 && n <= 20):
                    return new TileTypeObject(TileType.Lava, tiles[41]);
                case int n when (n >= 21 && n <= 46):
                    return new TileTypeObject(TileType.Rock, tiles[33]);
                case int n when (n >= 47 && n <= 63):
                    return new TileTypeObject(TileType.Rock, tiles[34]);
                case int n when (n >= 64 && n <= 80):
                    return new TileTypeObject(TileType.Rock, tiles[35]);
                case 81:
                    return new TileTypeObject(TileType.Avalanche, tiles[2]);
                case 82:
                    return new TileTypeObject(TileType.Chainlink, tiles[4]);

                case 84:
                    return new TileTypeObject(TileType.Solana, tiles[8]);
                case 85:
                    return new TileTypeObject(TileType.Bitcoin, tiles[12]);
                case 86:
                    return new TileTypeObject(TileType.Ethereum, tiles[14]);

                case 88:
                    return new TileTypeObject(TileType.BananaMonkey, tiles[24]);

                case 90:
                    return new TileTypeObject(TileType.HarbringerRelic, tiles[26]);
                case 91:
                    return new TileTypeObject(TileType.EscaladeRelic, tiles[27]);

                case 93:
                    return new TileTypeObject(TileType.DexterityRelic, tiles[30]);
                case 94:
                    return new TileTypeObject(TileType.SalubrityRelic, tiles[31]);
                case 95:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]);
                case int n when (n >= 96 && n <= 101):
                    return new TileTypeObject(TileType.Goldium, tiles[20]);
                case int n when (n >= 102 && n <= 114):
                    return new TileTypeObject(TileType.Silverium, tiles[22]);
                case int n when (n >= 115 && n <= 120):
                    return new TileTypeObject(TileType.Platinium, tiles[36]);
                case int n when (n >= 121 && n <= 125):
                    return new TileTypeObject(TileType.Einsteinium, tiles[37]);
                case int n when (n >= 126 && n <= 129):
                    return new TileTypeObject(TileType.Emerald, tiles[38]);
                case int n when (n >= 130 && n <= 131):
                    return new TileTypeObject(TileType.Ruby, tiles[39]);
                case 132:
                    return new TileTypeObject(TileType.Amazonite, tiles[40]);
                default:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]);
            }

        }        
        else if (random9d8 > 60) //pauah
        {
            int someValue = UnityEngine.Random.Range(21, 123);
            switch (someValue)
            {
                case int n when (n >= 21 && n <= 46):
                    return new TileTypeObject(TileType.Rock, tiles[33]);
                case int n when (n >= 47 && n <= 62):
                    return new TileTypeObject(TileType.Rock, tiles[34]);
                case int n when (n >= 63 && n <= 78):
                    return new TileTypeObject(TileType.Rock, tiles[35]);
                case 79:
                    return new TileTypeObject(TileType.Avalanche, tiles[2]);
                case 80:
                    return new TileTypeObject(TileType.Chainlink, tiles[4]);
                case 81:
                    return new TileTypeObject(TileType.CERRA, tiles[6]);
                case 82:
                    return new TileTypeObject(TileType.Solana, tiles[8]);
                case 83:
                    return new TileTypeObject(TileType.Bitcoin, tiles[12]);
                case 84:
                    return new TileTypeObject(TileType.Ethereum, tiles[14]);
                case 85:
                    return new TileTypeObject(TileType.BoredApe, tiles[23]);
                case 86:
                    return new TileTypeObject(TileType.BananaMonkey, tiles[24]);
                case 87:
                    return new TileTypeObject(TileType.PickleMan, tiles[25]);
                case 88:
                    return new TileTypeObject(TileType.HarbringerRelic, tiles[26]);
                case 89:
                    return new TileTypeObject(TileType.EscaladeRelic, tiles[27]);
                case 90:
                    return new TileTypeObject(TileType.RefrigerantRelic, tiles[29]);
                case 91:
                    return new TileTypeObject(TileType.DexterityRelic, tiles[30]);
                case 92:
                    return new TileTypeObject(TileType.SalubrityRelic, tiles[31]);
                case 93:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]);
                case int n when (n >= 94 && n <= 100):
                    return new TileTypeObject(TileType.Goldium, tiles[20]);
                case int n when (n >= 101 && n <= 114):
                    return new TileTypeObject(TileType.Silverium, tiles[22]);
                case int n when (n >= 115 && n <= 120):
                    return new TileTypeObject(TileType.Platinium, tiles[36]);
                case int n when (n >= 121 && n <= 122):
                    return new TileTypeObject(TileType.Einsteinium, tiles[37]);
                case 123:
                    return new TileTypeObject(TileType.Emerald, tiles[38]);
                default:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]);
            }


        }
        else if (random9d8 > 40) //pauah
        {
            int someValue = UnityEngine.Random.Range(67, 130);
            switch (someValue)
            {

                case int n when (n >= 67 && n <= 73):
                    return new TileTypeObject(TileType.Rock, tiles[34]);
                case int n when (n >= 74 && n <= 80):
                    return new TileTypeObject(TileType.Rock, tiles[35]);
                case 81:
                    return new TileTypeObject(TileType.Avalanche, tiles[2]);
                case 82:
                    return new TileTypeObject(TileType.ADA, tiles[3]);
                case 83:
                    return new TileTypeObject(TileType.Chainlink, tiles[4]);
                case 84:
                    return new TileTypeObject(TileType.AGIX, tiles[5]);
                case 85:
                    return new TileTypeObject(TileType.CERRA, tiles[6]);
                case 86:
                    return new TileTypeObject(TileType.CNCT, tiles[7]);

                case 88:
                    return new TileTypeObject(TileType.COCK, tiles[9]);
                case 89:
                    return new TileTypeObject(TileType.HUNT, tiles[10]);
                case 90:
                    return new TileTypeObject(TileType.LENFI, tiles[11]);

                case 92:
                    return new TileTypeObject(TileType.MIN, tiles[13]);

                case 94:
                    return new TileTypeObject(TileType.SNEK, tiles[15]);
                case 95:
                    return new TileTypeObject(TileType.WMBT, tiles[16]);
                case 96:
                    return new TileTypeObject(TileType.Tether, tiles[17]);
                case 97:
                    return new TileTypeObject(TileType.USD_Coin, tiles[18]);
                case 98:
                    return new TileTypeObject(TileType.BoredApe, tiles[23]);

                case 100:
                    return new TileTypeObject(TileType.PickleMan, tiles[25]);

                case 102:
                    return new TileTypeObject(TileType.EscaladeRelic, tiles[27]);
                case 103:
                    return new TileTypeObject(TileType.BoringRelic, tiles[28]);
                case 104:
                    return new TileTypeObject(TileType.RefrigerantRelic, tiles[29]);
                case 105:
                    return new TileTypeObject(TileType.DexterityRelic, tiles[30]);
                case 106:
                    return new TileTypeObject(TileType.SalubrityRelic, tiles[31]);
                case 107:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]);
                case int n when (n >= 108 && n <= 114):
                    return new TileTypeObject(TileType.Goldium, tiles[20]);//gold

                case int n when (n >= 115 && n <= 128):
                    return new TileTypeObject(TileType.Silverium, tiles[22]);//silver

                case 129:
                    return new TileTypeObject(TileType.Platinium, tiles[36]);


                default:
                    return new TileTypeObject(TileType.Rock, tiles[33]);
            }

        }


        else
        {
            int someValue = UnityEngine.Random.Range(1, 101);
            switch (someValue)
            {
                case int n when (n <= 36):
                    return new TileTypeObject(TileType.Copperion, tiles[19]); 

                case int n when (n >= 37 && n <= 90):
                    return new TileTypeObject(TileType.Ironion, tiles[21]); 
                case int n when (n >= 91 && n <= 98):
                    return new TileTypeObject(TileType.Silverium, tiles[22]);
                case int n when (n >= 99 && n <= 100):
                    return new TileTypeObject(TileType.Goldium, tiles[20]);
                default:
                    return new TileTypeObject(TileType.PestilenceRelic, tiles[32]); // 0% chance
            }

        }
    }

    public void EarthQuake()
    {

        StartCoroutine(DelayEarthquake());
    }

    void ClearColliders()
    {
        foreach (Transform child in collidersParent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    IEnumerator DelayEarthquake()
    {
        ClearColliders();

        yield return new WaitForSecondsRealtime(1);

        InitializeGridForEarthquake(size);

    }

}

