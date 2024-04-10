using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    HealthManager health;
    private void Awake()
    {
        health = GetComponentInParent<HealthManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !health.isLive) return;
        Vector3 dir = GetComponentInParent<Enemy>().GetDirection(transform.parent.transform.position, collision.transform.position);
        float force = GameManager.instance.playerSpawners[0].GetComponentInParent<PlayerSetting>().forceBack;
        GetComponentInParent<Enemy>().WasAttacked(dir, force);
        GetComponentInParent<HealthManager>().TakeDamage(GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().physicDamage);
    }
}
