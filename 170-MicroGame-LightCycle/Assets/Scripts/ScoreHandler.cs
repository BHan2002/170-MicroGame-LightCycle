using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int currentScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }
    public int GetScore()
    {
        return currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
