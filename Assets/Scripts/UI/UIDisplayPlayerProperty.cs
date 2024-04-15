using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplayPlayerProperty : MonoBehaviour
{
    public GameObject containerList;
    [SerializeField] GameObject itemListPrefabs;
    private Player player;
    private WeaponObject weapon;

    public void ClearAllProperty()
    {
        foreach(Transform obj in containerList.transform)
        {
            Destroy(obj.gameObject);
        }
    }
    public void SetPlayer(Player player)
    {
        this.player=player;
    }
    public void SetWeapon(WeaponObject weapon)
    {
        this.weapon = weapon;
    }

    public void SetListValueCharacter()
    {
        ClearAllProperty();
        SetItemValue("Damage", player.basePhysicDamage);
        SetItemValue("Move Speed", player.baseSpeed);
        SetItemValue("Health", player.baseMaxHealth);
    }
    public void SetListValueWeapon()
    {
        ClearAllProperty();
        SetItemValue($"Damage (+{weapon.bonusPhysicDamage})", player.basePhysicDamage);
        SetItemValue($"Move Speed (+{weapon.bonusMoveSpeed})", player.baseSpeed);
        SetItemValue($"Health (+{weapon.bonusMaxHealth})", player.baseMaxHealth);
        SetItemValue("Attack Range)", weapon.attackRange);
        SetItemValue("Explosion Range)", weapon.explosionRange);
    }
    public void SetListValuePlayer()
    {
        ClearAllProperty();
        SetItemValue($"Damage)", player.basePhysicDamage+ weapon.bonusPhysicDamage);
        SetItemValue($"Move Speed", player.baseSpeed+ weapon.bonusMoveSpeed);
        SetItemValue($"Health", player.baseMaxHealth+ weapon.bonusMaxHealth);
        SetItemValue("Attack Range)", weapon.attackRange);
        SetItemValue("Explosion Range)", weapon.explosionRange);
    }
    public void SetListValuePlayerDetail(int damage, float speed, int health, float attackRange, float explosionRange, float cooldownNormal, float cooldownUltimate)
    {
        ClearAllProperty();
        SetItemValue($"Damage: ", damage);
        SetItemValue($"Move Speed: ", speed);
        SetItemValue($"Health: ", health);
        SetItemValue("Attack Range: ", attackRange);
        SetItemValue("Explosion Range: ", explosionRange);
        SetItemValue("Cooldown Normal: ", cooldownNormal);
        SetItemValue("Cooldown Ultimate: ", cooldownUltimate);
    }

    public void SetItemValue(string text, int value)
    {
        GameObject item = Instantiate(itemListPrefabs, containerList.transform);
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
    public void SetItemValue(string text, float value)
    {
        GameObject item = Instantiate(itemListPrefabs, containerList.transform);
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
