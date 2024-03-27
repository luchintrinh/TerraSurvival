using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagement : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI enemyDeadNumber;

    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject lossCanvas;
    [SerializeField] GameObject UILevelUp;

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
        if (spawn.wave > 2) spawn.wave = 2;
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
    }

    public void CloseLevelUp()
    {
        Time.timeScale = 1;
        UILevelUp.gameObject.SetActive(false);
    }

    public void WinCanvas()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
        winCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().level}";
        winCanvas.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Killed: {FindObjectOfType<SpawnEnemy>().enemyDead}";
    }

    public void LossCanvas()
    {
        Time.timeScale = 0;
        lossCanvas.SetActive(true);
        lossCanvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {GameManager.instance.playerSpawners[0].GetComponent<PlayerSetting>().level}";
        lossCanvas.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Killed: {FindObjectOfType<SpawnEnemy>().enemyDead}";
    }

}
