using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    Image img;
    TextMeshProUGUI text;
    Color white = new Color(255, 255, 255, 255);
    Color black= new Color(51, 51, 51, 255);
    private void Start()
    {
        img = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void HoverButton()
    {
        img.color = Color.white;
    }
    public void UnHoverButton()
    {
        img.color =Color.black;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
