using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAchievement : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AchievementManagement>().Init();
    }
}
