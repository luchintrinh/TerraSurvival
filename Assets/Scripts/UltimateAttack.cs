using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateAttack : MonoBehaviour
{

    public Slider coolDown;

    // ultimate for Gun
    public GameObject ultimateBulletPreb;
    public float timeDelay = 3f;
    public float nextTime;

    float timer;

    public float preUltimate;
    public bool isReadyUltimate;


    //ultimate for sword;
    public float timeDelaySword = 3f;
    public float nextTimeSword;
    public bool isReadyUltimateSword;
    Animator ani;



    //SFX sounds

    SoundManager sfx;

    private void Awake()
    {
        isReadyUltimate = true;
        isReadyUltimateSword = true;
        sfx = FindObjectOfType<SoundManager>();
        
    }
    private void Start()
    {
        timeDelay = GetComponent<PlayerSetting>().attackDelayUltimate;
        timer = timeDelay;
        ani = transform.GetChild(2).GetComponent<Animator>();
    }
    public void Attack(Vector2 dir)
    {
        
        GameObject bullet = GameManager.instance.pool.Get(GetComponent<PlayerSetting>().weapon.bulletStrengthenPoolIndex);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().explosionRange = GetComponent<PlayerSetting>().explosionRange;
        bullet.GetComponent<Bullet>().initialPos = transform.position;
        int damage = transform.GetComponent<PlayerSetting>().physicDamage;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.GetComponent<Bullet>().Init(damage, dir);
        sfx.playSFX(SoundManager.SFXType.gunLaserUltimate);

    }

    public void AttackSword()
    {
        transform.GetChild(4).GetComponent<Animator>().Play("AttackSword", 0, 0f);
    }

    public void AttackUltimate()
    {
        if (!isReadyUltimate) return;
        switch (GameManager.instance.aim)
        {
            case GameManager.aimingStyle.mouse:
                Attack(GetComponentInParent<InputSystemManagement>().direction);
                break;
            case GameManager.aimingStyle.nearestEnemy:
                if (!GameManager.instance.nearestEnemyPos) return;
                Transform enemy = GameManager.instance.nearestEnemyPos;
                Vector3 dir = enemy.position - transform.position;
                dir= dir.normalized;
                Attack(dir);
                break;
        }
        isReadyUltimate = false;
        nextTime = Time.time + timeDelay;
        
        preUltimate = Time.time;
        timer = preUltimate;
    }


    public void AttackUltimateSword()
    {
        if (!isReadyUltimateSword) return;
        AttackSword();
        isReadyUltimateSword = false;
        nextTimeSword = Time.time + timeDelaySword;
        preUltimate = Time.time;
    }

    private void Update()
    {
        if (!isReadyUltimate && Time.time>=nextTime)
        {
            isReadyUltimate = true;
        }
        if(!isReadyUltimateSword && Time.time >= nextTimeSword)
        {
            isReadyUltimateSword = true;
        }
        if (timer < nextTime)
        timer += Time.deltaTime;
        GetComponentInChildren<UICharacter>().SetCoolDown(nextTime,preUltimate, timer);
    }
}
