using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] pointSpawn;
    public bool isReadySpawn;

    public EnemySpawnerObject spawner;

    //way setting
    public int waveNumber = 5;
    public int[] enemiesOfWave;
    int[] waveDelayEachOther;
    int[] waveCooldownPercentAllProperty;

    public int spawnEnemyNumberOfTime = 0;

    [Header("# Index enemies in Pool")]
    public int[] indexEnemies;


    // count of enemies
    public int enemyCount;
    public int enemyDead;


    //wave number
    public int wave;

    Coroutine spawnEnemy;

    GameObject preEnemy =  null;

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        isReadySpawn = true;
        StartCoroutine();
    }
    public void Init()
    {
        spawner = GameManager.instance.ranks[(int)GameManager.instance.rank];
        waveNumber = spawner.waveNumber;
        enemiesOfWave = spawner.enemiesOfWave;
        waveDelayEachOther = spawner.waveDelayEachOther;
        waveCooldownPercentAllProperty = spawner.cooldownPercentAllProperty;
    }


    public void StartNextWave()
    {
        if (enemyDead == enemiesOfWave[wave] && wave < waveNumber-1)
        {
            StopCoroutine();
            wave++;
            if (wave == waveNumber) return;
            StartCoroutine();
        }
        else if(wave==waveNumber) StopCoroutine();
    }

    public void StartCoroutine()
    {
        if(spawnEnemy==null)
        spawnEnemy = StartCoroutine(SpawnEnemyCoroutine());
        FindObjectOfType<UIManagement>().SetInformationWarning($"Wave {wave + 1}", Color.black);
    }
    public void StopCoroutine()
    {
        if (spawnEnemy != null)
        {
            StopCoroutine(spawnEnemy);
            spawnEnemy = null;
        }
        
    }
    IEnumerator SpawnEnemyCoroutine()
    {
            while (enemyCount<enemiesOfWave[wave])
        {
            if (preEnemy == null)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(preEnemy.GetComponent<Enemy>().timeSpawnDelay);
            }
            for(int i=0; i<wave+spawnEnemyNumberOfTime+1; i++)
            {
                if (enemyCount == enemiesOfWave[wave]) { break; }
                SpawnEnemyTurn();
            }

        }
    }

    private void SpawnEnemyTurn() {
            
        int enemyID = wave<indexEnemies.Length-1?Random.Range(0, wave+1):Random.Range(0, indexEnemies.Length);
        int enemySpawnPoint = Random.Range(0, pointSpawn.Length - 1);
        GameObject enemy = GameManager.instance.pool.Get(indexEnemies[enemyID]);
        enemy.GetComponent<Enemy>().SetProperty(waveCooldownPercentAllProperty[wave]);
        preEnemy = enemy;
        enemy.transform.position = pointSpawn[enemySpawnPoint].position;
        enemyCount++;
    }

    public void BossSpawn()
    {
        if (wave == waveNumber-1 && enemyDead==enemiesOfWave[wave])
        {
            FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.enemyBossAppear);
            FindObjectOfType<UIManagement>().SetInformationWarning($"Boss xuất hiện", Color.red);
            GameObject enemy = GameManager.instance.pool.Get(8);
            enemy.transform.position = pointSpawn[4].position;
        }
    }
}
