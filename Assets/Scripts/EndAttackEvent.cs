using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndAttackAnition()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }
}
