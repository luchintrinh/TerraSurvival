using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMenuCanvas : MonoBehaviour
{
    [Header("# Character")]
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Image characterSprite;

    [Header("# Weapon")]
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Image weaponSprite;

    public void SetProperty()
    {
        characterName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.playerChosen[0].namePlayer;
        characterSprite.sprite = Resources.Load<Sprite>($"/Sprite/Player/{GameManager.instance.playerChosen[0].playerSpriteName}");
        weaponName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.weaponChosen[0].weaponName;
        weaponSprite.sprite = Resources.Load<Sprite>($"Sprites/Weapon/{GameManager.instance.weaponChosen[0].weaponSpriteName}");
    }

}
