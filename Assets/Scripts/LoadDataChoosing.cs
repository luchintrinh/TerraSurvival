using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDataChoosing : MonoBehaviour
{
    public int playerChooseTurn;

    public GameObject containerCharacter;
    public GameObject containerWeapon;

    public GameObject item;
    public void LoadCharacterList()
    {
        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            GameObject character = Instantiate(item, containerCharacter.transform);
            int index = i;
            character.GetComponent<Button>().onClick.AddListener(delegate { ChoosingCharacter(index); });
            character.transform.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.characters[i].playerSprite;
            
        }
    }
    public void LoadWeaponList()
    {
        for (int i = 0; i < GameManager.instance.weapons.Count; i++)
        {
            GameObject character = Instantiate(item, containerWeapon.transform);
            int index = i;
            character.GetComponent<Button>().onClick.AddListener(delegate { ChoosingWeapon(index); });
            character.transform.GetChild(1).GetComponent<Image>().sprite = GameManager.instance.weapons[i].weaponSprite;
        }
    }
    private void Start()
    {
        playerChooseTurn = 0;
    }

    public void ChoosingCharacter(int index)
    {
        if (GameManager.instance.playerChosen.Count>playerChooseTurn)
        {
            GameManager.instance.playerChosen[playerChooseTurn] = GameManager.instance.characters[index];
        }
        else
        {
            GameManager.instance.playerChosen.Add(GameManager.instance.characters[index]);
        }
        FindObjectOfType<UIMenuCanvas>().SetProperty();
        
    }

    public void ChoosingWeapon(int index)
    {
        
        if (GameManager.instance.weaponChosen.Count>playerChooseTurn)
        {
            GameManager.instance.weaponChosen[playerChooseTurn] = GameManager.instance.weapons[index];
        }
        else
        {
            GameManager.instance.weaponChosen.Add(GameManager.instance.weapons[index]);
        }
        FindObjectOfType<UIMenuCanvas>().SetProperty();
    }
}
