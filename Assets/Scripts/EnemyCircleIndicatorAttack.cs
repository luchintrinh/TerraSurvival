using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleIndicatorAttack : MonoBehaviour
{
    SpriteRenderer sprite;
    EnemyBoss boss;


    public bool isTookDamage;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        boss = GetComponentInParent<EnemyBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        boss.isAttack = true;
        AbleIndicator(true);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (isTookDamage)
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(GetComponentInParent<Enemy>().attackDamage);
            isTookDamage = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player")) return;
        boss.isAttack = false;
        AbleIndicator(false);
        
    }

    public void AbleIndicator(bool check)
    {
        sprite.enabled = check;
    }
}
