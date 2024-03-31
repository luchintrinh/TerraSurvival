using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttackEffect : MonoBehaviour
{
    public void DamagePlayer()
    {
        transform.parent.GetComponentInChildren<EnemyCircleIndicatorAttack>().isTookDamage=true;
    }

    public void EndDamagePlayer()
    {
        transform.parent.GetComponentInChildren<EnemyCircleIndicatorAttack>().isTookDamage = false;
    }
    public void Reset()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    private void OnEnable()
    {
        Reset();
        GetComponent<Animator>().SetTrigger("Hit");
    }

}
