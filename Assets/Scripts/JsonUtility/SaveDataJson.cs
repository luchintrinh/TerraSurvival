using System.IO;
using UnityEngine;

public class SaveDataJson : MonoBehaviour
{
    UIShopCanvas canvas;
    private void Awake()
    {
        canvas = GetComponent<UIShopCanvas>();
    }
    public void SaveGameItem(Player[] players, WeaponObject[] weapons)
    {
        GameItem item = new GameItem();
        item.playerList = players;
        item.weaponList = weapons;
        
    }
}
