using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateGoblin : MonoBehaviour
{
    Animator ani;
    Transform player;
    Rigidbody2D rb;
    HealthManager health;
    public enum States { Move, Dash}
    public States state;

    public float dashSpeed = 10f;
    public float dashDuration = 1f;
    public float dashTimeDelay = 4f;
    public float nextime = 0;
    public float endDashTime=1f;
    public Vector3 positionTarget;
    private void Start()
    {
        state = States.Move;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthManager>();
        player = GetComponent<FindNearest>().FindPlayer();
    }
    public void checkTime()
    {
        if (state == States.Move)
        {
            if (Time.time >= nextime)
            {
                Dash();
            }
        }
        if (state == States.Dash && Time.time>= endDashTime)
        {
            
            GetComponent<Enemy>().moveSpeed -= dashSpeed;
            state = States.Move;
            nextime = Time.time + dashTimeDelay;
        }
    }

    private void Update()
    {
        checkTime();
        MoveToPlayer();
    }

    public void Dash()
    {
        DashAction();
    }
    public void DashAction()
    {
        positionTarget = player.position;
        state = States.Dash;
        GetComponent<Enemy>().moveSpeed += dashSpeed;
        endDashTime = Time.time + dashDuration;
    }

    private void MoveToPlayer()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        if (!player || !health.isLive) return;

        if (GetComponent<HealthManager>().type == HealthManager.Type.Boss)
        {
            ani.SetBool("Run", player != null);
        }
        player = GetComponent<FindNearest>().FindPlayer();
        Vector3 dir = GetComponent<Enemy>().GetDirection(positionTarget, transform.position);
        positionTarget = positionTarget + dir * 15;
        rb.MovePosition(transform.position + GetComponent<Enemy>().GetDirection(state==States.Move?player.position:positionTarget, transform.position) * GetComponent<Enemy>().moveSpeed * Time.deltaTime);
    }
}
