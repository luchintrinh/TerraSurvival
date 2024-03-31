using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{

    public int damage;
    public float bulletSpeed;
    Rigidbody2D rb;
    Animator ani;
    [SerializeField] Sprite initialSprite;
    public Vector3 initialPos;

    public float attackRange = 10f;


    //SFX sounds

    SoundManager sfx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Bullet") || collision.CompareTag("EBullet") || collision.CompareTag("Indicator") || collision.CompareTag("Trigger") || collision.CompareTag("Confiner")) return;
        ani.SetTrigger("Hit");
        StopMove();
        if(collision.CompareTag("Player"))
        collision.GetComponent<HealthManager>().TakeDamagePlayer(damage);


    }
    public void Init(int damage, Vector2 dir, float range)
    {
        this.damage = damage;
        this.attackRange = range;
        rb.velocity = dir.normalized * bulletSpeed;
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
        if (distance > attackRange)
        {
            StopMove();
            ani.SetTrigger("Hit");

        }
    }

    public void StopMove()
    {
        rb.velocity = Vector2.zero;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
    private void OnDisable()
    {
        transform.position = initialPos;
        GetComponent<SpriteRenderer>().sprite = null;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = initialSprite;
    }
}
