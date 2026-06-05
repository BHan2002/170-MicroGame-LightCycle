using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string name;
    public int score;
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

public class LeaderboardManager : MonoBehaviour
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "leaderboard.json");

    public LeaderboardData Data { get; private set; } = new LeaderboardData();

    void Awake()
    {
        Load();
    }

    public bool Qualifies(int score)
    {
        if (Data.entries.Count < 5) return true;
        return score > Data.entries[Data.entries.Count - 1].score;
    }

    public void AddEntry(string playerName, int score)
    {
        Data.entries.Add(new LeaderboardEntry
        {
            name = playerName.ToUpper(),
            score = score
        });

        Data.entries.Sort((a, b) => b.score.CompareTo(a.score));

        if (Data.entries.Count > 5)
            Data.entries.RemoveAt(Data.entries.Count - 1);

        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        Debug.Log("Path: " + SavePath);
        File.WriteAllText(SavePath, json);
    }

    public void Load()
    {
        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        Debug.Log("Loaded JSON:");
        Debug.Log(json);
        Data = JsonUtility.FromJson<LeaderboardData>(json);
    }
}