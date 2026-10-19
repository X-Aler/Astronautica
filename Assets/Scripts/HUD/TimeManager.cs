using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager
{
    private float cycleDuration;

    private string cycleName;

    private TextMeshProUGUI timerText;

    public event Action OnCycleEnded;

    private CurrentTime isDay;

    public TimeManager(float _cycleDuration, TextMeshProUGUI _timerText, CurrentTime _isDay)
    {
        cycleDuration = _cycleDuration;
        timerText = _timerText;
        isDay = _isDay;
    }

    private string GetName()
    {
        if (isDay.Value)
        {
            cycleName = "Day";
        }
        else
        {
            cycleName = "Night";
        }
        return cycleName;
    }

    public IEnumerator RunTimer()
    {
        float timeLeft = cycleDuration;
        while (timeLeft > 0)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);

            timerText.text = $"{GetName()}: {minutes}:{seconds}";

            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        OnCycleEnded?.Invoke();
    }    

}
