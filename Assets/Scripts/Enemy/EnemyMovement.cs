using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator ani;
    Transform player;
    Rigidbody2D rb;
    HealthManager health;
    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthManager>();
    }
    private void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        rb.velocity = Vector2.zero;
        player = GetComponent<FindNearest>().FindPlayer();
        if (!player || !health.isLive) return;

        if (GetComponent<HealthManager>().type == HealthManager.Type.Boss)
        {
            ani.SetBool("Run", player != null);
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack_Goblin")) return;
        rb.MovePosition(transform.position + GetComponent<Enemy>().GetDirection(player.position, transform.position) * GetComponent<Enemy>().enemyMoveSpeed * Time.deltaTime);
    }
}
