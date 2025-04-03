
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class UIHandler : MonoBehaviour
{
   
    public static event Action<int> OnCoinsCollected; // Event for coin update
    private const string LastLoginKey = "LastLoginDate";
    private const string StreakCountKey = "StreakCount";
    private const string RewardClaimedKey = "RewardClaimed";

    public InputField user_Name;
    public TextMeshProUGUI userName_Txt;
    public Text user_Txt;
    public GameObject daily_Reward_Popup;
    public GameObject congrats_Popup;
    public GameObject loadingScreen;
    public GameObject leaderBoard;
    public Text coins_Text, gameHighScoreText;
    public GameObject mainCanvas;
    public GameObject loginPopup;
    public GameObject settingsPopup;
    public GameObject coins_Shop;
    public GameObject daily_Challenge_Popup;
    public GameObject daily_Challenges_Prefab;
    public GameObject daily_Challenges_Parent;
    public RectTransform underline;
    public string[] first_Daily_Challenges;
    public string[] daily_Challenges;

    private void Start()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("COINS"))
        {
            PlayerPrefs.SetInt("COINS", 1000);
        }
        int coins = PlayerPrefs.GetInt("COINS");
        if (coins_Text != null)
        {
            coins_Text.text = coins.ToString();
        }
        StartCoroutine(Login());
        if (PlayerPrefs.HasKey("FIRST_CHALLENGE"))
        {
            daily_Challenges[0] = PlayerPrefs.GetString("FIRST_CHALLENGE");
        }
       
    }

    public IEnumerator Login()
    {
        if (PlayerPrefs.HasKey("USERNAME")) 
        {
          string name=  PlayerPrefs.GetString("USERNAME");
            userName_Txt.text = name;
            Invoke("CheckDailyReward", 1);
        }
        else 
        {
            yield return new WaitForSeconds(1);
            loginPopup.SetActive(true);
        }
        
    }

    public void SaveUserName()
    {
        if(!string.IsNullOrEmpty(user_Txt.text)) 
        {
            loginPopup.SetActive(false);
            print("USERNAME: "+ user_Txt.text);
            userName_Txt.text = user_Txt.text;
            PlayerPrefs.SetString("USERNAME", user_Txt.text);
            Invoke("CheckDailyReward", 1);
        }
    }

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
            List<int> n_List= new List<int>();
            n_List.Clear();
            for (int i = 0; i < 13; i++)
            {
                if (PlayerPrefs.HasKey("DAILYCHALLENGE" + i))
                {
                    PlayerPrefs.DeleteKey("DAILYCHALLENGE" + i);
                    PlayerPrefs.DeleteKey("ROUTINECHALLENGE" + i);
                }
            }
           
             PlayerPrefs.DeleteKey("TOTALDAILYCHALLENGE");


            int first_Chl = UnityEngine.Random.Range(0, first_Daily_Challenges.Length);
            PlayerPrefs.SetInt("FIRST_CHALLENGE_ID", first_Chl);
            PlayerPrefs.SetInt("DAILYCHALLENGE" + 0, 0);
            PlayerPrefs.SetInt("ROUTINECHALLENGE" + 0, 0);
            daily_Challenges[0] = first_Daily_Challenges[first_Chl];
            PlayerPrefs.SetString("FIRST_CHALLENGE", first_Daily_Challenges[first_Chl]);
           
            for (int i = 1; i < 3;)
            {                            
                int rnd = UnityEngine.Random.Range(1, daily_Challenges.Length);
                if (!n_List.Contains(rnd))
                {
                    n_List.Add(rnd);
                    //print("rnd: " + rnd);
                    PlayerPrefs.SetInt("DAILYCHALLENGE" + i, rnd);
                    PlayerPrefs.SetInt("ROUTINECHALLENGE" + i, rnd);
                    i++;
                }
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
       GameObject tempObj= Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
        tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        AudioManager.instance.PlaySound(0);
    }
    public void DailyChallenge()
    {
        daily_Challenge_Popup.SetActive(true);
        
        for (int i =0; i < 3; i++)
        {
          int ch_Num= PlayerPrefs.GetInt("ROUTINECHALLENGE" + i);
            GameObject dc_Tab=  Instantiate(daily_Challenges_Prefab, transform.position, Quaternion.identity, daily_Challenges_Parent.transform);
            dc_Tab.GetComponent<DailyChallenge>().id = ch_Num;
            dc_Tab.GetComponent<DailyChallenge>().description.text = daily_Challenges[ch_Num];          
        }
        for (int i = 0; i < daily_Challenges_Parent.transform.childCount; i++)
        {
           // int id = daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().id;
           // print("DAILYCHALLENGE: "+id);
            if (!PlayerPrefs.HasKey("DAILYCHALLENGE" + i))
            {
                daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().claimed.SetActive(true);
            }
            else
            {
                daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().coins_Tab.SetActive(true);
            }
            

        }
        AudioManager.instance.PlaySound(0);
    }
    public void BackToMainMenu()
    {
        leaderBoard.SetActive(false);
        daily_Challenge_Popup.SetActive(false);
        AudioManager.instance.PlaySound(0);
    }
   
    public void EnterUserName() 
    {
        print("Text Lenght: "+ user_Txt.text.Length);
        if (user_Txt.text.Length <= 12)
        {
            user_Txt.text = user_Name.text;            
        }
    }
}
