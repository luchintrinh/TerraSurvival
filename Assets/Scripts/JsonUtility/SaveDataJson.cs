using System.IO;
using UnityEngine;

public class SaveDataJson : MonoBehaviour
{
    UIShopCanvas canvas;
    private void Awake()
    {
        canvas = GetComponent<UIShopCanvas>();
        Init();
    }
    public void Init()
    {
        string json = FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem");
        if (json == null) return;
        canvas.playerStore = JsonUtility.FromJson<GameItem>(json).playerList; 
        canvas.weaponStore = JsonUtility.FromJson<GameItem>(json).weaponList;
    }
    public void SaveGameItem(Player[] players, WeaponObject[] weapons)
    {
        GameItem item = new GameItem();
        item.playerList = players;
        item.weaponList = weapons;
        FindObjectOfType<JsonUtilityReadWrite>().SaveToJsonUtility(item, "GameItem");
    }
}
