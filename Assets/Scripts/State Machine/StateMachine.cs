using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    Move,
    Attack,
    Dash
}
public class StateMachine : MonoBehaviour
{
    Enemy enemy;
    Rigidbody2D rb;
    public float forceDash = 10f;
    public State currentState;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.Move;
        StartCoroutine(ExecuteChaseState());
    }
    private void Update()
    {
        //ExecuteCurrentState();
    }
    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case State.Move:
                ExecuteIdleState();
                break;
            case State.Attack:
                StartCoroutine(ExecuteChaseState());
                break;
            case State.Dash:
                ExecuteDashState();
                break;
            default:
                break;
        }
    }

    private void ExecuteDashState()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        enemy.isDash = true;
        rb.AddForce(enemy.GetDirection(enemy.player.position, transform.position) * forceDash);
        yield return new WaitForSeconds(1f);
        ChangeState(State.Move);
    }

    IEnumerator ExecuteChaseState()
    {
        enemy.isDash = false;
        yield return new WaitForSeconds(3f);
        ChangeState(State.Dash);
        ExecuteDashState();
    }

    private void ExecuteIdleState()
    {
        
    }
    public void ChangeState(State newState)
    {
        currentState = newState;
    }
}
