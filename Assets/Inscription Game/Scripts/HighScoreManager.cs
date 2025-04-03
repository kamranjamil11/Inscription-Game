using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public enum SceneName {MainMenu,Game }
    public SceneName scene_Name;
    public Text coins_Text;
    private const string HighScoresKey = "HighScores";
    public List<int> highScores = new List<int>();
    public List<float> played_Time = new List<float>();
    public GameObject score_Tab;
    public GameObject content;
    public GameObject not_PlayedTxt;
    string session_Time;
    //void Start()
    //{
        
    //}
    private void OnEnable()
    {
        if (scene_Name == SceneName.MainMenu)
        {
            LoadHighScores();
        }
        else
        {
            LoadScores();
        }
        int coins = PlayerPrefs.GetInt("COINS");
        if (coins_Text != null)
        {
            coins_Text.text = coins.ToString();
        }
    }
    private void OnDisable()
    {
        if (scene_Name == SceneName.MainMenu)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
        }
    }
    public void AddScore(int score,float time)
    {
        print(score);
        highScores.Add(score);
        played_Time.Add(time);
        highScores.Sort((a, b) => b.CompareTo(a)); // Sort descending
        played_Time.Sort((a, b) => b.CompareTo(a));
        //if (highScores.Count > 5) // Keep top 5 scores
        //{
        //    highScores.RemoveAt(highScores.Count - 1);
        //}
        SaveHighScores();
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new ScoreList(highScores, played_Time));       
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            string json = PlayerPrefs.GetString(HighScoresKey);
            highScores = JsonUtility.FromJson<ScoreList>(json)?.scores ?? new List<int>();
            played_Time = JsonUtility.FromJson<ScoreList>(json)?.played_Time ?? new List<float>();
            print("json: " + json);
            string userName = PlayerPrefs.GetString("USERNAME");
            for (int i = 0; i < highScores.Count; i++)
            {
                //int rank = i;
               // rank++;
                GameObject tempObj = Instantiate(score_Tab, transform.position, Quaternion.identity, content.transform);
              
                tempObj.transform.GetChild(2).GetComponent<Text>().text = userName;
                tempObj.transform.GetChild(4).GetComponent<Text>().text = highScores[i].ToString();
                updateTimer(played_Time[i]);
                tempObj.transform.GetChild(3).GetComponent<Text>().text = session_Time;

            }
            not_PlayedTxt.SetActive(false);
        }
        else 
        {
            not_PlayedTxt.SetActive(true);
        }
    }
    private void LoadScores()
    {
        if (PlayerPrefs.HasKey(HighScoresKey))
        {
            string json = PlayerPrefs.GetString(HighScoresKey);
            highScores = JsonUtility.FromJson<ScoreList>(json)?.scores ?? new List<int>();
            played_Time = JsonUtility.FromJson<ScoreList>(json)?.played_Time ?? new List<float>();
            print("json: " + json);
            
        }
       
    }
    void updateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.RoundToInt(time % 60);
        string formatedSeconds = seconds.ToString();
        session_Time = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public List<int> GetHighScores()
    {
        return new List<int>(highScores);
    }
    public List<float> GetPlayedTime()
    {
        return new List<float>(played_Time);
    }
}

[System.Serializable]
public class ScoreList
{
    public List<int> scores;
    public List<float> played_Time;
    public ScoreList(List<int> scores, List<float> played_Time)
    {
        this.scores = scores;
        this.played_Time = played_Time;
    }
}
