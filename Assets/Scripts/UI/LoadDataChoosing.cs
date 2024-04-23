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
    public GameObject item;


    //get list 
    private Player[] characters;
    private WeaponObject[] weapons;


    private void Start()
    {
        playerChooseTurn = 0;
    }
    public void loadData()
    {
        if (!PlayerPrefs.HasKey("GameItem")) return;
        GameItem game = JsonUtility.FromJson<GameItem>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem"));
        characters = game.playerList;
        weapons = game.weaponList;
    }

    public void LoadCharacterAndWeapon()
    {
        loadData();
        LoadCharacterList();
        LoadWeaponList();
    }

    public void ClearCharacterAndWeapon()
    {
        foreach(Transform obj in containerCharacter.transform)
        {
            Destroy(obj.gameObject);
        }
        foreach (Transform obj in containerWeapon.transform)
        {
            Destroy(obj.gameObject);
        }
    }
    public void LoadCharacterList()
    {
        ClearCharacterAndWeapon();
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].isLock) continue;
            GameObject character = Instantiate(item, containerCharacter.transform);
            int index = i;
            character.GetComponent<Button>().onClick.AddListener(delegate { ChoosingCharacter(index); });
            Sprite sprite = FindObjectOfType<SpritesManagement>().sprites.listSprite[characters[i].spriteIndex];
            character.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            
        }
    }
    public void LoadWeaponList()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].isLock) continue;
            GameObject weapon = Instantiate(item, containerWeapon.transform);
            int index = i;
            weapon.GetComponent<Button>().onClick.AddListener(delegate { ChoosingWeapon(index); });
            weapon.transform.GetChild(0).GetComponent<Image>().sprite = FindObjectOfType<SpritesManagement>().sprites.listSprite[weapons[i].spriteIndex];
        }
    }
    

    public void ChoosingCharacter(int index)
    {
        if (GameManager.instance.playerChosen.Count>playerChooseTurn)
        {
            GameManager.instance.playerChosen[playerChooseTurn] = characters[index];
        }
        else
        {
            GameManager.instance.playerChosen.Add(characters[index]);
        }
        if(GameManager.instance.playerChosen.Count>0)
        FindObjectOfType<UIDisplayPlayerProperty>().SetPlayer(GameManager.instance.playerChosen[0]);
        FindObjectOfType<UIDisplayPlayerProperty>().SetListValueCharacter();
        FindObjectOfType<UIMenuCanvas>().SetProperty();
        
    }

    public void ChoosingWeapon(int index)
    {
        
        if (GameManager.instance.weaponChosen.Count>playerChooseTurn)
        {
            GameManager.instance.weaponChosen[playerChooseTurn] = weapons[index];
        }
        else
        {
            GameManager.instance.weaponChosen.Add(weapons[index]);
        }
        if(GameManager.instance.weaponChosen.Count>0)
        FindObjectOfType<UIDisplayPlayerProperty>().SetWeapon(GameManager.instance.weaponChosen[0]);
        FindObjectOfType<UIDisplayPlayerProperty>().SetListValueWeapon();
        FindObjectOfType<UIMenuCanvas>().SetProperty();
    }
}
