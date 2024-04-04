using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player", menuName ="New player")]
public class Player : ScriptableObject
{
    [Header("# Property")]
    public string namePlayer;
    public string description;
    public float baseSpeed;
    public int baseMaxHealth;
    public int baseHealth;
    public int basePhysicDamage;
    public int baseMagicalDamage;
    [Header("# Player Animator")]
    public RuntimeAnimatorController playerAni;

    [Header("# Sprites")]

    public Sprite playerSprite;

    [Header("# Lock")]
    public bool isLock;
    public int price;


    

}
