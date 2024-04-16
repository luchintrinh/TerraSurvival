
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpPlayer : MonoBehaviour
{
    [SerializeField] GameObject levelUpPopUp;
    [SerializeField] GameObject[] itemSkill;
    public enum Type { moveSpeed, attackDamage, cooldownNormal, coolDownUltimate, rangeAttack, maxHealth }
    [SerializeField] List<LevelUpSkillObject> skills;

    [Header("# Skills Upgraded List")]
    public List<LevelUpSkillObject> skillUpgraded;

    public GameObject listViewSkillsUpgraded;
    

    PlayerSetting player;

    private void Start()
    {
        player = GetComponent<PlayerSetting>();
        ResetLevelSkill();
    }
    public void ResetLevelSkill()
    {
        foreach(LevelUpSkillObject i in skills)
        {
            i.level = 0;
        }
    }

    public void LevelUpShowUp()
    {
        List<LevelUpSkillObject> skillAvailable=new List<LevelUpSkillObject>();
        
        if (skillUpgraded.Count == 6)
        {
            foreach(LevelUpSkillObject item in skillUpgraded)
            {
                if (item.level < 5)
                {
                    skillAvailable.Add(item);
                }
            }
        }
        else
        {
            foreach (LevelUpSkillObject skill in skills)
            {
                if (skill.level != 5)
                    skillAvailable.Add(skill);
            }
        }
        for(int i=0; i<itemSkill.Length; i++)
        {
            if (skillAvailable.Count == 0) return;
            int index = Random.Range(0, skillAvailable.Count);
            itemSkill[i].gameObject.SetActive(true);
            SetValue(skillAvailable[index], itemSkill[i]);
            skillAvailable.Remove(skillAvailable[index]);
        }
    }
    public void SetValue(LevelUpSkillObject level, GameObject item)
    {
        item.GetComponent<Button>().onClick.RemoveAllListeners();
        item.GetComponent<Button>().onClick.AddListener(() => LevelUpSelected(level));
        item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = level.nameSkill;
        item.transform.GetChild(1).GetComponent<Image>().sprite = level.sprite;
        item.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = level.description;
        item.GetComponentInChildren<UISkillUpgraded>().SetSkillUpgradedInfor(level);

    }

    public void LevelUpSelected(LevelUpSkillObject level)
    {
        level.level++;
        if (!skillUpgraded.Exists(obj => obj.id == level.id))
        {
            skillUpgraded.Add(level);
            listViewSkillsUpgraded.transform.GetChild(skillUpgraded.Count - 1).gameObject.SetActive(true);
        }
        SkillMaxUpgraded maxUpgrade = GetComponent<SkillMaxUpgraded>();
        switch (level.id)
        {
            case 0:
                GetComponent<PlayerSetting>().IncreaseMoveSpeed(level.ratio);
                if (level.level == 5) { maxUpgrade.isSkillDashUnlock = true; }
                
                break;
            case 1:
                GetComponent<PlayerSetting>().IncreaseDamage(level.ratio);
                break;
            case 2:
                GetComponent<PlayerSetting>().DecreaseTimeDelayAttack(level.ratio);
                if (level.level == 5) { maxUpgrade.MaxLevelCoolDownNormal(); }
                break;
            case 3:
                GetComponent<PlayerSetting>().DecreaseTimeDelayAttackUltimate(level.ratio);
                break;  
            case 4:
                GetComponent<PlayerSetting>().IncreaseRangeAttack(level.ratio);
                break;
            case 5:
                GetComponent<PlayerSetting>().IncreaseMaxHealth(level.ratio);
                if (level.level == 5) 
                { 
                    maxUpgrade.isSkillMaxHealthUnlock = true;
                    maxUpgrade.MaxLevelMaxHealth(level);
                }
                break;
            case 6:
                GetComponent<PlayerSetting>().IncreaseExplosion(level.ratio);
                break;
        }
        FindObjectOfType<UIListMaxSkillUpgraded>().SetSkill();
        SetInformationSkillUpgraded();
        FindObjectOfType<UIDisplayPlayerProperty>().SetListValuePlayerDetail(player.physicDamage, player.moveSpeed, player.maxHealth, player.attackRange, player.explosionRange, player.attackDelay, player.attackDelayUltimate);
        FindObjectOfType<UIManagement>().CloseLevelUp();
        FindObjectOfType<UIPauseGame>().ContinueGame();
        SetItemSkillHiden();
    }
    public void SetItemSkillHiden()
    {
        foreach(GameObject i in itemSkill)
        {
            i.gameObject.SetActive(false);
        }
    }
    public void SetInformationSkillUpgraded()
    {
        for(int j=0; j<skillUpgraded.Count; j++)
        {
            listViewSkillsUpgraded.transform.GetChild(j).GetChild(0).GetComponent<UISkillUpgraded>().SetSkillUpgradedInfor(skillUpgraded[j]);
            listViewSkillsUpgraded.transform.GetChild(j).GetComponent<UISkillUpgradeSetContect>().SetText(skillUpgraded[j]);
        }
        
    }
}
