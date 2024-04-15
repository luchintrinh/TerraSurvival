using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new SpriteManager", menuName ="new SpriteManager")]
public class SpritesManager :ScriptableObject
{
    public Sprite[] listSprite;
    public Animator[] listAnimator;
}
