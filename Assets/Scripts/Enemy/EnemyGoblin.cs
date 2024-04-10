using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblin : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] int number;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        GameObject rootWeapon = new GameObject("RootWeapon");
        rootWeapon.transform.parent = gameObject.transform;
        for(int i=0; i<number; i++)
        {

            GameObject weapon = Instantiate(weaponPrefab, rootWeapon.transform);
            weapon.transform.localPosition = rootWeapon.transform.position;
        }
    }
}
