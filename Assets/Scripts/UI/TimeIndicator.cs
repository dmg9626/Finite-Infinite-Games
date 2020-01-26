using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeIndicator : MonoBehaviour
{
    private GameManager gameManager;

    private TextMeshProUGUI timerText;

    void Start()
    {
        if(!TryGetComponent(out timerText)) {
            Debug.LogError(name + "| Missing TextMeshProUGUI component!");
        }
        
        // Hide timer when time runs out
        gameManager = GameManager.Instance;
        if(gameManager.finite) {
            gameManager.OnTimeExpired += () => { HideTimer(); };
        }
        else {
            HideTimer();
        }
    }

    void Update()
    {
        if(!gameManager.timeExpired) {
            SetTime(gameManager.timeRemaining);
        }
    }

    void SetTime(float time)
    {
        // Convert time to minutes : seconds format
        int totalSeconds = (int)time;
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        string timeStr = minutes + ":" + seconds;

        // Update UI
        timerText.text = "Time: " + timeStr;
    }

    void HideTimer()
    {
        timerText.enabled = false;
    }
}
