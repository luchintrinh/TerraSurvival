using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUIManager : MonoBehaviour
{
    public RectTransform prefab; // Prefab UI để pooling

    private List<RectTransform> pool = new List<RectTransform>();

    private void Awake()
    {
        GameManager.instance.poolUI = this;
    }
    void Start()
    {
        pool = new List<RectTransform>();
    }

    public RectTransform GetObjectFromPool(Transform parent)
    {
        foreach (RectTransform rect in pool)
        {
            if (!rect.gameObject.activeSelf)
            {
                rect.gameObject.SetActive(true);
                return rect;
            }
        }
        RectTransform newObj = Instantiate(prefab, parent);
        pool.Add(newObj);
        return newObj;
    }
}
