using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public enum aimingStyle { mouse, nearestEnemy }
    [Header("# Game Setting")]
    public aimingStyle aim;


    //pause game
    public bool isPause;




    [Header("# Player")]
    public List<GameObject> playerSpawners;
    public List<Player> playerChosen;
    public List<WeaponObject> weaponChosen;
    public int[] levels;


    
    public Player playerInfo;

    [Header("# Characters list")]
    public List<Player> characters;
    public List<WeaponObject> weapons;



    // aim setting
    

    
    public Vector3 direction;
    public Vector3 mousePos;
    // Get nearest enemy
    public Transform nearestEnemyPos;

    public PoolManager pool;





    private void Awake()
    {

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        isPause = false;

        playerChosen.Add(characters[0]);
        weaponChosen.Add(weapons[0]);
    }
    public void DestroyGame()
    {
        Destroy(instance);
    }
    public void Init()
    {
        for(int i=0; i<playerSpawners.Count; i++)
        {
            playerSpawners[i].GetComponent<PlayerSetting>().player = playerChosen[i];
            playerSpawners[i].GetComponent<PlayerSetting>().weapon = weaponChosen[i];
        }
    }

}
