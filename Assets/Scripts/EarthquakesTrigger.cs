using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakesTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Earthquakes.ReadyEarthquake(true);
            Destroy(gameObject, 2);
        }
    }
}
