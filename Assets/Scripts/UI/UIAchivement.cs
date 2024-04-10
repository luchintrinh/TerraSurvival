using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIAchivement : MonoBehaviour
{
    [SerializeField] Image spriteImage;
    [SerializeField] TextMeshProUGUI descText;
    private void Awake()
    {
        FindObjectOfType<AchievementManagement>().listView = gameObject.transform;
    }

    public void SetTextAchievement(Sprite sprite, string text)
    {
        spriteImage.GetComponent<Image>().sprite = sprite;
        descText.text = text;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
