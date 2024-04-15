
using UnityEngine;

[System.Serializable]
public class Player 
{
    [Header("# Property")]
    public string namePlayer;
    public string description;


    public float baseSpeed;
    public int baseMaxHealth;
    public int basePhysicDamage;
    [Header("# Player Animator")]
    public string playerAniStr;

    [Header("# Sprites")]
    public int spriteIndex;

    //[Header("# Lock")]
    public bool isLock;
    public int price;
}
