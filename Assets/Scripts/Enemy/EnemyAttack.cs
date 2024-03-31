using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange;

    public bool isReadyTime = true;
    public bool isInRange;
    public bool isReadyAttack;

    public float delayAttack;
    public float nextTime=0;
    public float timer;
    [SerializeField] LayerMask layer;

    RaycastHit2D[] playerHit;

    public void Attacked()
    {
        isReadyTime = false;
    }
    private void Awake()
    {
        delayAttack = GetComponent<Enemy>().enemy.spawnDelayTime;
        attackRange = GetComponent<Enemy>().enemy.attackRange;
    }

    private void Update()
    {
        if (!GetComponent<HealthManager>().isLive) return;
        AvailableAttack();
        if (isReadyTime) return;
        if (Time.time >= nextTime)
        {
            isReadyTime = true;
            nextTime =Time.time + delayAttack;
        }
    }
    private void LateUpdate()
    {
        if (!GetComponent<HealthManager>().isLive) return;
        isReadyAttack = isReadyTime && isInRange;
    }

    public void AvailableAttack()
    {
        Transform player = GetComponent<FindNearest>().FindPlayer();
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }
}
