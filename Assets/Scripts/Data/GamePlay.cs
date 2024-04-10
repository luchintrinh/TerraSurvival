
[System.Serializable]
public class GamePlay
{
    public int coinNumber;
    public int killNumber;
    public float timerPlay;
    public GamePlay(int coinNumber, int killNumber, float timerPlay)
    {
        this.coinNumber = coinNumber;
        this.killNumber = killNumber;
        this.timerPlay = timerPlay;
    }
}
