using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollected : MonoBehaviour
{
    public enum Type { Gold, Health }
    public Type type;

    [SerializeField] ItemsObject item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision);
        }    
    }

    public void Collect(Collider2D collision)
    {
        switch (type)
        {
            case Type.Gold:
                FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.itemCollect);
                GameManager.instance.CoinPickup += item.value;
                gameObject.SetActive(false);
                break;
            case Type.Health:
                FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.heal);
                HealthManager health = collision.GetComponentInParent<HealthManager>();
                int curHeallth = health.health;
                health.health = curHeallth + item.value > health.maxHealth ? health.maxHealth : curHeallth + item.value;
                health.HealPlayer("+", item.value);
                collision.transform.parent.GetComponentInChildren<UICharacter>().SetHealth(health.maxHealth,health.health);
                FindObjectOfType<UIManagement>().SetHealth(health.maxHealth, health.health);
                gameObject.SetActive(false);
                break;
        }
    }

}
