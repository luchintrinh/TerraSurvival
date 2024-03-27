using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;
    private void Awake()
    {
        GameManager.instance.pool = this;
        pools = new List<GameObject>[prefabs.Length];
        for(int i=0; i<pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    private void Start()
    {
        GameManager.instance.isPause = false;
    }
    public GameObject Get(int index)
    {
        GameObject item = null;
        foreach(GameObject i in pools[index]){
            if (!i.activeSelf)
            {
                item = i;
                i.SetActive(true);
                break;
            }
        }
        if (!item)
        {
            item = Instantiate(prefabs[index], transform);
            pools[index].Add(item);
        }
        return item;
    }
}
