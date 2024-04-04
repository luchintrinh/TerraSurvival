
using System;
using UnityEngine;


[CreateAssetMenu(fileName ="new Achievement", menuName ="new Achievemnt")]
public class Achievement: ScriptableObject
{
    public enum Type { killed, survival }
    public Type type;
    public string achievementName;
    public Sprite sprite;
    public bool isAchieved;
    public int goal;

    public void SetAchievement(int goalSuccess)
    {
        if (isAchieved) return;
        if (goal <= goalSuccess)
        {
            Debug.Log(achievementName + " Success");
            isAchieved = true;
            FindObjectOfType<AchievementManagement>().SaveAchievement();
        }
    }
}