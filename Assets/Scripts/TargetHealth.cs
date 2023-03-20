using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealth : MonoBehaviour
{
    // same as EnemyHealth, but defines the health slider that is appearing above the target


    public float maxHealth = 100;
    public float currentHealth;
    public bool isTargetDestroyed = false;

    public Slider healthSlider;
    public Text healthCounter;


    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthCounter();

    }

    public void DeductHealth(float deductHealth)
    {
        if (currentHealth > 0)
        {
            if (deductHealth >= currentHealth)
            {
                TargetDestroyed();

            }
            else
            {
                currentHealth -= deductHealth;
                healthSlider.value -= deductHealth;
            }
            UpdateHealthCounter();
        }

    }


    void TargetDestroyed()
    {
        currentHealth = 0;
        isTargetDestroyed = true;
        healthSlider.value = 0;
        UpdateHealthCounter();
        Destroy(gameObject);
    }

    void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }

}
