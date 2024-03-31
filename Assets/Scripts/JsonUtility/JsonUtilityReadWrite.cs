
using System.IO;
using UnityEngine;

public class JsonUtilityReadWrite : MonoBehaviour
{
    SoundManager sound;
    private void Awake()
    {
        sound = FindObjectOfType<SoundManager>();
    }
    public void SaveToJson()
    {
        GameInfor game = new GameInfor();
        game.isMuteMusic= sound.muteMusic;
        game.isMuteSFX= sound.muteSFX;
        game.volumn = sound.volume;
        string json = JsonUtility.ToJson(game,true);
        File.WriteAllText(Application.dataPath + "/Data/GameInfor.json", json);
    }
    public void LoadJson()
    {
        string jsonFilePath = Application.dataPath + "/Data/GameInfor.json";
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(Application.dataPath + "/Data/GameInfor.json");
            GameInfor gameInfor = JsonUtility.FromJson<GameInfor>(json);
            if (gameInfor == null) return;
            sound.muteMusic = gameInfor.isMuteMusic;
            sound.muteSFX = gameInfor.isMuteSFX;
            sound.volume = gameInfor.volumn;
        }
    }
}
