using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public enum SceneName {MainMenu,Game }
    public SceneName scene_Name;
    private const string HighScoresKey = "HighScores";
    public List<int> highScores = new List<int>();

    void Start()
    {
        if (scene_Name == SceneName.MainMenu)
        {
            LoadHighScores();
        }
    }

    public void AddScore(int score)
    {
        print(score);
        highScores.Add(score);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort descending
        if (highScores.Count > 5) // Keep top 5 scores
        {
            highScores.RemoveAt(highScores.Count - 1);
        }
        SaveHighScores();
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new ScoreList(highScores));
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            string json = PlayerPrefs.GetString(HighScoresKey);
            highScores = JsonUtility.FromJson<ScoreList>(json)?.scores ?? new List<int>();
        }
    }

    public List<int> GetHighScores()
    {
        return new List<int>(highScores);
    }
}

[System.Serializable]
public class ScoreList
{
    public List<int> scores;
    public ScoreList(List<int> scores) { this.scores = scores; }
}
