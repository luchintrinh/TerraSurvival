using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollideHandler : MonoBehaviour
{
    Coroutine takeDamage;
    HealthManager health;
    public float timeTakeDamage = 0.3f;

    private void Awake()
    {
        health = GetComponentInParent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!health.isLive) return;
        if (collision.CompareTag("Enemy"))
        {
            if (takeDamage == null)
                takeDamage = StartCoroutine(TakeDamage(collision));
        }

        else if (collision.CompareTag("EnemyWeapon"))
            health.TakeDamage(collision.GetComponentInParent<CircleWeapon>().damage);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if (takeDamage != null) StopCoroutine(takeDamage);
        takeDamage = null;
    }


    IEnumerator TakeDamage(Collider2D collision)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeTakeDamage);
            health.TakeDamage(collision.GetComponent<Enemy>().attackDamage);
        }

    }
}
