using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{

    public Player player;
    public WeaponObject weapon;

    public int physicDamage;
    public int mageDamage;
    public float moveSpeed;
    public int maxHealth;
    public int explosionDamage;
    public float forceBack;
    public float attackRange;
    public float attackDelay;
    public float attackDelayUltimate;
    public float explosionRange;
    public int ratio=2;


    // in game
    [Header("# InGame")]
    public int level;
    public int exp;



    

    private void Awake()
    {
        if (GameManager.instance.playerSpawners.Count != 0) GameManager.instance.playerSpawners.Clear();
        GameManager.instance.playerSpawners.Add(this.gameObject);
        GameManager.instance.Init();
        InitPlayer();
        level = 1;
    }

    private void InitPlayer()
    {
        physicDamage = player.basePhysicDamage + weapon.bonusPhysicDamage;
        mageDamage = player.baseMagicalDamage + weapon.bonusMagicalDamage;
        moveSpeed = player.baseSpeed + weapon.bonusMoveSpeed;
        maxHealth = player.baseMaxHealth + weapon.bonusMaxHealth;
        explosionDamage = weapon.explosionDamage;
        forceBack = weapon.forceBack;
        attackRange = weapon.attackRange;
        attackDelay = weapon.attackDelay;
        attackDelayUltimate = weapon.attackDelayUltimate;
        explosionRange = weapon.explosionRange;
    }

    public float UpgradeWeapon(float property, float ratio)
    {
        float value = property + level * ratio * property / 100;
        return value;
    }
    public float DecreaseTimeDelay(float property, float ratio)
    {
        float value = property - level * ratio * property / 100;
        return value;
    }

    public void LevelUp(int exp)
    {
        if (level == GameManager.instance.levels.Length) return;
        this.exp += exp;
        if (this.exp >= GameManager.instance.levels[level - 1])
        {
            level++;
            Invoke("OpenLevelUpUI", 1f);
        }
    }
    public void IncreaseMoveSpeed(float ratio)
    {
        moveSpeed = UpgradeWeapon(player.baseSpeed + weapon.bonusMoveSpeed, ratio);
        
    }
    public void DecreaseTimeDelayAttack(float ratio)
    {
        attackDelay = DecreaseTimeDelay(weapon.attackDelay, ratio);
    }

    public void DecreaseTimeDelayAttackUltimate(float ratio)
    {
        attackDelayUltimate = DecreaseTimeDelay(weapon.attackDelayUltimate, ratio);
    }

    public void IncreaseDamage(float ratio)
    {
        physicDamage = (int)Mathf.Ceil(UpgradeWeapon(player.basePhysicDamage + weapon.bonusPhysicDamage, ratio));
    }
    public void IncreaseMaxHealth(float ratio)
    {
        maxHealth = (int)Mathf.Ceil(UpgradeWeapon(player.baseMaxHealth + weapon.bonusMaxHealth, ratio));
    }
    public void IncreaseExplosion(float ratio)
    {
        explosionDamage = (int)Mathf.Ceil(UpgradeWeapon(weapon.explosionDamage, ratio));
    }

    public void IncreaseRangeAttack(float ratio)
    {
        attackRange = UpgradeWeapon(weapon.attackRange, ratio);
    }


    public void OpenLevelUpUI()
    {
        FindObjectOfType<UIManagement>().LevelUp();
        GetComponent<LevelUpPlayer>().LevelUpShowUp();
    }

    private void Start()
    {   
        Init();
    }
    void Init()
    {
        if (!weapon.isGun)
        {
            transform.GetChild(2).GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Player/{weapon.weaponAniStr}");
        }
        else
        {
            transform.GetChild(2).GetComponent<Animator>().enabled = false;
            transform.GetChild(2).localPosition = new Vector3(0, -0.3f, 0);
        }
        transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Player/{player.playerAniStr}");
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Weapon/{weapon.weaponSpriteName}");
    }
   
}
