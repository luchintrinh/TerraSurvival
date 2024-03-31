using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="LevelUp skill", menuName ="new LevelUp Skill")]
public class LevelUpSkillObject : ScriptableObject
{
    public int id;
    public string nameSkill;
    public string description;
    public float ratio;
    public Sprite sprite;
}
