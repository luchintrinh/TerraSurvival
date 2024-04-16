using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] pointSpawn;
    public bool isReadySpawn;


    //way setting
    public int waveNumber = 3;
    public int[] enemiesOfWave;
    public int[] waveDelayEachOther;
    public int[] waveCooldownPercentAllProperty;


    // count of enemies
    public int enemyCount;
    public int enemyDead;


    //wave number
    public int wave;

    Coroutine spawnEnemy;

    GameObject preEnemy =  null;
    void Start()
    {
        isReadySpawn = true;
        StartCoroutine();
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

                
                int enemyID = Random.Range(2, wave + 3);
                int enemySpawnPoint = Random.Range(0, pointSpawn.Length - 1);
                GameObject enemy = GameManager.instance.pool.Get(enemyID);
                enemy.GetComponent<Enemy>().SetProperty(waveCooldownPercentAllProperty[wave]);
                preEnemy = enemy;
                enemy.transform.position = pointSpawn[enemySpawnPoint].position;
                enemyCount++;
                
            }
    }
    public void BossSpawn()
    {
        if (wave == waveNumber-1 && enemyDead==enemiesOfWave[wave])
        {
            FindObjectOfType<UIManagement>().SetInformationWarning($"Boss xuất hiện", Color.red);
            GameObject enemy = GameManager.instance.pool.Get(8);
            enemy.transform.position = pointSpawn[4].position;
        }
    }
}
