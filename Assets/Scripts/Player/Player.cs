



[System.Serializable]
public class Player 
{
    
    //[Header("# Property")]
    public string namePlayer;
    public string description;
    public float baseSpeed;
    public int baseMaxHealth;
    public int baseHealth;
    public int basePhysicDamage;
    public int baseMagicalDamage;
    //[Header("# Player Animator")]
    public string playerAniStr;

    //[Header("# Sprites")]

    public string playerSpriteName;

    //[Header("# Lock")]
    public bool isLock;
    public int price;
}
