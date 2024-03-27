using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthManager : MonoBehaviour
{
    public enum Type { Enemy, Player, Boss}
    public Type type;
    public GameObject healthPopup;
    public int health = 30;
    public int maxHealth = 30;
    Animator ani;

    public bool isLive;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        switch (type)
        {
            case Type.Enemy:
            case Type.Boss:
                ani = GetComponent<Animator>();
                
                maxHealth = GetComponent<Enemy>().enemy.maxHealth;
                health = maxHealth;
                break;
            case Type.Player:
                ani =transform.GetChild(0).GetComponent<Animator>();
                maxHealth = GetComponent<PlayerSetting>().maxHealth;
                health = maxHealth;
                break;
        }
        isLive = true;
    }

    public void HidePopupHealth(GameObject popup)
    {
        popup.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        switch (type)
        {
            case Type.Player:
                TakeDamagePlayer(damage);
                break;
            case Type.Enemy:
                TakeDamageMinion(damage);
                break;
            case Type.Boss:
                TakeDamageBoss(damage);
                break;
        }
        
    }

    public void TakeDamagePlayer(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        GetComponentInChildren<UICharacter>().SetHealth(maxHealth, health);
        FindObjectOfType<UIManagement>().SetHealth(maxHealth, health);
        ani.SetTrigger("Hit");
        if (health < 0)
        {
            isLive = false;
            Time.timeScale = 0;
            FindObjectOfType<UIManagement>().LossCanvas();
        }
    }

    private void TakeDamageMinion(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        ani.SetInteger("Health", health);
        if (health < 0)
        {
            isLive = false;
            UnEnableEnemy();
            int deadNumber= FindObjectOfType<SpawnEnemy>().enemyDead++;
            GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().LevelUp(GetComponent<Enemy>().enemy.getExp);
        }
    }

    public void TakeDamageBoss(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        
        if (health < 0)
        {
            ani.SetTrigger("Dead");
            isLive = false;
            UnEnableEnemy();
            FindObjectOfType<SpawnEnemy>().enemyDead++;
            Invoke("WinCanvas", 1f);
        }
    }

    public void WinCanvas()
    {
        FindObjectOfType<UIManagement>().WinCanvas();
    }
    public void UnEnableEnemy()
    {
        GetComponent<Collider2D>().enabled = false;
        
    }
    public void NotActiveEnemy()
    {
        gameObject.SetActive(false);
    }

}