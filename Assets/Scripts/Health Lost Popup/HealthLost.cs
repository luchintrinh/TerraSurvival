using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HealthLost : MonoBehaviour
{
    public float targetY = 50f;
    public float moveDuration = 1.5f;
    public float fadeDuration = 0.5f;
    public Vector2 initialPos;
    public Vector3 targetScale;
    private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        initialPos = rect.position;
        targetScale = rect.localScale * 1.5f;
    }
    public void SetText(string str,int health)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = str + health.ToString();
    }
    public void ResetPopUp()
    {
        rect.localScale = Vector3.one;
        GetComponentInChildren<TextMeshProUGUI>().DOFade(255, 0);
    }
    private void OnEnable()
    {
        ResetPopUp();
        rect.DOAnchorPos(rect.position+new Vector3(0, targetY, 0), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() =>
            {
                rect.localScale = Vector3.Lerp(rect.localScale, targetScale, Time.deltaTime);
            })
            .OnComplete(() =>
            {
                FadeOutText();
            });
    }
    private void FadeOutText()
    {
        GetComponentInChildren<TextMeshProUGUI>().DOFade(0f, fadeDuration)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
    private void OnDisable()
    {
       rect.localPosition = initialPos;
    }
}
