
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Square : MonoBehaviour
{
    
    [SerializeField] bool hasMinerals = true;
    [SerializeField] int amountOfMinerals = 0;
    [SerializeField] Sprite[] mineralImages;

    public static bool IsBeingDug { get; private set; }

    TileType tileType;
    Diggers diggers;
    BoxCollider2D boxCollider;

    

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        int randomNumber = Random.Range(1, 101);

        


        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        


        






    }

    private void Start()
    {
        diggers = FindObjectOfType<Diggers>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Digger") return;
        if (IsBeingDug) return;
        Rigidbody2D digger = collision.gameObject.GetComponentInParent<Rigidbody2D>();


        ToggleFreezeDigger(digger, true);

        StartDigging(digger);
    }

    private void ToggleFreezeDigger(Rigidbody2D digger, bool toFreeze)
    {
        if (toFreeze) digger.constraints |= RigidbodyConstraints2D.FreezePosition; //adds the freeze position constraint
        else
        {
            digger.constraints &= ~RigidbodyConstraints2D.FreezePosition; //removes the freeze position constraint
        }
    }

    async void StartDigging(Rigidbody2D digger)
    {
        
        IsBeingDug = true;
        await Task.Delay(500);

        ToggleFreezeDigger(digger, false);

        boxCollider.enabled = false;
        digger.gameObject.GetComponent<Diggers>().DisableAllDiggers();

        IsBeingDug = false;

        if(hasMinerals)
        {
            ResourcesManager.Instance.AddMinerals(amountOfMinerals);
        }

        Destroy(gameObject);
    }
}
