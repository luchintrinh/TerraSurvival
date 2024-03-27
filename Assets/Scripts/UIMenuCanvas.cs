using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMenuCanvas : MonoBehaviour
{
    [Header("# Character")]
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] Image characterSprite;

    [Header("# Weapon")]
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Image weaponSprite;

    private void Start()
    {
        SetProperty();
    }
    public void SetProperty()
    {
        characterName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.playerChosen[0].namePlayer;
        characterSprite.sprite = GameManager.instance.playerChosen[0].playerSprite;
        weaponName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.weaponChosen[0].weaponName;
        weaponSprite.sprite = GameManager.instance.weaponChosen[0].weaponSprite;
    }
}
