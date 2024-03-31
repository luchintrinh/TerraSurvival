using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpPlayer : MonoBehaviour
{
    [SerializeField] GameObject levelUpPopUp;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    public enum Type { moveSpeed, attackDamage, cooldownNormal, coolDownUltimate, rangeAttack, maxHealth }
    [SerializeField] List<LevelUpSkillObject> skills;

    public void LevelUpShowUp()
    {
        int index1 = Random.Range(0, skills.Count);
        int index2 = Random.Range(0, skills.Count);
        while (index2 == index1)
        {
            index2 = Random.Range(0, skills.Count);
        }
        int index3 = Random.Range(0, skills.Count);
        while(index3==index1|| index3 == index2)
        {
            index3 = Random.Range(0, skills.Count);
        }
        SetValue(skills[index1], item1);
        SetValue(skills[index2], item2);
        SetValue(skills[index3], item3);
    }
    public void SetValue(LevelUpSkillObject level, GameObject item)
    {
        item.GetComponent<Button>().onClick.RemoveAllListeners();
        item.GetComponent<Button>().onClick.AddListener(() => LevelUpSelected(level));
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = level.nameSkill;
        item.transform.GetChild(1).GetComponent<Image>().sprite = level.sprite;
        item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = level.description;
    }

    public void LevelUpSelected(LevelUpSkillObject level)
    {
        switch (level.id)
        {
            case 0:
                GetComponent<PlayerSetting>().IncreaseMoveSpeed(level.ratio);
                break;
            case 1:
                GetComponent<PlayerSetting>().IncreaseDamage(level.ratio);
                break;
            case 2:
                GetComponent<PlayerSetting>().DecreaseTimeDelayAttack(level.ratio);
                break;
            case 3:
                GetComponent<PlayerSetting>().DecreaseTimeDelayAttackUltimate(level.ratio);
                break;  
            case 4:
                GetComponent<PlayerSetting>().IncreaseRangeAttack(level.ratio);
                break;
            case 5:
                GetComponent<PlayerSetting>().IncreaseMaxHealth(level.ratio);
                break;
        }
        FindObjectOfType<UIManagement>().CloseLevelUp();
    }
}
