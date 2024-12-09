using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;
    [SerializeField] CinemachineImpulseSource diggingShake;

    HealthManager healthManager;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        healthManager = GetComponent<HealthManager>();
    }
    private void OnEnable()
    {
        PlayerMovement.OnCrash += TriggerImpulse;
        PlayerMovement.OnCrash += ExplosionSound;
        PlayerMovement.OnCrash += ExplosionVFX;
        Drill.OnDrill += Drill_OnDrill;
    }

    private void Drill_OnDrill()
    {
        diggingShake.GenerateImpulse();
    }

    private void OnDisable()
    {
        PlayerMovement.OnCrash -= TriggerImpulse;
        PlayerMovement.OnCrash -= ExplosionSound;
        PlayerMovement.OnCrash -= ExplosionVFX;
        Drill.OnDrill -= Drill_OnDrill;
    }

    void TriggerImpulse()
    {
        impulseSource.GenerateImpulse();
    }

    void ExplosionSound()
    {
        SoundManager.Instance.PlaySound(Sounds.Explosion);
    }

    void ExplosionVFX()
    {
        healthManager.ChangeHealth(0);
    }
}
