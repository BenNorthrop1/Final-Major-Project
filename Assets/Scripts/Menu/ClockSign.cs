using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClockSign : MonoBehaviour
{
    [Header("Text Reference")]
    [SerializeField] private TMP_Text timeText;

    [Space(10)]
    [SerializeField] private TMP_Text bestTimeText;

    private float currentTime;
    private float bestTime;

    private void Start() 
    {
        bestTimeText.SetText(PlayerPrefs.GetFloat("BestTime", 0).ToString());
    }

    private void Update() 
    {

        #region CurrentTime

        currentTime += Time.deltaTime;

        int seconds = Mathf.FloorToInt(currentTime % 60);
        int minutes = Mathf.FloorToInt(currentTime / 60) % 60;
        int hours = Mathf.FloorToInt(currentTime / 3600);

        timeText.SetText(string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds));

        #endregion

        #region BestTime

        bestTime = currentTime;

        int bestTimeSeconds = Mathf.FloorToInt(bestTime % 60);
        int bestTimeMinutes = Mathf.FloorToInt(bestTime / 60) % 60;
        int bestTimeHours = Mathf.FloorToInt(bestTime / 3600);

        bestTimeText.SetText(string.Format("{0:0}:{1:00}:{2:00}", bestTimeHours, bestTimeMinutes, bestTimeSeconds));

        #endregion

        if(currentTime > PlayerPrefs.GetFloat("BestTime", 0))
        {          
            PlayerPrefs.SetFloat("BestTime", bestTime);
            bestTimeText.SetText(bestTime.ToString());
        }
    }
}
