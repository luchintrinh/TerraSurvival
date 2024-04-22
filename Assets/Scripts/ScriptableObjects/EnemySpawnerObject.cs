using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new EnemySpawner", menuName ="new EnemySpawner")]
public class EnemySpawnerObject : ScriptableObject
{
    public int waveNumber;
    public int enemySpawnMorePerTime;
    public int[] enemiesOfWave;
    public int[] waveDelayEachOther;
    public int[] cooldownPercentAllProperty;
}
