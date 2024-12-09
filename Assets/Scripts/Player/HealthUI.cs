using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    //[SerializeField] Image healthBarImage;
    //[SerializeField] Gradient healthBarGradient;
    [SerializeField] Slider healthBarSlider;

    void Start()
    {
        HealthManager.HealthChanged += HealthManager_HealthChanged;
        UpdateHealthBarFillAmount(HealthManager.PlayerHealth);
    }

    private void HealthManager_HealthChanged(float health)
    {
        UpdateHealthBarFillAmount(health);
    }


    void UpdateHealthBarFillAmount(float health)
    {
        healthBarSlider.value = health / HealthManager.MaxHealth;

        //healthBarImage.color = healthBarGradient.Evaluate(healthBarImage.fillAmount);
    }

    private void OnDestroy()
    {
        HealthManager.HealthChanged -= HealthManager_HealthChanged;
    }

}
