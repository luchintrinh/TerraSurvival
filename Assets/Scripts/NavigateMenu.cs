using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class NavigateMenu : MonoBehaviour
{
    public RectTransform mainMenu;
    public RectTransform chooseCharacter;
    public RectTransform chooseWeapon;
    public RectTransform settingScene;

    public Vector2 initialPosMainMenu=new Vector2(0, 0);
    public Vector2 initialPosChooseCharacter = new Vector2(1920, 0);
    public Vector2 initialPosChooseWeapon = new Vector2(3840, 0);
    public Vector2 initialPosSettingScene = new Vector2(0, 1080);
    public Vector2 jumpSpace=new Vector2(1920, 0);
    // Start is called before the first frame update


    public Canvas gamePlayCanvas;
    void Start()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.5f, false);
        FindObjectOfType<LoadDataChoosing>().LoadCharacterList();
        FindObjectOfType<LoadDataChoosing>().LoadWeaponList();
    }

    public void SettingScene()
    {
        settingScene.DOAnchorPos(new Vector2(0, 0), 1, false);
    }

    public void CloseSettingScene()
    {
        settingScene.DOAnchorPos(initialPosSettingScene, 1, false);
    }
    public void MainMenuScene()
    {
        mainMenu.DOAnchorPos(initialPosMainMenu, 1, false);
        chooseCharacter.DOAnchorPos(initialPosChooseCharacter, 1, false);
        chooseWeapon.DOAnchorPos(initialPosChooseWeapon, 1, false);
    }
    public void ChooseCharacterScene()
    {
        mainMenu.DOAnchorPos(initialPosMainMenu-jumpSpace, 1, false);
        chooseCharacter.DOAnchorPos(initialPosChooseCharacter-jumpSpace, 1, false);
        chooseWeapon.DOAnchorPos(initialPosChooseWeapon-jumpSpace, 1, false);
        Debug.Log(mainMenu.position);
    }
    public void ChooseWeaponScene()
    {
        mainMenu.DOAnchorPos(initialPosMainMenu-jumpSpace*2, 1, false);
        chooseCharacter.DOAnchorPos(initialPosChooseCharacter-jumpSpace*2, 1, false);
        chooseWeapon.DOAnchorPos(initialPosChooseWeapon - jumpSpace * 2, 1, false);
    }
    public void StartPlay()
    {
        SceneManager.LoadScene(1);
    }
}
