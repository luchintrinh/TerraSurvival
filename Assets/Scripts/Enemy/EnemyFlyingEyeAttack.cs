using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingEyeAttack : MonoBehaviour
{
    Transform player;
    EnemyAttack enemyAttack;
    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
    }

    public void Attack()
    {
        GameObject bullet= GameManager.instance.pool.Get(10);
        bullet.GetComponent<EBullet>().initialPos = transform.position;
        bullet.transform.position = transform.position;
        player = GetComponent<FindNearest>().FindPlayer();
        Vector2 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle-90);
        bullet.GetComponent<EBullet>().Init(GetComponent<Enemy>().enemy.damage, dir, GetComponent<EnemyAttack>().attackRange);
    }
    private void Update()
    {
        if (enemyAttack.isReadyAttack && GetComponent<HealthManager>().isLive)
        {
            Attack();
            enemyAttack.Attacked();
        }
    }


}
