using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public bool isAttack;
    public bool isReadyAttack;
    Animator ani;
    Enemy enemy;


    public float delayAttackTime;
    float nextTime=0;
    

    private void Start()
    {
        ani = GetComponent<Animator>();
        isAttack = false;
        enemy = GetComponent<Enemy>();
        delayAttackTime = enemy.enemy.spawnDelayTime;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        isReadyAttack = isAttack && Time.time >= nextTime;
        if (!isReadyAttack) return;
        ani.SetTrigger("Attack"); 
        nextTime += delayAttackTime;
    }

    public void CheckAbleToAttackIndicator()
    {
        GetComponentInChildren<EnemyCircleIndicatorAttack>().AbleIndicator(false);
    }
}
