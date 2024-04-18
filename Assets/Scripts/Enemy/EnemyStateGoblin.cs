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

    public float force = 150f;
    public float dashDuration = 1f;
    public float dashTimeDelay = 4f;
    public float nextime = 0;
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
    }

    private void Update()
    {
        checkTime();
        StateAction();
    }
    public void StateAction()
    {
        switch (state)
        {
            case States.Move:
                MoveToPlayer();
                break;
            case States.Dash:
                
                break;
        }
    }

    public void Dash()
    {
        StartCoroutine(DashAction());
    }
    IEnumerator DashAction()
    {
        state = States.Dash;
        rb.AddForce(GetComponent<Enemy>().GetDirection(player.position, transform.position)*force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);
        nextime = Time.time + dashTimeDelay;
        state = States.Move;
    }

    private void MoveToPlayer()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        if (!player || !health.isLive) return;

        if (GetComponent<HealthManager>().type == HealthManager.Type.Boss)
        {
            ani.SetBool("Run", player != null);
        }
        rb.MovePosition(transform.position + GetComponent<Enemy>().GetDirection(player.position, transform.position) * GetComponent<Enemy>().enemyMoveSpeed * Time.deltaTime);
    }
}
