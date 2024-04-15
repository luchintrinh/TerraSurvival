using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncManagement : MonoBehaviour
{
    [Header("# Menu Screen")]
    [SerializeField] private GameObject loadScreen;

    [Header("# Slider")]
    [SerializeField] private Image loadProgress;
    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }
    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        loadScreen.gameObject.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadProgress.fillAmount = progressValue;
            yield return null;
        }
        loadScreen.gameObject.SetActive(false);
    }

}
