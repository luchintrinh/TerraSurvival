using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    Slider slider;
    HealthManager health;
    private void Awake()
    {
        slider = transform.Find("Canvas").GetChild(0).GetComponent<Slider>();
        health = GetComponent<HealthManager>();
    }
    private void Start()
    {
        slider.maxValue = health.maxHealth;
        slider.minValue = 0;
        slider.value = health.maxHealth;
        Sethealth();
    }

    public void Sethealth()
    {
        slider.value = health.health;
    }
}
