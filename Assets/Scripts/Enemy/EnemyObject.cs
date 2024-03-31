using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Enemy", menuName ="New Enemy")]
public class EnemyObject : ScriptableObject
{
    public string enemyName;
    public string description;
    public int health;
    public int maxHealth;
    public int damage;
    public float speed;
    public float spawnDelayTime;
    public float attackRange;
    public int getExp;
}
