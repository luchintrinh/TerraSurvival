

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class UIShopCanvas : MonoBehaviour
{
    public enum Type { player, weapon }
    [Header("# Phan loai")]
    public Type type;

    [Header("# List product")]
    Player[] playerStore;
    WeaponObject[] weaponStore;

    [SerializeField] Transform itemListView;
    [SerializeField] GameObject weaponItemPrefab;
    [SerializeField] GameObject playerItemPrefab;
    [SerializeField] GameObject itemDetailPopup;

    [SerializeField] GameObject message;
 
    [Header("# Item detail Popup")]
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI nameText;

    [Header("# List  item")]
    List<Transform> children;

    [Header("# Coins")]
    private int coin;
    public int Coin { get => coin; set => coin = value; }
    [SerializeField] TextMeshProUGUI coinText;

    [Header("# Price")]
    [SerializeField] TextMeshProUGUI priceText;

    [Header("# Description")]
    [SerializeField] TextMeshProUGUI desc;

    [Header("# Get Sprite Manager")]
    SpritesManager spritesManager;

 

    private void Awake()
    {

        children = new List<Transform>();
        spritesManager = FindObjectOfType<SpritesManagement>().sprites;
        type = Type.player;
        GetItemInitial();

    }

    public void GetItemInitial()
    {
        if (!PlayerPrefs.HasKey("GameItem")) return;
        GameItem game = JsonUtility.FromJson<GameItem>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem"));
        playerStore = game.playerList;
        weaponStore = game.weaponList;
    }

    private void Start()
    {
        PlayerStore();
        UpdateCoin();
    }

    public void UpdateCoin()
    {
        Coin = JsonUtility.FromJson<GamePlay>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GamePlay")).coinNumber;
        coinText.GetComponent<TextMeshProUGUI>().text = Coin.ToString();
    }

    public void SaveGamePlay()
    {
        GamePlay play = JsonUtility.FromJson<GamePlay>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GamePlay"));
        play.coinNumber = Coin;
        FindObjectOfType<JsonUtilityReadWrite>().SaveToJsonUtility(play, "GamePlay");
    }

    public void ClearAllItem()
    {
        foreach(Transform child in itemListView)
        {
            Destroy(child.gameObject);
        }
        children.Clear();
    }
    public void PlayerStore()
    {
        type = Type.player;
        ClearAllItem();
        if (playerStore.Length == 0)
        {
            message.SetActive(true);
            return;
        }
        message.SetActive(false);
        for (int i=0; i< playerStore.Length; i++)
        {
            GameObject playerItem = Instantiate(playerItemPrefab, itemListView);
            children.Add(playerItem.transform);
            int index = i;
            if (playerStore[i].isLock)
            {
                playerItem.GetComponent<Button>().onClick.AddListener(() => OnClickItem(index));
            }
            Sprite sprite=spritesManager.listSprite[playerStore[i].spriteIndex];

            playerItem.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            playerItem.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = playerStore[i].namePlayer;
            if (!playerStore[i].isLock)
            {
                playerItem.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                playerItem.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void WeaponStore()
    {
        type = Type.weapon;
        ClearAllItem();
        if (weaponStore.Length == 0)
        {
            message.SetActive(true);
            return;
        }
        message.SetActive(false);
        for (int i = 0; i < weaponStore.Length; i++)
        {
            GameObject weaponItem = Instantiate(playerItemPrefab, itemListView);
            children.Add(weaponItem.transform);
            int index = i;
            if (weaponStore[i].isLock)
            {
                weaponItem.GetComponent<Button>().onClick.AddListener(() => OnClickItem(index));
            }
            weaponItem.transform.GetChild(0).GetComponent<Image>().sprite = spritesManager.listSprite[weaponStore[i].spriteIndex];
            weaponItem.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = weaponStore[i].weaponName;
            if (!weaponStore[i].isLock)
            {
                weaponItem.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                weaponItem.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void OnClickClear()
    {
        foreach(Transform child in children)
        {
            child.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnClickItem(int i)
    {
        switch (type)
        {
            case Type.player:
                OnClickClear();
                DisplayPropertyPlayer(playerStore[i]);
                itemDetailPopup.SetActive(true);
                
                itemSprite.GetComponent<Image>().sprite = spritesManager.listSprite[playerStore[i].spriteIndex];
                nameText.GetComponent<TextMeshProUGUI>().text = playerStore[i].namePlayer;
                priceText.GetComponent<TextMeshProUGUI>().text = playerStore[i].price.ToString();
                desc.GetComponent<TextMeshProUGUI>().text = playerStore[i].description;
                itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnClickBuyAndExit(i));
                if (Coin < playerStore[i].price)
                {
                    priceText.GetComponent<TextMeshProUGUI>().color = Color.red;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().interactable = false;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(true);
                }
                
                break;
            case Type.weapon:
                OnClickClear();
                DisplayPropertyWeapon(weaponStore[i]);
                itemDetailPopup.SetActive(true);
                itemSprite.GetComponent<Image>().sprite = spritesManager.listSprite[weaponStore[i].spriteIndex];
                nameText.GetComponent<TextMeshProUGUI>().text = weaponStore[i].weaponName;
                priceText.GetComponent<TextMeshProUGUI>().text = weaponStore[i].price.ToString();
                desc.GetComponent<TextMeshProUGUI>().text = weaponStore[i].description;
                itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnClickBuyAndExit(i));
                if (Coin < weaponStore[i].price)
                {
                    priceText.GetComponent<TextMeshProUGUI>().color = Color.red;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().interactable = false;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(true);
                }
                
                break;
        }
        
    }
    public void ExitItemDetailPopUp()
    {
        itemDetailPopup.SetActive(false);
    }

    public void UpdateData()
    {
        GameItem game = new GameItem();
        game.playerList = playerStore;
        game.weaponList = weaponStore;
        FindObjectOfType<JsonUtilityReadWrite>().SaveToJsonUtility(game, "GameItem");
    }

    public void OnClickBuyAndExit(int i)
    {
        switch (type)
        {
            case Type.player:
                Coin -= playerStore[i].price;
                SaveGamePlay();
                playerStore[i].isLock = false;
                ExitItemDetailPopUp();
                UpdateData();
                PlayerStore();
                UpdateCoin();
                break;
            case Type.weapon:
                Coin -= weaponStore[i].price;
                SaveGamePlay();
                weaponStore[i].isLock = false;
                ExitItemDetailPopUp();
                UpdateData();
                WeaponStore();
                UpdateCoin();
                break;
        }

        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void DisplayPropertyPlayer(Player player)
    {
        transform.GetChild(1).GetChild(1).GetComponent<UIDetailPopUp>().SetValuePropertyCharacter(player);
    }
    public void DisplayPropertyWeapon(WeaponObject weapon)
    {
        transform.GetChild(1).GetChild(1).GetComponent<UIDetailPopUp>().SetValuePropertyWeapon(weapon);
    }
}
