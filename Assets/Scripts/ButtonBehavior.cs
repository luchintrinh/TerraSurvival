using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IEventSystemHandler
{
    private void Start()
    {
        //Init();
        GetComponent<Button>().onClick.AddListener(ButtonOnclick);
    }
    public void Init()
    {
        if (GetComponent<EventTrigger>() == null)
        {
            gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => OnPointerClick());
        trigger.triggers.Add(entry);
    }

    public void ButtonOnclick()
    {
        GetComponent<Image>().color = Color.white;
    }

    public void OnPointerClick()
    {
        GetComponent<Image>().color = Color.white;
    }
}
