using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NameEntry : MonoBehaviour
{
    [SerializeField] private TMP_InputField initialsInput;
    [SerializeField] private LeaderboardManager leaderboardManager;
    [SerializeField] private Button submitButton;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private GameObject nameEntryScreen;

    [SerializeField] private GameObject leaderboardScreen;

    [SerializeField] private LeaderboardUI leaderboardUI;

    public void SubmitScore()
    {
        StartCoroutine(SubmitScoreRoutine());
    }

    private IEnumerator SubmitScoreRoutine()
    {
        string initials = initialsInput.text.ToUpper();

        if (string.IsNullOrWhiteSpace(initials))
        {
            initials = "AAA";
        }

        int finalScore = scoreHandler.GetScore();

        leaderboardManager.AddEntry(initials, finalScore);

        nameEntryScreen.SetActive(false);
        leaderboardScreen.SetActive(true);

        yield return null;

        leaderboardUI.Refresh();
    }
}
