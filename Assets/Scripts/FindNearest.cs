using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearest : MonoBehaviour
{
    [SerializeField] float radius;

    RaycastHit2D[] enemys;
    [SerializeField] LayerMask targetLayer;

    private void Start()
    {
        //radius = GetComponent<PlayerSetting>().weapon.attackRange;
    }
    public Transform Find()
    {
        Transform result=null;
        float diff = 100f;
        Debug.DrawRay(transform.position, Vector2.right * radius, Color.green);
        enemys = Physics2D.CircleCastAll(transform.position, GetComponent<PlayerSetting>().weapon.attackRange, Vector2.zero, 0, targetLayer);
        foreach(RaycastHit2D enemy in enemys)
        {
            if (!enemy.transform.gameObject.GetComponent<HealthManager>().isLive) continue;
            Vector3 myPos = transform.position;
            Vector3 enemyPos = enemy.transform.position;
            float curDiff = Vector3.Distance(myPos, enemyPos);
            if (diff > curDiff)
            {
                diff = curDiff;
                result = enemy.transform;
            }
        }
        return result;
    }

    
    public Transform FindPlayer()
    {
        Transform result = null;
        float diff = 100f;
        foreach(GameObject player in GameManager.instance.playerSpawners)
        {
            if (!player.GetComponent<HealthManager>().isLive) continue;
            Vector3 myPos = transform.position;
            Vector3 playerPos = player.transform.position;
            float curDiff = Vector3.Distance(myPos, playerPos);
            if (diff > curDiff)
            {
                diff = curDiff;
                result = player.transform;
            }
        }
        return result;
    }
    

}
