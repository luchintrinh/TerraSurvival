using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDetailPopUp : MonoBehaviour
{
    public enum Type { player, weapon}
    public Type type;
    [SerializeField] GameObject listProperty;
    [SerializeField] GameObject propertyItemPrefab;
    public Player player;
    public WeaponObject weapon;

    private void OnEnable()
    {
        //SetValuePopup();
    }
    public void SetValuePopup()
    {
        switch (type)
        {
            case Type.player:
                if (player != null)
                    SetValuePropertyCharacter(player);
                break;
            case Type.weapon:
                if (weapon != null)
                    SetValuePropertyWeapon(weapon);
                break;
        }
    }

    public void SetValuePropertyCharacter(Player player)
    {
        DestroyProperty();
        SetRowValue("Damage", player.basePhysicDamage);
        SetRowValue("Speed", player.baseSpeed);
        SetRowValue("Health", player.baseMaxHealth);
    }
    public void SetValuePropertyWeapon(WeaponObject weapon)
    {
        DestroyProperty();
        SetRowValue("Bonus Damage", weapon.bonusPhysicDamage);
        SetRowValue("Bonus Speed", weapon.bonusMoveSpeed);
        SetRowValue("Bonus Health", weapon.bonusMaxHealth);
    }

    void SetRowValue(string name, int value)
    {
        GameObject item = Instantiate(propertyItemPrefab, transform);
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    void SetRowValue(string name, float value)
    {
        GameObject item = Instantiate(propertyItemPrefab, transform);
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
    void DestroyProperty()
    {
        foreach (Transform obj in listProperty.transform)
        {
            Destroy(obj.gameObject);
        }
    }
}
