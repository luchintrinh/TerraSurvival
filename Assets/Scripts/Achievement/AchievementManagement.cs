
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManagement : MonoBehaviour
{
    [SerializeField] GameObject achievementPrefab;
    public Transform listView;

    public Achievement[] achievements;


    private void Start()
    {
        PlayerPrefs.DeleteKey("Achievement");
        AchievementStore achievement = new AchievementStore();
        if (!PlayerPrefs.HasKey("Achievement"))
        {
            achievement.achievements = achievements;
            GetComponent<JsonUtilityReadWrite>().SaveToJsonUtility(achievement, "Achievement");
        }
        achievement.achievements = JsonUtility.FromJson<AchievementStore>(GetComponent<JsonUtilityReadWrite>().GetJsonUtility("Achievement")).achievements;
        achievements = achievement.achievements;
    }
    public void Init()
    {
        for(int i=0; i<achievements.Length; i++)
        {
            
            GameObject achievement = Instantiate(achievementPrefab, listView);
            achievement.GetComponent<AchievementButton>().achievement = achievements[i];
            if (achievements[i].isAchieved)
            {
                achievement.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>($"Achievements/{achievements[i].spriteName}");
            }
        }
    }

    public void CheckAchievement(int goal)
    {
        foreach (Achievement achiev in achievements)
        {
            switch (achiev.type)
            {
                case Achievement.Type.killed:
                    SetAchievement(goal, achiev);
                    break;
                case Achievement.Type.survival:
                    break;
            }
        }
    }

    public void SetAchievement(int goalSuccess, Achievement achiev)
    {
        if (achiev.isAchieved) return;
        if (achiev.goal <= goalSuccess)
        {
            Debug.Log(achiev.achievementName + " Success");
            achiev.isAchieved = true;
            AchievementStore achievement = new AchievementStore();
            achievement.achievements = achievements;
            GetComponent<JsonUtilityReadWrite>().SaveToJsonUtility(achievement, "Achievement");
        }
        
    }

}

