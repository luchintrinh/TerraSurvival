using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New weapon", menuName ="new Weapon")]
public class WeaponObject : ScriptableObject
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
    public Sprite weaponSprite;
    public RuntimeAnimatorController weaponAni;

    [Header("# bullets ")]
    public int bulletPoolIndex;
    public int bulletStrengthenPoolIndex;
}
