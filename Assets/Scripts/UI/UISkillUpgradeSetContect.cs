using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UISkillUpgradeSetContect : MonoBehaviour
{
    public void SetText(LevelUpSkillObject item)
    {
        transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = item.sprite;
        transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.nameSkill;
    }
}
