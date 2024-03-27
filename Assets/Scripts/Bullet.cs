using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { normal, strengthen}

    public BulletType type;

    int damage;
    public float bulletSpeed;
    Rigidbody2D rb;
    Animator ani;
    [SerializeField] Sprite initialSprite;
    public Vector3 initialPos;
    public float explosionRange = 1f;
    LayerMask attackLayerMask;


    //SFX sounds

    SoundManager sfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        attackLayerMask = LayerMask.GetMask("Enemy");
        sfx = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        if (!GetComponent<Animator>()) return;
        ani = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }


    void Explosion()
    {
        transform.localScale = new Vector3(explosionRange, explosionRange, 1);
        ani.enabled = true;
        StopMove();
    }

    public void ExplosionDamage()
    {
        sfx.playSFX(SoundManager.SFXType.exploise);
        CameraShake();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, attackLayerMask);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.GetComponent<HealthManager>().isLive) return;
            Vector3 dir = enemy.transform.position - transform.position;
            dir = dir.normalized;
            enemy.GetComponent<Enemy>().WasAttacked(dir, GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().forceBack);
            enemy.GetComponent<HealthManager>().TakeDamage(GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().explosionDamage);
        }
        
    }
    public void CameraShake()
    {
        foreach(GameObject player in GameManager.instance.playerSpawners)
        {
            player.GetComponentInChildren<CinemachineShake>().ShakeScreen();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bullet")|| collision.CompareTag("Indicator") || collision.CompareTag("Confiner")) return;
        switch (type)
        {
            case BulletType.normal:
                ani.SetTrigger("Hit");
                StopMove();
                break;
            case BulletType.strengthen:
                Explosion();
                break;

        }
        
    }
    public void Init(int damage, Vector2 dir)
    {
        this.damage = damage;
        rb.velocity = dir.normalized*bulletSpeed;
    }
    public void DestroyBullet()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        BulletBehaviorOutOfRange(initialPos);
    }

    private void BulletBehaviorOutOfRange(Vector3 pos)
    {
        if (!gameObject.activeSelf) return;
        float distance = Vector3.Distance(pos, transform.position);
        if (distance > GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().attackRange)
        {
            switch (type)
            {
                case BulletType.normal:
                    StopMove();
                    ani.SetTrigger("Hit");
                    break;
                case BulletType.strengthen:
                    Explosion();
                    break;
            }

        }
    }

    public void StopMove()
    {
        rb.velocity = Vector2.zero;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null; 
    }
    private void OnDisable()
    {
        if (type == BulletType.strengthen)
            ani.enabled = false;
        transform.position = initialPos;
        GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = initialSprite;
    }
}
