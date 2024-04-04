using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadDataChoosing : MonoBehaviour
{
    public int playerChooseTurn;

    public GameObject containerCharacter;
    public GameObject containerWeapon;

    [Header("# List Item")]
    public Player[] playerList;
    public WeaponObject[] weaponList;

    public GameObject item;

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        playerChooseTurn = 0;
    }

    private void Init()
    {
        string json = FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem");
        if (json == null) return;
         playerList = JsonUtility.FromJson<GameItem>(json).playerList;
        weaponList = JsonUtility.FromJson<GameItem>(json).weaponList;
    }

    public void LoadCharacterList()
    {
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].isLock) continue;
            GameObject character = Instantiate(item, containerCharacter.transform);
            int index = i;
            character.GetComponent<Button>().onClick.AddListener(delegate { ChoosingCharacter(index); });
            character.transform.GetChild(1).GetComponent<Image>().sprite = playerList[i].playerSprite;
            
        }
    }
    public void LoadWeaponList()
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            if (weaponList[i].isLock) continue;
            GameObject weapon = Instantiate(item, containerWeapon.transform);
            int index = i;
            weapon.GetComponent<Button>().onClick.AddListener(delegate { ChoosingWeapon(index); });
            weapon.transform.GetChild(1).GetComponent<Image>().sprite = weaponList[i].weaponSprite;
        }
    }
    

    public void ChoosingCharacter(int index)
    {
        if (GameManager.instance.playerChosen.Count>playerChooseTurn)
        {
            GameManager.instance.playerChosen[playerChooseTurn] = playerList[index];
        }
        else
        {
            GameManager.instance.playerChosen.Add(playerList[index]);
        }
        FindObjectOfType<UIMenuCanvas>().SetProperty();
        
    }

    public void ChoosingWeapon(int index)
    {
        
        if (GameManager.instance.weaponChosen.Count>playerChooseTurn)
        {
            GameManager.instance.weaponChosen[playerChooseTurn] =weaponList[index];
        }
        else
        {
            GameManager.instance.weaponChosen.Add(weaponList[index]);
        }
        FindObjectOfType<UIMenuCanvas>().SetProperty();
    }
}
