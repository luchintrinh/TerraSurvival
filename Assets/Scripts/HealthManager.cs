using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public enum Type { Enemy, Player, Boss, EnemyWithAttack}
    public Type type;
    public GameObject healthPopup;
    public int health = 30;
    public int maxHealth = 30;
    HealthLost healthLostPupUp;
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
            case Type.EnemyWithAttack:
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
            case Type.EnemyWithAttack:
                TakeDamageEnemyWithAttack(damage);
                break;
        }
        
    }

    public void TakeDamagePlayer(int damage)
    {
        health -= damage;
        HealPlayer("-", damage);
        if (health == 0) health = -1;
        GetComponentInChildren<UICharacter>().SetHealth(maxHealth, health);
        FindObjectOfType<UIManagement>().SetHealth(maxHealth, health);
        ani.SetTrigger("Hit");
        if (health < 0)
        {
            FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.playerDead);
            isLive = false;
            Time.timeScale = 0;
            FindObjectOfType<UIManagement>().LossCanvas();
        }
    }

    public void HealPlayer(string str ,int health)
    {
        RectTransform healthPopUp = GameManager.instance.poolUI.GetObjectFromPool(transform.Find("Canvas").GetChild(0).transform);
        healthPopUp.GetComponent<HealthLost>().SetText(str,health);
    }

    private void TakeDamageMinion(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        GameObject healLost = GameManager.instance.pool.Get(11);
        healLost.GetComponent<HealthLostEnemy>().initialPos = gameObject.transform.position;
        healLost.GetComponent<HealthLostEnemy>().PopUpMovement();
        healLost.GetComponent<HealthLostEnemy>().SetText(damage);
        healLost.transform.position = gameObject.transform.position;
        ani.SetInteger("Health", health);
        if (health < 0)
        {
            isLive = false;
            UnEnableEnemy();
            FindObjectOfType<SpawnEnemy>().enemyDead++;
            GetComponent<Enemy>().randomItem();
            GameManager.instance.killed = FindObjectOfType<SpawnEnemy>().enemyDead;
            FindObjectOfType<SpawnEnemy>().StartNextWave();
            GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().LevelUp(GetComponent<Enemy>().enemy.getExp);
            FindObjectOfType<SpawnEnemy>().BossSpawn();
        }
    }

    public void TakeDamageBoss(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        GetComponent<EnemyBoss>().Sethealth();
        GameObject healLost = GameManager.instance.pool.Get(11);
        healLost.GetComponent<HealthLostEnemy>().initialPos = gameObject.transform.position;
        healLost.GetComponent<HealthLostEnemy>().PopUpMovement();
        healLost.GetComponent<HealthLostEnemy>().SetText(damage);
        healLost.transform.position = gameObject.transform.position;
        if (health <= 0)
        {
            ani.SetTrigger("Dead");
            isLive = false;
            FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.enemyBossDead);
            UnEnableEnemy();
            Invoke("WinCanvas", 1f);
        }
    }
    public void TakeDamageEnemyWithAttack(int damage)
    {
        health -= damage;
        if (health == 0) health = -1;
        if (health < 0)
        {
            ani.SetTrigger("Dead");
            isLive = false;
            UnEnableEnemy();
            FindObjectOfType<SpawnEnemy>().enemyDead++;
            GameManager.instance.killed = FindObjectOfType<SpawnEnemy>().enemyDead;
            FindObjectOfType<SpawnEnemy>().StartNextWave();
            GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().LevelUp(GetComponent<Enemy>().enemy.getExp);
            FindObjectOfType<SpawnEnemy>().BossSpawn();
        }
    }

    public void WinCanvas()
    {
        FindObjectOfType<UIManagement>().WinCanvas();
    }
    public void UnEnableEnemy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        
    }
    public void NotActiveEnemy()
    {
        gameObject.SetActive(false);
    }

}
