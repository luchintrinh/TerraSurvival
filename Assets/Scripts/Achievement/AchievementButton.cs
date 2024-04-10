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
            FindObjectOfType<UIAchivement>().SetTextAchievement(Resources.Load<Sprite>($"Achievements/{achievement.spriteName}"), achievement.achievementName);
        }
        else
        {
            FindObjectOfType<UIAchivement>().SetTextAchievement(defaultSprite, achievement.achievementName);
        }
    }
}
