using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponObject
{
    [Header("# IsGun")]
    public bool isGun;
    public string weaponName;
    public string description;
    public int bonusPhysicDamage;
    public int bonusMagicalDamage;
    public int explosionDamage;
    public float explosionRange;
    public int strengthenDamage;
    public int bonusMaxHealth;
    public float bonusArmor;
    public float bonusMoveSpeed;
    public float attackDelay;
    public float attackDelayUltimate;
    public float forceBack;
    public float attackRange;

    [Header("# Weapon")]
    public string weaponSpriteName;
    public string weaponAniStr;

    [Header("# bullets ")]
    public int bulletPoolIndex;
    public int bulletStrengthenPoolIndex;

    [Header("# Lock")]
    public bool isLock;
    public int price;
}
