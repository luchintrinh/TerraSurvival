using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISkillUpgraded : MonoBehaviour
{
    public Sprite colorStar;
    public Sprite noneColorStar;
    public void SetSkillUpgradedInfor(LevelUpSkillObject item)
    {
        for(int i=0; i<5; i++)
        {
            if (i < item.level)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = colorStar;
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().sprite = noneColorStar;
            }
        }
        
    }
}
