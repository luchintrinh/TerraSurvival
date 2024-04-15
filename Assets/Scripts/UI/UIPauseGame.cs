using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseGame : MonoBehaviour
{

    public GameObject settingGameObject;
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameManager.instance.isPause = true;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        GameManager.instance.isPause = false;
    }
    public void ResumeGame()
    {
        GameManager.instance.isPause = false;
        Time.timeScale = 1;
        GetComponent<UIManagement>().pauseGame.SetActive(false);
    }
    public void SettingGame()
    {
        settingGameObject.SetActive(true);
    }

    public void LeaveSetting()
    {
        settingGameObject.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
        GameManager.instance.isPause = false;
        Time.timeScale = 1;
    }
}
