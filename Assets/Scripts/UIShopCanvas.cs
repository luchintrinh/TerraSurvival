

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIShopCanvas : MonoBehaviour
{
    public enum Type { player, weapon }
    [Header("# Phan loai")]
    public Type type;

    [Header("# List product")]
    public Player[] playerStore;
    public WeaponObject[] weaponStore;

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
    public int coin;
    [SerializeField] TextMeshProUGUI coinText;

    [Header("# Price")]
    [SerializeField] TextMeshProUGUI priceText;

    private void Awake()
    {

        children = new List<Transform>();
        type = Type.player;
        if(!PlayerPrefs.HasKey("GameItem"))
        FindObjectOfType<SaveDataJson>().SaveGameItem(playerStore, weaponStore);
        playerStore = JsonUtility.FromJson<GameItem>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem")).playerList;
        weaponStore = JsonUtility.FromJson<GameItem>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GameItem")).weaponList;

    }

    private void Start()
    {
        PlayerStore();
        UpdateCoin();
    }

    public void UpdateCoin()
    {
        coinText.GetComponent<TextMeshProUGUI>().text = coin.ToString();
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
            
            playerItem.transform.GetChild(0).GetComponent<Image>().sprite = playerStore[i].playerSprite;
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
            weaponItem.transform.GetChild(0).GetComponent<Image>().sprite = weaponStore[i].weaponSprite;
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
                itemDetailPopup.SetActive(true);
                itemSprite.GetComponent<Image>().sprite = playerStore[i].playerSprite;
                nameText.GetComponent<TextMeshProUGUI>().text = playerStore[i].namePlayer;
                priceText.GetComponent<TextMeshProUGUI>().text = playerStore[i].price.ToString();
                itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnClickBuyAndExit(i));
                if (coin < playerStore[i].price)
                {
                    priceText.GetComponent<TextMeshProUGUI>().color = Color.red;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().interactable = false;
                    itemDetailPopup.transform.GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(true);
                }
                break;
            case Type.weapon:
                OnClickClear();
                itemDetailPopup.SetActive(true);
                itemSprite.GetComponent<Image>().sprite = weaponStore[i].weaponSprite;
                nameText.GetComponent<TextMeshProUGUI>().text = weaponStore[i].weaponName;
                priceText.GetComponent<TextMeshProUGUI>().text = weaponStore[i].price.ToString();
                itemDetailPopup.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnClickBuyAndExit(i));
                if (coin < weaponStore[i].price)
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

    public void OnClickBuyAndExit(int i)
    {
        switch (type)
        {
            case Type.player:
                coin -= playerStore[i].price;
                playerStore[i].isLock = false;
                FindObjectOfType<SaveDataJson>().SaveGameItem(playerStore, weaponStore);
                ExitItemDetailPopUp();
                PlayerStore();
                UpdateCoin();
                break;
            case Type.weapon:
                coin -= weaponStore[i].price;
                weaponStore[i].isLock = false;
                FindObjectOfType<SaveDataJson>().SaveGameItem(playerStore, weaponStore);
                ExitItemDetailPopUp();
                WeaponStore();
                UpdateCoin();
                break;
        }
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
