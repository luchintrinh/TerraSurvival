using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManagement : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI enemyDeadNumber;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject lossCanvas;
    [SerializeField] GameObject UILevelUp;
    [SerializeField] GameObject WarningPopup;
    [SerializeField] GameObject UIInGame;

    public GameObject propertyPopup;

    [SerializeField] Slider healthSlider;

    public float timer;
    SpawnEnemy spawn;

    // UI system

    public GameObject pauseGame;

    private void Start()
    {
        timer = 0;
        
    }
    public void SetHealth(int maxHealth, int health)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!spawn) spawn = FindObjectOfType<SpawnEnemy>();
        GameInformation();

        
    }
    public void GameInformation()
    {
        int wave = spawn.wave + 1;
        waveNumberText.text ="Wave "+wave.ToString();
        //if (spawn.wave > 2) spawn.wave = 2;
        enemyDeadNumber.text = spawn.enemyDead.ToString()+"/"+spawn.enemiesOfWave[spawn.wave];
        timeText.text = Mathf.Floor(timer).ToString() + "s";
    }

    public void BackToHome()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void LevelUp()
    {
        Time.timeScale = 0;
        UILevelUp.gameObject.SetActive(true);
        propertyPopup.SetActive(true);
    }

    public void SetInformationWarning(string text, Color color)
    {
        StartCoroutine(PopupCoroutine(text, color));
    }

    IEnumerator PopupCoroutine(string text, Color color)
    {
        WarningPopup.gameObject.SetActive(true);
        WarningPopup.transform.DOShakeRotation(1, 30f);
        WarningPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        WarningPopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
        yield return new WaitForSeconds(2f);
        WarningPopup.gameObject.SetActive(false);
    }

    public void CloseLevelUp()
    {
        Time.timeScale = 1;
        UILevelUp.gameObject.SetActive(false);
        propertyPopup.SetActive(false);
    }

    public void WinCanvas()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
        UIInGame.SetActive(false);
        winCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().level}";
        winCanvas.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Killed: {FindObjectOfType<SpawnEnemy>().enemyDead}";
        winCanvas.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.CoinPickup.ToString();
        SaveGamePlay();
    }

    private void SaveGamePlay()
    {
        GamePlay play = JsonUtility.FromJson<GamePlay>(FindObjectOfType<JsonUtilityReadWrite>().GetJsonUtility("GamePlay"));
        play.coinNumber += GameManager.instance.CoinPickup;
        play.killNumber += GameManager.instance.killed;
        play.timerPlay += timer;
        FindObjectOfType<JsonUtilityReadWrite>().SaveToJsonUtility(play, "GamePlay");
        FindObjectOfType<AchievementManagement>().CheckAchievement(play.killNumber);
    }

    public void LossCanvas()
    {
        Time.timeScale = 0;
        lossCanvas.SetActive(true);
        UIInGame.SetActive(false);
        lossCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().level}";
        lossCanvas.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Killed: {FindObjectOfType<SpawnEnemy>().enemyDead}";
        lossCanvas.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.CoinPickup.ToString();
        SaveGamePlay();
    }

}
