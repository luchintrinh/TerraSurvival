using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class UIWarmUpScene : MonoBehaviour
{
    public GameObject loadScene;
    public GameObject bar;
    public float time;

    private void Start()
    {
        AnimateBar();
    }
    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time).setOnComplete(()=> {
            loadScene.gameObject.SetActive(false);
        });
        
    }

}
