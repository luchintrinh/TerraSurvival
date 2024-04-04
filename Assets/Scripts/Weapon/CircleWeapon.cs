using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] int number;
    [SerializeField] float speed=5f;
    [SerializeField] float rangeWeapon=2f;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        for(int i=0; i<number; i++)
        {
            float angle = 360 / number * i;
            GameObject weapon = Instantiate(weaponPrefab, gameObject.transform);
            weapon.transform.parent = gameObject.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            weapon.transform.Translate(Vector3.up*rangeWeapon, Space.Self);
        }
    }
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.back * speed*Time.deltaTime);
    }
}
