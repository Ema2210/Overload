using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public static float MaxHealth { get; private set; } = 100;
    [SerializeField] float invFramesTime  = 0.15f;
    [SerializeField] GameObject gameOverUi;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float damageFromLava;
    public static float PlayerHealth { get; private set; }
    public static bool IsDead { get; private set; }

    public static event Action<float> HealthChanged;

    FuelManager fuelManager;
    PlayerEquipment playerEquipment;
    Animator myAnimator;

    float lastHitTime;


    void Awake()
    {
        PlayerHealth = MaxHealth;
        myAnimator = GetComponent<Animator>();
        playerEquipment = GetComponent<PlayerEquipment>();
        IsDead = false;

    }
    void Start()
    {
        fuelManager = GetComponent<FuelManager>();

        fuelManager.TriggerDeath += FuelManager_TriggerDeath;
    }



    private void ExecuteDeath()
    {
        fuelManager.TriggerDeath -= FuelManager_TriggerDeath;

        myAnimator.SetTrigger("Explode");
        IsDead = true;

        Invoke(nameof(OpenGameOverUI), 2);

    }

    private void OpenGameOverUI()
    {
        gameOverUi.SetActive(true);
    }

    public void ChangeHealth(float amount)
    {
        if(amount < 0 && IsInvulnerable()) return;

        PlayerHealth += amount;
        if(PlayerHealth > MaxHealth)
        {
            PlayerHealth = MaxHealth;
        }
        else if (PlayerHealth < 0)
        {
            ExecuteDeath();
        }

        Debug.Log($"Current Health: {PlayerHealth}, Max Health: {MaxHealth}");
        HealthChanged?.Invoke(PlayerHealth);

        if(amount <= 0)
        {
            explosion.Play();
            lastHitTime = Time.time;
        }


    }

    bool IsInvulnerable()
    {
        if(Time.time - lastHitTime > invFramesTime) return false;
        return true;
    }

    public void TakeDamageFromLava()
    {
        RadiatorSO currentRadiator = playerEquipment.equippedRadiator;
        float damageTaken = damageFromLava * currentRadiator.DamageEffectiveness;
        ChangeHealth(damageTaken);
    }

    public void TakeDamageFromGas()
    {
        RadiatorSO currentRadiator = playerEquipment.equippedRadiator;
        float damageTaken = ((Depth.DepthValue + 3000)/15) * currentRadiator.DamageEffectiveness;
        ChangeHealth(-damageTaken);
    }

    public static void FullyRepair(float amount)
    {
        PlayerHealth += amount;
        HealthChanged?.Invoke(PlayerHealth);
    }

    public static void SetHealthToMaxValue()
    {
        PlayerHealth = MaxHealth;
        HealthChanged?.Invoke(PlayerHealth);
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        MaxHealth = newMaxHealth;
    }
    public static float GetMaxHealth()
    {
        return MaxHealth;
    }


    //EVENTS
    private void FuelManager_TriggerDeath()
    {
        ExecuteDeath();
    }
}
