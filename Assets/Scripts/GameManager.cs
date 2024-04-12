using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public enum aimingStyle { mouse, nearestEnemy }
    [Header("# Game Setting")]
    public aimingStyle aim;


    //pause game
    public bool isPause;


    [Header("# Coin")]
    private int coinPickup;
    public int CoinPickup { get => coinPickup; set => coinPickup = value; }




    [Header("# Player")]
    public List<GameObject> playerSpawners;
    public List<Player> playerChosen;
    public List<WeaponObject> weaponChosen;
    public int[] levels;


    
    public Player playerInfo;

    [Header("# Characters list")]
    public Player[] characters;
    public WeaponObject[] weapons;

    JsonUtilityReadWrite store;



    // aim setting
    public Vector3 direction;
    public Vector3 mousePos;
    // Get nearest enemy
    public Transform nearestEnemyPos;


    // Pool object
    public PoolManager pool;
    public PoolUIManager poolUI;



    [Header("# In Game")]
    public int killed;

    

    public void SaveOrGet()
    {
        PlayerPrefs.DeleteKey("GameItem");
        store = GetComponent<JsonUtilityReadWrite>();
        GamePlay play = new GamePlay(0, 0, 0);
        if (!PlayerPrefs.HasKey("GamePlay"))
        {
            store.SaveToJsonUtility(play, "GamePlay");
        }
        GameItem game = new GameItem();
        if (!PlayerPrefs.HasKey("GameItem"))
        {
            game.playerList = characters;
            game.weaponList = weapons;
            store.SaveToJsonUtility(game, "GameItem");
            File.WriteAllText(Application.dataPath + "/Data/GameItem.json", store.GetJsonUtility("GameItem"));
        }
        else
        {
            game = JsonUtility.FromJson<GameItem>(store.GetJsonUtility("GameItem"));
            characters = game.playerList;
            weapons = game.weaponList;
        }
    }

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
    }
    private void Start()
    {
        SaveOrGet();
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
