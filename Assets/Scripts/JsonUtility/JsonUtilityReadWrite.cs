
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
            string json = File.ReadAllText(jsonFilePath);
            GameInfor gameInfor = JsonUtility.FromJson<GameInfor>(json);
            if (gameInfor == null) return;
            sound.muteMusic = gameInfor.isMuteMusic;
            sound.muteSFX = gameInfor.isMuteSFX;
            sound.volume = gameInfor.volumn;
        }
    }
    public string GetJsonUtility(string nameSaved)
    {
        string json = PlayerPrefs.GetString(nameSaved);
        return json;
    }
    public void SaveToJsonUtility(GameItem game,string nameSaved)
    {
        string json = JsonUtility.ToJson(game, true);
        PlayerPrefs.SetString(nameSaved, json);
        PlayerPrefs.Save();
    }
    public void SaveToJsonUtility(AchievementStore game, string nameSaved)
    {
        string json = JsonUtility.ToJson(game, true);
        PlayerPrefs.SetString(nameSaved, json);
        PlayerPrefs.Save();
    }
}
