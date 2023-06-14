using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Object References")]
    public GameObject MenuObject;
    public GameObject settingsObject;
    public GameObject creditsObject;
    public GameObject controlsObject;
    public GameObject infoScreen;
    public GameObject loadingScreen;

    [Header("Loading Reference")]
    public Slider loadingSlider;

    [Header("Text References")]
    [SerializeField] private TMP_Text bestScoreText;

    private float bestTime;

    private void Start() 
    {
        infoScreen.SetActive(false);

        bestTime = PlayerPrefs.GetFloat("BestTime", 0);

        int bestTimeSeconds = Mathf.FloorToInt(bestTime % 60);
        int bestTimeMinutes = Mathf.FloorToInt(bestTime / 60) % 60;
        int bestTimeHours = Mathf.FloorToInt(bestTime / 3600);

        bestScoreText.SetText(string.Format("{0:0}:{1:00}:{2:00}", bestTimeHours, bestTimeMinutes, bestTimeSeconds));
    }

    public void Play(string LevelName)
    {
        MenuObject.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(LevelName));
    }

    public void Settings()
    {
        infoScreen.SetActive(true);
        settingsObject.SetActive(true);
        creditsObject.SetActive(false);
        controlsObject.SetActive(false);
    }

    public void Credits()
    {
        infoScreen.SetActive(true);
        creditsObject.SetActive(true);
        settingsObject.SetActive(false);
        controlsObject.SetActive(false);
    }
    public void Controls()
    {
        infoScreen.SetActive(true);
        creditsObject.SetActive(false);
        settingsObject.SetActive(false);
        controlsObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadLevelASync(string LevelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(LevelName);

        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
