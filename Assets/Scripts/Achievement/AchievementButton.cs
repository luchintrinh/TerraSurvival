using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementButton : MonoBehaviour
{
    public Achievement achievement;
    [SerializeField] Sprite defaultSprite;
    public void HoverButtonAchievement()
    {
        if (achievement.isAchieved)
        {
            FindObjectOfType<UIAchivement>().SetTextAchievement(FindObjectOfType<SpritesManagement>().sprites.listAchievement[achievement.spriteIndex], achievement.achievementName);
        }
        else
        {
            FindObjectOfType<UIAchivement>().SetTextAchievement(defaultSprite, achievement.achievementName);
        }
    }
}
