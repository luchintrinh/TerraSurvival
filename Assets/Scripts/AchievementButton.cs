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
            FindObjectOfType<UIAchivement>().SetTextAchievement(achievement.sprite, achievement.name);
        }
        else
        {
            FindObjectOfType<UIAchivement>().SetTextAchievement(defaultSprite, achievement.name);
        }
    }
}
