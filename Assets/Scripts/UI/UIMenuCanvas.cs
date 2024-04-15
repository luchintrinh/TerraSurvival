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
        characterSprite.sprite = FindObjectOfType<SpritesManagement>().sprites.listSprite[GameManager.instance.playerChosen[0].spriteIndex];
        weaponName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.weaponChosen[0].weaponName;
        weaponSprite.sprite = FindObjectOfType<SpritesManagement>().sprites.listSprite[GameManager.instance.weaponChosen[0].spriteIndex];
    }

}
