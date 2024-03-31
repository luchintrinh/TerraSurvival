using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    ParticleSystem hitEffect;
    HealthManager health;

    Animator ani;

    public Transform player;
    // Enemy object

    public EnemyObject enemy;


    // Property

    public int currentHealth;
    public int maxHealth;
    public int attackDamage;
    public float moveSpeed;
    public float timeSpawnDelay;


    public float enemyMoveSpeed;
    private void Awake()
    {
        health = GetComponent<HealthManager>();
        Init();
    }
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hitEffect = transform.GetChild(1).GetComponent<ParticleSystem>();
        ani = GetComponent<Animator>();
        SetProperty(0);
    }

    public void Init()
    {
        maxHealth = enemy.maxHealth;
        currentHealth = enemy.maxHealth;
        attackDamage = enemy.damage;
        moveSpeed = enemy.speed;
        timeSpawnDelay = enemy.spawnDelayTime;
    }
    private void OnEnable()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<HealthManager>().health = GetComponent<HealthManager>().maxHealth;
        health.isLive = true;
    }
    private void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        rb.velocity = Vector2.zero;
        player = GetComponent<FindNearest>().FindPlayer();
        if (!player||!health.isLive) return;
        
        if (GetComponent<HealthManager>().type == HealthManager.Type.Boss)
        {
            ani.SetBool("Run", player!=null);
        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack_Goblin")) return;
        rb.MovePosition(transform.position + GetDirection(player.position, transform.position) * enemyMoveSpeed * Time.deltaTime);
    }
    private void LateUpdate()
    {
        if (!health.isLive) return;
        if (!GetComponent<FindNearest>().FindPlayer()) return;
        sprite.flipX = transform.position.x < GetComponent<FindNearest>().FindPlayer().position.x ? false : true;
    }

    Vector3 GetDirection(Vector3 target, Vector3 myPos)
    {
        Vector3 dir = target - myPos;
        dir = dir.normalized;
        return dir;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !health.isLive) return;
        Vector3 dir = GetDirection(transform.position, collision.transform.position);
        float force = GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().forceBack;
        WasAttacked(dir, force);
        GetComponent<HealthManager>().TakeDamage(GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().physicDamage);
    }
    
    public void WasAttacked(Vector3 dir, float forceBack)
    {
        if (GetComponent<HealthManager>().type == HealthManager.Type.Boss) return;
        ani.SetTrigger("Hit");
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * forceBack, ForceMode2D.Impulse);
        hitEffect.Play();
    }



    public void SetProperty(int percent)
    {
        maxHealth =enemy.maxHealth+(maxHealth*percent)/100;
        currentHealth = maxHealth;
        moveSpeed = enemy.speed + (moveSpeed * percent) / 100;
        timeSpawnDelay = enemy.spawnDelayTime - (timeSpawnDelay * percent) / 100;
        attackDamage = enemy.damage + (attackDamage * percent) / 100;
    }

}
