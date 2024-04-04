
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
        if (!PlayerPrefs.HasKey("Achievement")) SaveAchievement();
        achievements = JsonUtility.FromJson<AchievementStore>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("Achievement")).achievements;

        
    }
    public void Init()
    {
        for(int i=0; i<achievements.Length; i++)
        {
            
            GameObject achievement = Instantiate(achievementPrefab, listView);
            achievement.GetComponent<AchievementButton>().achievement = achievements[i];
            if (achievements[i].isAchieved)
            {
                achievement.transform.GetChild(0).GetComponent<Image>().sprite = achievements[i].sprite;
            }
        }
    }
    private void Update()
    {
        foreach(Achievement achiev in achievements)
        {
            switch (achiev.type)
            {
                case Achievement.Type.killed:
                    achiev.SetAchievement(GameManager.instance.killed);
                    break;
                case Achievement.Type.survival:
                    break;
            }
        }
    }
    public void SaveAchievement()
    {
        AchievementStore store = new AchievementStore();
        store.achievements = achievements;
        FindObjectOfType<JsonUtilityReadWrite>().SaveToJsonUtility(store, "Achievement");
    }
    
}

