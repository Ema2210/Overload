using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NarrativeEventTrigger))]
public class Earthquakes : MonoBehaviour
{
    public static event Action OnEarthquake;

    [SerializeField] AudioSource earthquakeSound;
    [SerializeField] CinemachineImpulseSource earthquakeShake;

    GridSystem gridSystem;
    public static bool EarthquakeReady { get; private set; } = false;

    NarrativeEventTrigger eventTrigger;

    void Awake()
    {
        eventTrigger = GetComponent<NarrativeEventTrigger>();
    }

    void Start()
    {
        gridSystem = FindObjectOfType<GridSystem>();
    }


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EarthquakeReady = true;
            TriggerEarthquake();
        }
#endif
    }

    public void TriggerEarthquake()
    {
        if (EarthquakeReady == false) return;

        OnEarthquake?.Invoke();
        earthquakeSound.PlayOneShot(earthquakeSound.clip);
        earthquakeShake.GenerateImpulse();

        Invoke(nameof(NarrativeEvent), 1);

        EarthquakeReady = false;

    }

    private void NarrativeEvent()
    {
        eventTrigger.TriggerNarrativeEvent();
        gridSystem.EarthQuake();
    }

    public static void ReadyEarthquake(bool ready)
    {
        EarthquakeReady = ready;
    }
}
