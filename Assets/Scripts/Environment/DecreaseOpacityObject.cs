using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseOpacityObject : MonoBehaviour
{
    Color32 initialColor;
    private void Awake()
    {
        initialColor = GetComponentInParent<SpriteRenderer>().color;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("EBullet")) return;
        GetComponentInParent<SpriteRenderer>().color = new Color32(initialColor.r, initialColor.g, initialColor.b, 150);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("EBullet")) return;
        GetComponentInParent<SpriteRenderer>().color = initialColor;
    }
}
