using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; 
    public Image Healthbar; 
    
    void Update()
    {
    }

    public void SetMaxHealth(float health)
    {
        Healthbar.fillAmount = health;
    }

    public void SetHealth(float health)
    {
        Healthbar.fillAmount = Mathf.Max(0, health);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}