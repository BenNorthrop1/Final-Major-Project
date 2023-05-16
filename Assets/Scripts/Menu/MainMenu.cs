using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuObject;
    public GameObject settingsObject;
    public GameObject creditsObject;
    public GameObject infoScreen;
    public GameObject loadingScreen;

    public Slider loadingSlider;

    private void Start() {
        infoScreen.SetActive(false);
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
    }

    public void Credits()
    {
        infoScreen.SetActive(true);
        creditsObject.SetActive(true);
        settingsObject.SetActive(false);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
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
