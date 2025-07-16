
using Samples.Purchasing.GooglePlay.FraudDetection;
using Samples.Purchasing.GooglePlay.RestoringTransactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
//using Unity.Mathematics;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
   
    public static event Action<int> OnCoinsCollected; // Event for coin update
    private const string LastLoginKey = "LastLoginDate";
    private const string StreakCountKey = "StreakCount";
    private const string RewardClaimedKey = "RewardClaimed";

    public RestoringTransactions IAP_Restore;
    public GameObject mainCanvas;
    public GameObject loadingPanel;
    [Header("Landscape UI")] 
    public InputField user_Name;
    public TextMeshProUGUI userName_Txt;
    public Text user_Txt;
    public GameObject landscape_UI;
    public GameObject daily_Reward_Popup;
    public GameObject congrats_Popup;
    public GameObject loadingScreen;
    public GameObject leaderBoard;
    public Text coins_Text;  
    public GameObject loginPopup;
    public GameObject settingsPopup, settingsPopup_IOS;
    public GameObject coins_Shop;
    public GameObject daily_Challenge_Popup;
    public GameObject daily_Challenges_Prefab;
    public GameObject daily_Challenges_Parent;
    public GameObject removeAds_Landscape;
    public GameObject removeAds_Popup_Landscape;
    [Header("Portrait UI")]
    public Button[] allButtons;
    public GameObject touch_Btn;
    public GameObject hand_Icon;
    public GameObject left_InfoPanel, right_InfoPanel, left_bottom_InfoPanel, bottom_InfoPanel;  
    public InputField user_Name_Portrait;
    public TextMeshProUGUI userName_Txt_Portrait;
    public Text user_Txt_Portrait;
    public GameObject portrait_UI;
    public GameObject daily_Reward_Popup_Portrait;
    public GameObject congrats_Popup_Portrait;
    public GameObject loadingScreen_Portrait;
    public GameObject leaderBoard_Portrait;
    public Text coins_Text_Portrait;  
    public GameObject loginPopup_Portrait;
    public GameObject settingsPopup_Portrait, settingsPopup_Portrait_IOS;
    public GameObject coins_Shop_Portrait;
    public GameObject daily_Challenge_Popup_Portrait;
    public GameObject daily_Challenges_Prefab_Portrait;
    public GameObject daily_Challenges_Parent_Portrait;
    public GameObject removeAds_Portrait;
    public GameObject removeAds_Popup_Portrait;
    public string[] first_Daily_Challenges;
    public string[] daily_Challenges;
    public HashSet<string> wordSet = new HashSet<string>();
    
    private void Start()
    {     
       FirebaseData.instance.DateLoadFunc();
        //IsPalindrome(121);
        // PrimeNumber();
        Time.timeScale = 1;
       // OnGameStartDataSet();
       
        if (coins_Text != null)
        {
            int coins = PlayerPrefs.GetInt("COINS");

            coins_Text.text = FormatNumber(coins);
            coins_Text_Portrait.text = FormatNumber(coins);
        }
        
        StartCoroutine(Login());
        if (PlayerPrefs.HasKey("FIRST_CHALLENGE"))
        {
            daily_Challenges[0] = "Word of the day " + "(" + PlayerPrefs.GetString("FIRST_CHALLENGE")+")";
        }
        
        RemoveAds();
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            hand_Icon.gameObject.SetActive(true);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(0,1);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(325,-410);
            ButtonsInteractable(0);
        }
        ChangeOrientation();
        StartCoroutine(LoadWords());
       
    }
 
    void OnGameStartDataSet()
    {
        //if (!PlayerPrefs.HasKey("COINS"))
        //{
        //    PlayerPrefs.SetInt("COINS", 1000);
        //}

        //if (!PlayerPrefs.HasKey("HINT_POWERUP"))
        //{
        //    PlayerPrefs.SetInt("HINT_POWERUP", 1);
        //}
        

        //if (!PlayerPrefs.HasKey("LOTUS_POWERUP"))
        //{
        //    PlayerPrefs.SetInt("LOTUS_POWERUP", 1);
        //}
       

        //if (!PlayerPrefs.HasKey("SCRAB_POWERUP"))
        //{
        //    PlayerPrefs.SetInt("SCRAB_POWERUP", 1);
        //}
    }
    void ChangeOrientation()
    {
        if (!SettingPopup.isPortrait)
        {
            landscape_UI.SetActive(false);
            portrait_UI.SetActive(true);
           
        }
        else
        {
            landscape_UI.SetActive(true);
            portrait_UI.SetActive(false);
        }
    }
    public IEnumerator Login()
    {
        if (PlayerPrefs.HasKey("USERNAME")) 
        {
          string name=  PlayerPrefs.GetString("USERNAME");
            userName_Txt.text = name;
            userName_Txt_Portrait.text = name;
            Invoke("CheckDailyReward", 0.5f);
        }
        else 
        {
            yield return new WaitForSeconds(0.5f);
            if (!SettingPopup.isPortrait)
            {
                loginPopup_Portrait.SetActive(true);
            }
            else
            {
                loginPopup.SetActive(true);
            }
           
        }
        
    }

    public void SaveUserName()
    {
        if (!SettingPopup.isPortrait)
        {
            if (!string.IsNullOrEmpty(user_Txt_Portrait.text))
            {             
                loginPopup_Portrait.SetActive(false);
                print("USERNAME: " + user_Txt_Portrait.text);            
                userName_Txt_Portrait.text = user_Txt_Portrait.text;
                userName_Txt.text = user_Txt_Portrait.text;
                PlayerPrefs.SetString("USERNAME", user_Txt_Portrait.text);
                Invoke("CheckDailyReward", 0.5f);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(user_Txt.text))
            {
                loginPopup.SetActive(false);             
                print("USERNAME: " + user_Txt.text);
                userName_Txt.text = user_Txt.text;
                userName_Txt_Portrait.text = user_Txt.text;
                PlayerPrefs.SetString("USERNAME", user_Txt.text);
                Invoke("CheckDailyReward", 0.5f);
            }
        }
        
    }

    void CheckDailyReward()
    {
        
        string lastLogin = PlayerPrefs.GetString(LastLoginKey, "");
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        if (lastLogin != today)
        {
           // Time.timeScale = 0;
            if (!SettingPopup.isPortrait)
            {
                daily_Reward_Popup_Portrait.SetActive(true);
            }
            else
            {
                daily_Reward_Popup.SetActive(true);
            }
           
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
            string wordOfDay = "";
            for (int i = 0; i < 1;)
            {
                int rndWord = UnityEngine.Random.Range(0, wordSet.Count);
                wordOfDay = wordSet.ElementAt(rndWord);
                if (wordOfDay.Length < 6)
                {
                    i++;
                    break;
                }
            }
            
            print("wordOfDay: "+ wordOfDay);
            daily_Challenges[0] ="Word of the day "+"("+ wordOfDay /*first_Daily_Challenges[first_Chl]*/+ ")";
            PlayerPrefs.SetString("FIRST_CHALLENGE", wordOfDay /*first_Daily_Challenges[first_Chl]*/);
           
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
            if (!SettingPopup.isPortrait)
            {
                daily_Reward_Popup_Portrait.SetActive(false);
            }
            else
            {
                daily_Reward_Popup.SetActive(false);
            }
           
            int coins = PlayerPrefs.GetInt("COINS");
            coins += 25;
            PlayerPrefs.SetInt("COINS", coins);
            coins_Text.text = FormatNumber(coins);
            coins_Text_Portrait.text =FormatNumber(coins);
            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            PlayerPrefs.SetString(LastLoginKey, today);
            //int streak = PlayerPrefs.GetInt(StreakCountKey, 1);
            // int reward = 100 + (streak * 10); // Example reward scaling

            Debug.Log($"Reward Claimed! You received {coins} coins.");

            PlayerPrefs.SetInt(RewardClaimedKey, 1);
            PlayerPrefs.Save();
            FirebaseData.instance.DataSaveFun();
        }
        else
        {
            Debug.Log("Reward already claimed.");
        }
       
        AudioManager.instance.PlaySound(0);
    }
    public void ButtonsInteractable(int id) 
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable=false;
        }
        allButtons[id].interactable = true;
    }
    public void RemoveAds()
    {
        if (PlayerPrefs.HasKey("NO_ADS"))
        {
           
            removeAds_Portrait.GetComponent<Button>().interactable = false;
           // removeAds_Portrait.transform.GetChild(0).gameObject.SetActive(true);
            removeAds_Landscape.GetComponent<Button>().interactable = false;
            //removeAds_Landscape.transform.GetChild(0).gameObject.SetActive(true);
            
        }
        else 
        {
            removeAds_Portrait.GetComponent<Button>().interactable = true;
           // removeAds_Portrait.transform.GetChild(0).gameObject.SetActive(false);
            removeAds_Landscape.GetComponent<Button>().interactable = true;
           // removeAds_Landscape.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void PurchaseAds()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, -450);

            left_InfoPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            left_InfoPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            left_InfoPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            left_InfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(884, -988);
            left_InfoPanel.GetComponentInChildren<Text>().text = "Buy the ad-free version for a smoother, uninterrupted experience.";
            left_InfoPanel.SetActive(true);
            ButtonsInteractable(2);
        }
        else
        {
            if (!SettingPopup.isPortrait)
            {
                removeAds_Popup_Landscape.SetActive(false);
                removeAds_Popup_Portrait.SetActive(true);
            }
            else
            {
                removeAds_Popup_Landscape.SetActive(true);
                removeAds_Popup_Portrait.SetActive(false);
            }
        }

    }
   public void RemoveAdsCompleted()
    {
        loadingPanel.SetActive(false);
        print("Ads_Purchased");
        PlayerPrefs.SetString("NO_ADS", "Purchased");
        RemoveAds();
        removeAds_Popup_Landscape.SetActive(false);
        removeAds_Popup_Portrait.SetActive(false);
    }
    public void PlayButton()
    {
        hand_Icon.gameObject.SetActive(false);
        if (!SettingPopup.isPortrait)
        {
            loadingScreen_Portrait.SetActive(true);
        }
        else
        {
            loadingScreen.SetActive(true);
        }
       
        AudioManager.instance.PlaySound(0);
    }
    public void TouchButton()
    {
        touch_Btn.SetActive(false);      
        left_InfoPanel.SetActive(false);
        right_InfoPanel.SetActive(false);
        left_bottom_InfoPanel.SetActive(false);
        bottom_InfoPanel.SetActive(false);
       // if (allButtons[allButtons.Length - 1].interactable == false)
       // {
            hand_Icon.SetActive(true);
        //}
        AudioManager.instance.PlaySound(0);
    }
    public void SettingBtn()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            

            left_InfoPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            left_InfoPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            left_InfoPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            left_InfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(884, -700);
            left_InfoPanel.GetComponentInChildren<Text>().text = "Switch between landscape and portrait mode, toggle music, and manage sound effects here.";
            left_InfoPanel.SetActive(true);
            if (!PlayerPrefs.HasKey("NO_ADS"))
            {
                hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(325, -722);
                ButtonsInteractable(1);
            }
            else
            {
                hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, -450);
                ButtonsInteractable(2);
            }
        }
        else
        {
            GameObject tempSetting = null;
            if (!SettingPopup.isPortrait)
            {

//#if UNITY_IOS
            tempSetting = Instantiate(settingsPopup_Portrait_IOS, transform.position, Quaternion.identity, mainCanvas.transform);
           // tempSetting.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
//#endif
                //tempSetting = Instantiate(settingsPopup_Portrait, transform.position, Quaternion.identity, mainCanvas.transform);
            }
            else
            {
//#if UNITY_IOS
            tempSetting = Instantiate(settingsPopup_IOS, transform.position, Quaternion.identity, mainCanvas.transform);
            //tempSetting.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
//#endif
                //tempSetting = Instantiate(settingsPopup, transform.position, Quaternion.identity, mainCanvas.transform);
            }
            tempSetting.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Vector3 pos = tempSetting.GetComponent<RectTransform>().anchoredPosition;
            pos.z = 0;
            tempSetting.GetComponent<RectTransform>().localPosition = pos;
            Time.timeScale = 0f;
        }
        AudioManager.instance.PlaySound(0);
    }
    public void LeaderBoardBtn()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(190,-365);
            left_bottom_InfoPanel.GetComponentInChildren<Text>().text = "Keep pushing your limits by beating your own high score.";
            left_bottom_InfoPanel.SetActive(true);
            ButtonsInteractable(6);
            
        }
        else
        {
            if (!SettingPopup.isPortrait)
            {
                leaderBoard.SetActive(false);
                leaderBoard_Portrait.SetActive(true);
            }
            else
            {
                leaderBoard.SetActive(true);
                leaderBoard_Portrait.SetActive(false);
            }
        }
        AudioManager.instance.PlaySound(0);
    }
    public void CoinsShopBtn()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 455);
            right_InfoPanel.GetComponentInChildren<Text>().text = "Keep track of your coins here. Use them to buy power-ups.";
            right_InfoPanel.SetActive(true);
            ButtonsInteractable(3);
        }
        else
        {
            GameObject tempObj = null;
            if (!SettingPopup.isPortrait)
            {
                tempObj = Instantiate(coins_Shop_Portrait, transform.position, Quaternion.identity, mainCanvas.transform);

            }
            else
            {
                tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
            }

            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Vector3 pos = tempObj.GetComponent<RectTransform>().anchoredPosition;
            pos.z = 0;
            tempObj.GetComponent<RectTransform>().localPosition = pos;
        }
        AudioManager.instance.PlaySound(0);
    }
    public void CoinsShopBtn1()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(630,130);

            bottom_InfoPanel.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0f);
            bottom_InfoPanel.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0f);
            bottom_InfoPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            bottom_InfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-996,747);
            bottom_InfoPanel.GetComponentInChildren<Text>().text = "Buy extra coins or unlock powerful boosts to enhance your wordplay.";
            bottom_InfoPanel.SetActive(true);
            ButtonsInteractable(5);
        }
        else
        {
            GameObject tempObj = null;
            if (!SettingPopup.isPortrait)
            {
                tempObj = Instantiate(coins_Shop_Portrait, transform.position, Quaternion.identity, mainCanvas.transform);

            }
            else
            {
                tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
            }

            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Vector3 pos = tempObj.GetComponent<RectTransform>().anchoredPosition;
            pos.z = 0;
            tempObj.GetComponent<RectTransform>().localPosition = pos;
        }
        AudioManager.instance.PlaySound(0);
    }
    public void DailyChallenge()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            touch_Btn.SetActive(true);
            hand_Icon.SetActive(false);
            hand_Icon.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0);
            hand_Icon.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0);
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            hand_Icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-455,115);

            bottom_InfoPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
            bottom_InfoPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
            bottom_InfoPanel.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            bottom_InfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-307,1022);
            bottom_InfoPanel.GetComponentInChildren<Text>().text = "Complete three unique challenges every day to earn extra rewards.";
            bottom_InfoPanel.SetActive(true);
            ButtonsInteractable(4);
        }
        else
        {
            if (!SettingPopup.isPortrait)
            {
                daily_Challenge_Popup_Portrait.SetActive(true);
            }
            else
            {
                daily_Challenge_Popup.SetActive(true);
            }
            for (int i = 0; i < 3; i++)
            {
                int ch_Num = PlayerPrefs.GetInt("ROUTINECHALLENGE" + i);
                GameObject dc_Tab = null;
                if (!SettingPopup.isPortrait)
                {
                    dc_Tab = Instantiate(daily_Challenges_Prefab_Portrait, transform.position, Quaternion.identity, daily_Challenges_Parent_Portrait.transform);
                }
                else
                {
                    dc_Tab = Instantiate(daily_Challenges_Prefab, transform.position, Quaternion.identity, daily_Challenges_Parent.transform);
                }
                dc_Tab.GetComponent<DailyChallenge>().id = ch_Num;
                dc_Tab.GetComponent<DailyChallenge>().description.text = daily_Challenges[ch_Num];
            }
            if (!SettingPopup.isPortrait)
            {
                for (int i = 0; i < daily_Challenges_Parent_Portrait.transform.childCount; i++)
                {
                    if (!PlayerPrefs.HasKey("DAILYCHALLENGE" + i))
                    {
                        daily_Challenges_Parent_Portrait.transform.GetChild(i).GetComponent<DailyChallenge>().claimed.SetActive(true);
                    }
                    else
                    {
                        daily_Challenges_Parent_Portrait.transform.GetChild(i).GetComponent<DailyChallenge>().coins_Tab.SetActive(true);
                    }
                    if (i == 0)
                    {
                        daily_Challenges_Parent_Portrait.transform.GetChild(i).GetComponent<DailyChallenge>().coins_Tab.GetComponentInChildren<Text>().text = "100";
                    }
                }
            }
            else
            {
                for (int i = 0; i < daily_Challenges_Parent.transform.childCount; i++)
                {
                    if (!PlayerPrefs.HasKey("DAILYCHALLENGE" + i))
                    {
                        daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().claimed.SetActive(true);
                    }
                    else
                    {
                        daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().coins_Tab.SetActive(true);
                    }
                    if (i == 0)
                    {
                        daily_Challenges_Parent.transform.GetChild(i).GetComponent<DailyChallenge>().coins_Tab.GetComponentInChildren<Text>().text = "100";
                    }
                }
            }
        }
        AudioManager.instance.PlaySound(0);
    }
   
    public void BackToMainMenu()
    {
        leaderBoard.SetActive(false);
        leaderBoard_Portrait.SetActive(false);
        daily_Challenge_Popup.SetActive(false);
        daily_Challenge_Popup_Portrait.SetActive(false);
        AudioManager.instance.PlaySound(0);
    }   
    public void EnterUserName() 
    {
        if (!SettingPopup.isPortrait)
        {
            print("Text Lenght: " + user_Txt_Portrait.text.Length);
            if (user_Txt_Portrait.text.Length <= 12)
            {
                user_Txt_Portrait.text = userName_Txt_Portrait.text;
            }
        }
        else
        {
            print("Text Lenght: " + user_Txt.text.Length);
            if (user_Txt.text.Length <= 12)
            {
                user_Txt.text = user_Name.text;
            }
        }
        
    }

    public static string FormatNumber(long number)
    {      
         if (number >= 1_000_000)
            return (number / 1_000_000f).ToString("0.#") + "M";
        else if (number >= 10000)
            return (number / 1000f).ToString("0.#") + "K";
        else
            return number.ToString();
    }

    public IEnumerator LoadWords()
    {
        string fileName = "wordlist1.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        print(path);
        if (!File.Exists(path))
        {
            Debug.LogError("File not found in persistentDataPath, copying from StreamingAssets...");
            yield return StartCoroutine(CopyFileFromStreamingAssets(fileName, path));
        }

        LoadAllWords(path);
    }

    void LoadAllWords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        string jsonData = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(jsonData))
        {
            Debug.LogError("JSON file is empty or corrupted.");
            return;
        }

        Debug.Log("Loaded JSON Data (First 100 chars): " + jsonData.Substring(0, Mathf.Min(100, jsonData.Length))); // Debug

        // 🔹 Split the words using newline character
        //  wordList = new List<string>(jsonData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

        // 🔹 Store in HashSet for fast lookup
        wordSet = new HashSet<string>(jsonData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

        Debug.Log("Words Loaded Successfully: " + wordSet.Count);
       
    }
    private IEnumerator CopyFileFromStreamingAssets(string fileName, string destinationPath)
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, fileName);

#if UNITY_ANDROID && !UNITY_EDITOR
    UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(sourcePath);
    yield return request.SendWebRequest();

    if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
    {
        Debug.LogError("Failed to load from StreamingAssets: " + request.error);
    }
    else
    {
        File.WriteAllBytes(destinationPath, request.downloadHandler.data);
        // LoadAllWords(destinationPath);
    }
#else
        // For Windows, Mac, iOS (non-Android)
        if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, destinationPath, true);
            //  LoadAllWords(destinationPath);
        }
        else
        {
            Debug.LogError("Source file not found in StreamingAssets: " + sourcePath);
        }
        yield return null;
#endif
    }



    void PrimeNumber()
    {
        int[] digits = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        foreach (int number in digits)
        {
            if (IsPrime(number))
            {
                print("Prime Num: " + number);
            }
        }
    }

    bool IsPrime(int num)
    {
        if (num < 2)
            return false;

        for (int i = 2; i <= Mathf.Sqrt(num); i++)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }

    bool IsPalindrome(int number)
    {
        int original = number;
        int reversed = 0;

        while (number > 0)
        {
            int digit = number % 10;
            reversed = reversed * 10 + digit;
            number /= 10;
        }

        return original == reversed;
    }

}
