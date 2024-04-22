using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMaxUpgraded : MonoBehaviour
{

    [Header("# Max Speed")]
    public bool isSkillDashUnlock = false;
    public bool isReadyDash = true;
    public float nextTime;
    public float duration=0.5f;
    public float cooldownTime = 2f;
    public float skilldurationEnd;
    public float baseSpeed;
    public float dashBonusSpeed=10f;

    [Header("# Max Health")]
    public bool isSkillMaxHealthUnlock=false;

    [Header("# Max Cooldown Normal")]
    public bool isSkillMaxCooldownNormal = false;


    PlayerSetting player;
    private void Start()
    {
        player = GetComponent<PlayerSetting>();
        
    }
    public void MaxLevelMoveSpeed()
    {
        if (!isReadyDash) return;
        if (baseSpeed == 0)
        {
            baseSpeed = player.moveSpeed;
        }
        transform.Find("Trail").gameObject.SetActive(true);
        player.moveSpeed += dashBonusSpeed;
        FindObjectOfType<SoundManager>().playSFX(SoundManager.SFXType.dash);
        isReadyDash = false;
        skilldurationEnd = Time.time + duration;
        nextTime = Time.time + duration + cooldownTime;
    }
    public void MaxLevelAttackRange()
    {
        
    }
    public void MaxLevelAttackDamage()
    {

    }
    public void MaxLevelMaxHealth(LevelUpSkillObject skill)
    {
        GameObject rootCircleWeapon = new GameObject("rootCircleWeapon");
        rootCircleWeapon.transform.parent = gameObject.transform;
        rootCircleWeapon.transform.localPosition = Vector3.zero;
        CircleWeapon circle= rootCircleWeapon.AddComponent<CircleWeapon>();
        circle.weaponPrefab = skill.weaponMaxHeath;
    }
    public void MaxLevelCoolDownNormal() 
    {
        isSkillMaxCooldownNormal = true;
        transform.Find("Weapon").GetComponent<Weapon>().NumberAttack = 5;
    }
    public void MaxLevelCoolDownUltimate()
    {

    }

    public void MaxLevelExplosionRange()
    {

    }
    private void Update()
    {
        if (!isReadyDash)
        {
            if (Time.time >= skilldurationEnd)
            {
                EndDash();
            }
            if (Time.time >= nextTime)
            {
                isReadyDash = true;
            }
        }
    }
    public void EndDash()
    {
        player.moveSpeed=baseSpeed;
        transform.Find("Trail").gameObject.SetActive(false);
    }
}

