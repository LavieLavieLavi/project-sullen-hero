using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider; // to get a reference on the slider component

    public void SetMaxhHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }


    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

}
