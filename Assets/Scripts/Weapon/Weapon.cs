using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask enemyLayerMask;
    public enum weaponType { soldier , sniper, mage }
    public weaponType type;
    InputSystemManagement move;


    Vector3 initialPos;

    SpriteRenderer sprite;

    [Header("# Bullet")]
    [SerializeField] GameObject bulletPrebs;
    [SerializeField] GameObject muzzleFlash;

    [Header("# Pool Object")]
    [SerializeField] GameObject poolObject;
    [SerializeField] Transform firePoint;

    [Header("# Number Attack")]
    private int numberAttack = 1;
    public int NumberAttack { get => numberAttack; set => numberAttack = value; }





    //SFX sounds
    SoundManager sfx;

    

    private void Awake()
    {
        sfx = FindObjectOfType<SoundManager>();
    }


    void Start()
    {
        move = GetComponentInParent<InputSystemManagement>();
        sprite = GetComponent<SpriteRenderer>();
        if (!GetComponentInParent<PlayerSetting>().weapon.isGun)
        {
            sprite.flipX = true;
            transform.localPosition = Vector3.zero;
            initialPos = transform.localPosition;
        }
    }

    void Update()
    {
        GameManager.instance.nearestEnemyPos = transform.parent.GetComponent<FindNearest>().Find();
        if (!GameManager.instance.nearestEnemyPos  && !GetComponentInParent<PlayerSetting>().weapon.isGun && GameManager.instance.aim==GameManager.aimingStyle.nearestEnemy) move.isAttack = false;
        WeaponDirection();

    }
    private void LateUpdate()
    {
        WeaponFlipX();
    }


    // Weapon Direction change following mouse positon.
    void WeaponDirection()
    {
        
        switch (GameManager.instance.aim)
        {
            
            case GameManager.aimingStyle.mouse:
                WeaponDirectionDetail(GetComponentInParent<InputSystemManagement>().direction);
                break;
            case GameManager.aimingStyle.nearestEnemy:
                if (!GameManager.instance.nearestEnemyPos) return;
                Vector3 dir = GameManager.instance.nearestEnemyPos.position - transform.position;
                dir = dir.normalized;
                WeaponDirectionDetail(dir);
                break;
        }
        
    }

    // modify weapon direction
    public Vector3 WeaponDirectionDetail(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return dir;
    }

    // flip X weapon

    void WeaponFlipX()
    {
        Vector3 scale = transform.localScale;
        scale.y=transform.localRotation.eulerAngles.z<90||transform.localRotation.eulerAngles.z>270?1: -1;
        transform.localScale = scale;
    }

    public void Attack()
    {
        switch (type)
        {
            case weaponType.soldier:
                break;
            case weaponType.sniper:
                Fire();
                break;
            case weaponType.mage:
                break;
        }
    }

    public void AttackFinish()
    {
        if (GetComponentInParent<PlayerSetting>().weapon.isGun) return;
        GetComponent<Animator>().SetBool("Attack", false);
    }
    void Fire()
    {
        if (!GameManager.instance.nearestEnemyPos&&GameManager.instance.aim!=GameManager.aimingStyle.mouse) return;
        switch (GameManager.instance.aim)
        {
            case GameManager.aimingStyle.mouse:
                BulletModify(GameManager.instance.direction);
                break;
            case GameManager.aimingStyle.nearestEnemy:
                if (!GameManager.instance.nearestEnemyPos) return;
                Vector3 dir = GameManager.instance.nearestEnemyPos.position - transform.position;
                dir = dir.normalized;
                BulletModify(dir);
                break;
        }
      
    }

    // Modify bullet.
    void BulletModify(Vector3 dir)
    {
        if (!GetComponentInParent<PlayerSetting>().weapon.isGun)
        {
            sfx.playSFX(SoundManager.SFXType.sword);
            GetComponent<Animator>().SetBool("Attack", true);
        }
        else
        {
            
            GameObject muzzle_flash = Instantiate(muzzleFlash, transform.GetChild(0).transform);
            GameObject bullet = GameManager.instance.pool.Get(GetComponentInParent<PlayerSetting>().weapon.bulletPoolIndex);
            bullet.GetComponent<Bullet>().initialPos = transform.parent.transform.position;
            bullet.GetComponent<Bullet>().EnemyNumberAttack = numberAttack;
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
            bullet.GetComponent<Bullet>().Init(transform.parent.GetComponent<PlayerSetting>().physicDamage, dir);
            sfx.playSFX(SoundManager.SFXType.gunLaser);
            Destroy(muzzle_flash, 0.1f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if (!collision.GetComponent<HealthManager>().isLive || GetComponentInParent<PlayerSetting>().weapon.isGun) return;
        if (move.isStrengthen)
        {
            StartCoroutine(slashEffect(collision));

        }
        if(move.isAttack)
        AttackEnemy(collision);
    }

    IEnumerator slashEffect(Collider2D collision)
    {
        GameObject slash = GameManager.instance.pool.Get(7);

        float angle = Mathf.Atan2(GameManager.instance.direction.y, GameManager.instance.direction.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 90 )
        {
            slash.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            slash.GetComponent<SpriteRenderer>().flipY = false;
            
        }
        slash.transform.position = collision.gameObject.transform.position;
        slash.transform.rotation = Quaternion.Euler(0, 0, angle);
        yield return new WaitForSeconds(0.25f);
        slash.gameObject.SetActive(false);

    }


    void AttackEnemy(Collider2D collision)
    {
        Vector3 dir = collision.transform.position - transform.parent.transform.position;
        dir = dir.normalized;
        collision.GetComponent<Enemy>().WasAttacked(dir, GetComponentInParent<PlayerSetting>().weapon.forceBack);
        int damage = move.isStrengthen ? GetComponentInParent<PlayerSetting>().weapon.strengthenDamage : GetComponentInParent<PlayerSetting>().weapon.bonusPhysicDamage + GetComponentInParent<PlayerSetting>().player.basePhysicDamage;
        collision.GetComponent<HealthManager>().TakeDamage(damage);
    }


    public void ColliderEnable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void ColliderUnEnable()
    {
        GetComponent<Collider2D>().enabled = false;
    }

}
