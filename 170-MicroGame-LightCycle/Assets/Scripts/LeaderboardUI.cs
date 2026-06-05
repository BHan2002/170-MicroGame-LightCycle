using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardManager leaderboardManager;
    [SerializeField] private TextMeshProUGUI leaderboardText;

    void OnEnable()
    {
        Refresh();
    }
    public void Refresh()
    {
        leaderboardText.text = "LEADERBOARD\n\n";

        var entries = leaderboardManager.Data.entries;

        for (int i = 0; i < 5; i++)
        {
            if (i < entries.Count)
                leaderboardText.text += $"{i + 1}. {entries[i].name} - {entries[i].score}\n";
            else
                leaderboardText.text += $"{i + 1}. --- - 0\n";
        }
    }
}