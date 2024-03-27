using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider ultimateSlider;
    [SerializeField] Color healthColor;
    [SerializeField] Color ultimateColor;
    [SerializeField] Color ultimateColorFull;


    public void SetHealth(int maxHealth, int health)
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void SetCoolDown(float timeCooldown, float minTime, float timer )
    {
        ultimateSlider.maxValue = timeCooldown;
        ultimateSlider.minValue = minTime;
        ultimateSlider.value = timer;        
    }

    private void Start()
    {
        transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    private void Update()
    {
        transform.position = transform.parent.position;
    }

}
