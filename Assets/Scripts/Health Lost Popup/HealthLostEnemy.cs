
using UnityEngine;
using DG.Tweening;

public class HealthLostEnemy : MonoBehaviour
{
    public float targetY = 1f;
    public float moveDuration = 1.5f;
    public Vector3 initialPos;
    public Vector3 targetScale;
    private Transform pos;
    public Color32 color;
    private void Awake()
    {
        color= new Color32(211, 33, 0, 255);
        pos = gameObject.transform;
        targetScale = pos.localScale * 1.5f;
    }
    public void SetText(int health)
    {
        GetComponent<TextMesh>().text = "-" + health.ToString();
    }
    public void ResetPopUp()
    {
        pos.localScale = Vector3.one;
        GetComponent<TextMesh>().color = color;
    }
    private void OnEnable()
    {
        ResetPopUp();
    }
    public void PopUpMovement()
    {
        pos.DOMoveY(initialPos.y + targetY, moveDuration)
            .SetEase(Ease.OutQuad)
            .OnUpdate(() =>
            {
                pos.localScale = Vector3.Lerp(pos.localScale, targetScale, Time.deltaTime);
            })
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

}
