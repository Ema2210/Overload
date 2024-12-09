using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        if(collider == null) Destroy(gameObject.transform.parent.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Emptyness") return;

        Destroy(gameObject.transform.parent.gameObject);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Emptyness") return;

        Destroy(gameObject.transform.parent.gameObject);
    }
}
