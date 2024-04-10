
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public enum Type { killed, survival }
    public Type type;
    public string achievementName;
    public string spriteName;
    public bool isAchieved;
    public int goal;
}