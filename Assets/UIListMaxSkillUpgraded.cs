using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIListMaxSkillUpgraded : MonoBehaviour
{
    public GameObject[] listSkillObject;
    List<LevelUpSkillObject> listSkillUpgrade;
    private void Start()
    {
        listSkillUpgrade = FindObjectOfType<LevelUpPlayer>().skillUpgraded;
    }
    public void SetSkill()
    {
        for(int i=0; i<listSkillUpgrade.Count; i++)
        {
            if (listSkillUpgrade[i].level == 5)
            {
                listSkillObject[i].gameObject.SetActive(true);
                listSkillObject[i].transform.GetChild(0).GetComponent<Image>().sprite = listSkillUpgrade[i].sprite;
            }
        }
    }
}
