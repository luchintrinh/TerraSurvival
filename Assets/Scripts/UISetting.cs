using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [SerializeField] GameObject optionsSetting;
    [SerializeField] GameObject generalSetting;
    [SerializeField] GameObject gamePlaySetting;


    // General setting
    [SerializeField] Toggle muteMusicToggle;
    [SerializeField] Toggle muteSFXToggle;
    [SerializeField] Slider volumnSlider;

    // Gameplay setting

    [SerializeField] Dropdown aimingDropdown;


    //Sounds

    SoundManager sound;

    private void Awake()
    {
        sound = FindObjectOfType<SoundManager>();
    }


    public void OpenGeneralSetting()
    {
        optionsSetting.SetActive(false);
        generalSetting.SetActive(true);
        muteSFXToggle.isOn = sound.muteSFX;
        muteMusicToggle.isOn = sound.muteMusic;
        volumnSlider.value = sound.volume;
        
    }

    public void OpenOptionsSetting()
    {
        generalSetting.SetActive(false);
        gamePlaySetting.SetActive(false);
        optionsSetting.SetActive(true);
        SaveSoundSetting();
    }
    public void OpenGamePlaySetting()
    {
        aimingDropdown.value = (int)GameManager.instance.aim;
        gamePlaySetting.SetActive(true);
        optionsSetting.SetActive(false);
    }

    public void SaveSoundSetting()
    {
        sound.muteMusic = muteMusicToggle.isOn;
        sound.muteSFX = muteSFXToggle.isOn;
        sound.volume = volumnSlider.value;
        sound.ChangeSoundSetting();
    }


    public void OnChangeAimingDropDown()
    {
        GameManager.instance.aim = aimingDropdown.value == 0 ? GameManager.aimingStyle.mouse : GameManager.aimingStyle.nearestEnemy;
    }
}
