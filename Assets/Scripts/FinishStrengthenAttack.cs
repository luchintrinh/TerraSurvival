using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStrengthenAttack : MonoBehaviour
{
    InputSystemManagement input;
    private void Start()
    {
        input = transform.parent.GetComponent<InputSystemManagement>();
    }
    public void EndStrengthenAttack()
    {
        input.isStrengthen = false;
        transform.parent.GetChild(4).GetComponent<SpriteRenderer>().sprite = null;
    }
}
