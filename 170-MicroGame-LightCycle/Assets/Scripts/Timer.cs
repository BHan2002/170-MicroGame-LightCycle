using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private WinScreenManager winScreenManager;
    [SerializeField] private GameManager gameManager;
    private float timeLeft;

    private bool hasWon = false;

    void Start()
    {
        timeLeft = 30f; // Set the timer to start at 30 seconds
    }

    void Update()
    {
        if (hasWon)
            return;

        timeLeft -= Time.deltaTime;
        
        if (timeLeft <= 1.35f)
        {
            gameManager.WinGame();
            hasWon = true;
        }
        timerText.text = FormatTime(timeLeft);
    }

    string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return string.Format("{0:00}:{1:000}", seconds, milliseconds);
    }
}