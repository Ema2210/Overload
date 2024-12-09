using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Drill : MonoBehaviour
{
    public static event Action OnDrill;

    BoxCollider2D myCollider;
    Rigidbody2D playerRigidbody;
    InputReader inputReader;
    float gravityIntensity;

    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] Inventory inventory;
    [SerializeField] Animator rocksDug; //mettilo qua per risolver il bug




    public static bool IsDigging { get; private set; }
    public static bool IsDiggingDown { get; private set; }
    [SerializeField] Vector3Int offset;
    [field: SerializeField] public bool IsDownDrill { get; private set; }
    [field: SerializeField] public float DiggingSpeed { get; private set; }
    HealthManager health = null;



        void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        playerRigidbody = GetComponentInParent<Rigidbody2D>();
        inputReader = GetComponentInParent<InputReader>();
    }
    private void Start()
    {
        gravityIntensity = playerRigidbody.gravityScale;
        gameObject.SetActive(false);
        health = FindObjectOfType<HealthManager>();

    }

    private void Update()
    {
        Dig();
    }

    public void Dig()
    {
        if (HealthManager.IsDead) return;
        if (IsDigging) return;
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Ground"));
        if (hitCollider == null) return;

        Rigidbody2D digger = gameObject.GetComponentInParent<Rigidbody2D>();
        StartDigging(digger);
    }

    void StartDigging(Rigidbody2D digger)
    {
        IsDigging = true;
        IsDiggingDown = IsDownDrill;



        ToggleDrillingSequence((cellPosition) =>
        {
            if(cellPosition == null) return;

            Vector2Int playerPosition = playerRigidbody.GetComponent<PlayerMovement>().GetGridPosition();


            GridSystem.Instance.Tilemap.SetTile(((Vector3Int)cellPosition), null);
            GridSystem.Instance.TilemapBackground.SetTile(((Vector3Int)cellPosition), GridSystem.Instance.EmptyRuleTileBackground);
            



            IsDigging = false;
            IsDiggingDown = false;

            TileType tileType = GridSystem.Instance.GridObjects[cellPosition.x, -cellPosition.y + 39].TileType;

            if(tileType != TileType.None && tileType != TileType.Rock && tileType != TileType.Lava && tileType != TileType.Gas)
            {
                SoundManager.Instance.PlaySound(Sounds.Positive);
                //ResourcesManager.Instance.AddMinerals(numberOfMineralsDug);
                inventory.AddToInventory(tileType);

            }



            Debug.Log($"TileType: {tileType}, Coordinates: ({cellPosition.x}, {-cellPosition.y + 39})");

            digger.gameObject.GetComponent<Diggers>().DisableAllDiggers();

            if (tileType == TileType.Lava)
            {
                health.TakeDamageFromLava();
            }

            if (tileType == TileType.Gas)
            {
                health.TakeDamageFromGas();
            }

        });

    }

    private void ToggleDrillingSequence(Action<Vector3Int> onComplete)
    {
        Vector3Int cellPosition = new Vector3Int(700,700,700);     




        if (IsDiggingDown)
        {
            Vector3 playerPos = playerRigidbody.transform.position;
            Vector3Int playerCellPos = GridSystem.Instance.Tilemap.WorldToCell(playerPos);
            Vector3Int rightPos = new Vector3Int (playerCellPos.x, playerCellPos.y -1, playerCellPos.z);
            cellPosition = rightPos;
        }
        else
        {
            cellPosition = GridSystem.Instance.Tilemap.WorldToCell(myCollider.transform.position);
        }

        TileBase tileCheck = GridSystem.Instance.Tilemap.GetTile(cellPosition);

            if (tileCheck == null)
        {
            Debug.LogWarning("Invalid cell position detected during digging.");
            IsDigging = false;
            IsDiggingDown = false;
            rocksDug.enabled = false;
            gameObject.GetComponentInParent<Diggers>().DisableAllDiggers();
            return;
        }



        TileBase tile = GridSystem.Instance.TilemapBackground.GetTile(cellPosition);



        StartCoroutine(DrillingAnimation(cellPosition, onComplete)); 
    }


    IEnumerator DrillingAnimation(Vector3Int cellPosition, Action<Vector3Int> onComplete)
    {
        //Debug.Log("Coroutine started. Time.deltaTime: " + Time.deltaTime);
        playerRigidbody.velocity = Vector2.zero; // Stop any current motion
        playerRigidbody.isKinematic = true; // Temporarily set to kinematic during the animation



        Vector3 start = playerRigidbody.transform.position;
        Vector3 end = GridSystem.Instance.Tilemap.CellToWorld(cellPosition) + new Vector3(0.5f, 0.3f, 0);

        float duration = Mathf.Min(1.75f, Mathf.Max(0.01f, (20.0f / playerEquipment.equippedDigger.DiggingSpeed) + -Depth.DepthValue / (90.0f * 11.5f)));

        duration = Mathf.Max(0.25f, duration);

        float elapsed = 0;
        //Debug.Log($"Looping.Duration: {duration}, DDepth.DepthValue: {Depth.DepthValue}, Depth.DepthValue/50.0f: {Depth.DepthValue / 50.0f}");

        rocksDug.enabled = true;
        OnDrill?.Invoke();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            //Debug.Log($"Looping. Elapsed: {elapsed}, Duration: {duration}, DeltaTime: {Time.deltaTime}");
            //float t = Mathf.Clamp01(elapsed / duration);
            playerRigidbody.gravityScale = 0;
            playerRigidbody.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        playerRigidbody.transform.position = end;
        playerRigidbody.gravityScale = gravityIntensity;
        playerRigidbody.isKinematic = false; // Restore after the animation

        Sound.Instance.StopSound();
        onComplete?.Invoke(cellPosition);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, new Vector3(0.2f, 0.2f, 0));
    }

    public void SetDiggingSpeed(float speed)
    {
        DiggingSpeed = speed;
    }
}
