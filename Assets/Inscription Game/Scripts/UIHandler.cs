
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour
{
    public static event Action<int> OnCoinsCollected; // Event for coin update

    
    public GameObject daily_Reward_Popup;
    public GameObject congrats_Popup;
    public GameObject loadingScreen;
    public GameObject leaderBoard;
    public Text coins_Text, gameHighScoreText;
    public GameObject mainCanvas;
    public GameObject settingsPopup;
    public GameObject coins_Shop;
    public GameObject daily_Challenge_Popup;
    public GameObject daily_Challenges_Prefab;
    public GameObject daily_Challenges_Parent;
    public string[] daily_Challenges;
    
    private void Start()
    {
        if (!PlayerPrefs.HasKey("COINS"))
        {
            PlayerPrefs.SetInt("COINS", 1000);
        }
        int coins = PlayerPrefs.GetInt("COINS");
        if (coins_Text != null)
        {
            coins_Text.text = coins.ToString();
        }
        Invoke("CheckDailyReward",1);
       // CheckDailyReward();
    }
    
    private const string LastLoginKey = "LastLoginDate";
    private const string StreakCountKey = "StreakCount";
    private const string RewardClaimedKey = "RewardClaimed";

   

    void CheckDailyReward()
    {
        
        string lastLogin = PlayerPrefs.GetString(LastLoginKey, "");
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        if (lastLogin != today)
        {
            Time.timeScale = 0;
            daily_Reward_Popup.SetActive(true);
            int streak = PlayerPrefs.GetInt(StreakCountKey, 0);

            if (!string.IsNullOrEmpty(lastLogin))
            {
                DateTime lastLoginDate = DateTime.Parse(lastLogin);
                if ((DateTime.UtcNow - lastLoginDate).TotalDays == 1)
                {
                    streak++; // Consecutive login
                }
                else
                {
                    streak = 1; // Reset streak if missed a day
                }
            }
            else
            {
                streak = 1; // First-time login
            }

            
            PlayerPrefs.SetInt(StreakCountKey, streak);
            PlayerPrefs.SetInt(RewardClaimedKey, 0);
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt("DAILYCHALLENGE" + i, 0);      
            }

            PlayerPrefs.Save();

            Debug.Log($"Daily reward available! Streak: {streak} days.");
        }
        else
        {
            Debug.Log("Daily reward already claimed today.");
        }
    }

    public void ClaimReward()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt(RewardClaimedKey, 0) == 0)
        {
            daily_Reward_Popup.SetActive(false);
            int coins = PlayerPrefs.GetInt("COINS");
            coins += 25;
            PlayerPrefs.SetInt("COINS", coins);
            coins_Text.text = coins.ToString();
            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            PlayerPrefs.SetString(LastLoginKey, today);
            //int streak = PlayerPrefs.GetInt(StreakCountKey, 1);
            // int reward = 100 + (streak * 10); // Example reward scaling

            Debug.Log($"Reward Claimed! You received {coins} coins.");

            PlayerPrefs.SetInt(RewardClaimedKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Reward already claimed.");
        }
        AudioManager.instance.PlaySound(0);
    }


    public void PlayButton()
    {
        loadingScreen.SetActive(true);
        AudioManager.instance.PlaySound(0);
    }
    public void SettingBtn()
    {
        Instantiate(settingsPopup, transform.position, Quaternion.identity, mainCanvas.transform);
        Time.timeScale = 0f;
        AudioManager.instance.PlaySound(0);
    }
    public void LeaderBoardBtn()
    {
        leaderBoard.SetActive(true);
        AudioManager.instance.PlaySound(0);
    }
    public void CoinsShopBtn()
    {
        Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
        AudioManager.instance.PlaySound(0);
    }
    public void DailyChallenge()
    {
        daily_Challenge_Popup.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
           
            GameObject dc_Tab=  Instantiate(daily_Challenges_Prefab, transform.position, Quaternion.identity, daily_Challenges_Parent.transform);
            dc_Tab.GetComponent<DailyChallenge>().id = i;
            dc_Tab.GetComponent<DailyChallenge>().description.text = daily_Challenges[i];
        }
        AudioManager.instance.PlaySound(0);
    }
    public void BackToMainMenu()
    {
        leaderBoard.SetActive(false);
        daily_Challenge_Popup.SetActive(false);
        AudioManager.instance.PlaySound(0);
    }
}
