using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        if (hours > 0)
        {
            timeText.SetText($"{hours} Hrs {minutes} Mins {seconds} Secs");
        }
        else if (minutes > 0)
        {
            timeText.SetText($"{minutes} Mins {seconds} Secs");
        }
        else
        {
            timeText.SetText($"{seconds} Secs");
        }

        #endregion

        #region BestTime

        bestTime = currentTime;

        int bestTimeSeconds = Mathf.FloorToInt(bestTime % 60);
        int bestTimeMinutes = Mathf.FloorToInt(bestTime / 60) % 60;
        int bestTimeHours = Mathf.FloorToInt(bestTime / 3600);

        if (bestTimeHours > 0)
        {
            bestTimeText.SetText($"{bestTimeHours} Hrs {bestTimeMinutes} Mins {bestTimeSeconds} Secs");
        }
        else if (bestTimeMinutes > 0)
        {
            bestTimeText.SetText($"{bestTimeMinutes} Mins {bestTimeSeconds} Secs");
        }
        else
        {
            bestTimeText.SetText($"{bestTimeSeconds} Secs");
        }

        #endregion

        if(currentTime > PlayerPrefs.GetFloat("BestTime", 0))
        {          
            PlayerPrefs.SetFloat("BestTime", bestTime);
            bestTimeText.SetText(bestTime.ToString());
        }
    }
}
