using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour
{
    public EventHandler<OncCastLetterArgs> OnCastLetter;
    public class OncCastLetterArgs : EventArgs
    {
        public SingleLetter castedLetter;
    }
    public List<string> finded_Words = new List<string>();
    public List<string> words_Lenght = new List<string>();
    public List<GameObject> activeLetters;
    public string formedWord = "";
   
    List<RaycastResult> results = new List<RaycastResult>();
    private GameObject lastLetter;
    private GameObject currLetter;
    public Sprite selectedLetterSprite;
    public Sprite unSelectedLetterSprite;
    private LevelCreator lvlCreator;
    public GameObject tempParent;
    [Header("Win")]
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject loadingPanel;
    public GameObject gridFrame;
    public GameObject lotusPowerPanel;
    // public GameObject right_Setting_Btn, left_Setting_Btn;
    public GameObject coinsAdded_Box, challenge_Box;
    // public GameObject right_Toggle, left_Toggle;
    public GameObject scoreAdded_Box;
    public GameObject word_Box;
    public GameObject gameOver_Content;
    public GameObject mainCanvas;
    public GameObject settingsPopup;
    public GameObject coins_Shop;
    public GameObject congrats_Popup;
    public GameObject hintPrefab, hintBox;
    public GameObject block_Brake;
    public GameObject scarabPower;
    public Text score_Text, gameOverScore_Text, highScore_Text;
    public Text coins_Text;
    public Text correctWord;
    public Text bestTime;
    public Text currTime;
    public Text countDownText;
    public float totalTime = 60f;
    private float timeLeft;
    private bool gameOn = true;
    public bool isToggle;
    public bool isNextWork;
    bool isScarab = false;
    private int minutes;
    private int seconds;
    public static int score;

    private Color tmpCol;
    
    private string hintInfo;
    public List<string> hints = new List<string>() { "1stLetter", "2ndLetter", "descTextual" };
    public HashSet<string> wordSet = new HashSet<string>(); // Fast lookup storage
    public AudioSource sfx;
    public Button hintButton,lotusButton,scrabButton;
    public Generic_Timer generic_Timer;
    public LevelCreator LevelCreator;
    public HighScoreManager hS_Manager;
    [Header("Daily Challenges")]
    public float start_Time, end_Time;
    public float ChallengeTime;
    public int usedHint;
    public int consistent_Word;
    public string currentWord = "";
    public string[] Challenges_Description;
    private void Start()
    {
        isNextWork=true;
        score = 0;
        usedHint = 0;
        currentWord = "";
        gameOn = true;
        consistent_Word = 0;
        finded_Words.Clear();
        words_Lenght.Clear();   
        start_Time = Time.time;
        score_Text.text = score.ToString();
        int coins = PlayerPrefs.GetInt("COINS");
        coins_Text.text = coins.ToString();
        if (!PlayerPrefs.HasKey("HINT_POWERUP")) 
        {
            PlayerPrefs.SetInt("HINT_POWERUP", 1); 
        }        
        int hint = PlayerPrefs.GetInt("HINT_POWERUP");
        hintButton.GetComponentInChildren<Text>().text = hint.ToString();

        if (!PlayerPrefs.HasKey("LOTUS_POWERUP"))
        {
            PlayerPrefs.SetInt("LOTUS_POWERUP", 1);
        }        
        int lotus_Hint = PlayerPrefs.GetInt("LOTUS_POWERUP");
        lotusButton.GetComponentInChildren<Text>().text = lotus_Hint.ToString();

        if (!PlayerPrefs.HasKey("SCRAB_POWERUP"))
        {
            PlayerPrefs.SetInt("SCRAB_POWERUP", 1);
        }
        int scrab_Hint = PlayerPrefs.GetInt("SCRAB_POWERUP");
        scrabButton.GetComponentInChildren<Text>().text = scrab_Hint.ToString();

        activeLetters = new List<GameObject>();
        lvlCreator = FindObjectOfType<LevelCreator>();
        timeLeft = totalTime;
        tmpCol = lvlCreator.lettersGrid[0].GetComponentInChildren<Text>().color;
        string first_Chl = PlayerPrefs.GetString("FIRST_CHALLENGE");
        Challenges_Description[0]= first_Chl; 
        StartCoroutine(LoadWords());

    }

   
    private void OnNewLetterCasted(object sender, OncCastLetterArgs e)
    {
        OnCastLetter -= OnNewLetterCasted;
        print("Select_Letter");
        //don't allow selecting the same letter twice
        foreach (GameObject l in activeLetters)
        {
            if (e.castedLetter.gameObject == l)
            {
                return;
            }
        }

        //it's valid number add it to array 
        //valid : last letter is a neighbour and it's repeted in the list
        activeLetters.Add(e.castedLetter.gameObject);

        //don't allow selecting letter that's not a neighbour
        if (activeLetters.Count > 1)
        {
            if (!activeLetters[activeLetters.Count - 2].GetComponent<SingleLetter>().possibleWays.Contains(e.castedLetter.gameObject))
            {
                activeLetters.Remove(e.castedLetter.gameObject);
                return;
            }
        }
        e.castedLetter.gameObject.GetComponent<Image>().sprite = e.castedLetter.gameObject.GetComponent<SingleLetter>().selected_Sprite; //selectedLetterSprite;
        e.castedLetter.gameObject.GetComponentInChildren<Text>().color = Color.white;
       // e.castedLetter.gameObject.GetComponent<Animator>().SetTrigger("Press");
        sfx.Play();
        sfx.pitch = sfx.pitch + 0.22f;
        formedWord += e.castedLetter.Value;

        if (activeLetters.Count > 1)
        {
            lastLetter = activeLetters[activeLetters.Count - 2];
        }
        currLetter = activeLetters[activeLetters.Count - 1];
        LinksManager(lastLetter, currLetter);

        bool isFound = WordExists(formedWord);
        if (isFound)
        {
            foreach (var item in activeLetters)
            {
                item.gameObject.GetComponent<Image>().sprite = item.gameObject.GetComponent<SingleLetter>().green_Sprite;
            }           
        }
        else 
        {
            foreach (var item in activeLetters)
            {
                item.gameObject.GetComponent<Image>().sprite = item.gameObject.GetComponent<SingleLetter>().selected_Sprite;
            }
        }
    }

    private void Update()
    {
        if (isNextWork)
        {
            ChallengeTime += Time.deltaTime;
            if (ChallengeTime >= 90 && ChallengeTime < 91)
            {
                TimeChallenges(1);
            }
            else if (ChallengeTime >= 120 && ChallengeTime < 121)
            {
                TimeChallenges(7);
            }
            else if (ChallengeTime >= 180 && ChallengeTime < 181)
            {
                TimeChallenges(9);
            }
            //if (gameOn)
            //{
            //    timeLeft -= Time.deltaTime;
            //    minutes = Mathf.FloorToInt(timeLeft / 60f);
            //    seconds = Mathf.FloorToInt(timeLeft % 60f);
            //    countDownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            //}

            //if (timeLeft <= 0)
            //{
            //    gameOn = false;
            //    losePanel.SetActive(true);
            //    countDownText.text = "00:00";
            //}
            if (Input.GetMouseButton(0))
            {
                // Create a new PointerEventData object
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                // Raycast from the mouse position and store the results
                EventSystem.current.RaycastAll(eventData, results);
            }

#if MOBILE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Create a new PointerEventData object
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = touch.position;
                // Raycast from the touch position and store the results
                EventSystem.current.RaycastAll(eventData, results);
            }
        }
#endif
            //whene relese mouse button
            if (Input.GetMouseButtonUp(0))
            {
                //verify result
                Vector3 worldPosition = Vector3.zero;
               bool isFound = WordExists(formedWord);
                //if (formedWord == LevelCreator.levelWord)
                if (isFound)
                {
                    consistent_Word++;
                    currentWord = formedWord.ToLower();
                    finded_Words.Add(formedWord);
                    if (currentWord.Length >= 5)
                    {
                        words_Lenght.Add(currentWord);
                    }
                    print("WordComplete:");
                    //show win popup
                    // winPanel.SetActive(true);
                    // correctWord.text = LevelCreator.levelWord;

                    //currTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                    //if (!PlayerPrefs.HasKey(LevelCreator.levelWord))
                    //{
                    //    PlayerPrefs.SetString(LevelCreator.levelWord, string.Format("{0:0}:{1:00}", minutes, seconds));
                    //    bestTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                    //}
                    //else
                    //{
                    //    checkIfBestRecord(LevelCreator.levelWord);
                    //}
                    foreach (GameObject selectedLetter in activeLetters)
                    {
                        //For Dissolve
                        // selectedLetter.GetComponent<Image>().material = selectedLetter.GetComponent<DissolveController>().mat;       
                        // selectedLetter.GetComponent<DissolveController>().isDissolving = true; 
                        //For Rock
                        selectedLetter.GetComponent<Image>().enabled = false;
                        Instantiate(block_Brake, selectedLetter.transform.position, Quaternion.identity);
                    }
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10f; // Distance from the camera (adjust as needed)
                    worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                    ScoreSystem(formedWord.Length, worldPosition);

                    StartCoroutine(Next("Next"));

                }
                else
                {
                    results.Clear();
                    foreach (GameObject selectedLetter in activeLetters)
                    {

                        selectedLetter.GetComponent<Image>().sprite = selectedLetter.GetComponent<SingleLetter>().unSelected_Sprite;//unSelectedLetterSprite;
                        selectedLetter.GetComponentInChildren<Text>().color = tmpCol;

                    }
                    activeLetters.Clear();
                    currLetter = null;
                    lastLetter = null;
                    formedWord = "";
                    foreach (Transform linkerGrp in tempParent.transform)
                    {
                        foreach (Transform link in linkerGrp)
                        {
                            if (link.gameObject.activeInHierarchy)
                            {
                                link.gameObject.SetActive(false);
                            }
                        }
                    }

                }
                sfx.pitch = 1f;
                
            }

            if (results.Count == 1 && results[0].gameObject.GetComponent<SingleLetter>() != null)
            {
                // Handle the UI element hit by the raycast
                GameObject hitLetter = results[0].gameObject;

                OnCastLetter?.Invoke(this, new OncCastLetterArgs
                {
                    castedLetter = hitLetter.GetComponent<SingleLetter>()
                });

            }

        }
    }
   
    public void TileBox() 
    {
        if (isScarab)
        {
            isScarab = false;
            Generic_Timer.isStop = false;    
            GameObject scarab = Instantiate(scarabPower, scrabButton.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity, scrabButton.transform.parent.transform);          
            scarab.GetComponent<RectTransform>().anchoredPosition=new Vector2(-302,-436);
            GameObject targetObj = EventSystem.current.currentSelectedGameObject;
            scarab.GetComponent<ScoreMultiplayer>().target = targetObj.transform;
            StartCoroutine(scarab.GetComponent<ScoreMultiplayer>().ScarabMoveUI(targetObj.GetComponent<RectTransform>().anchoredPosition, 2f, targetObj, lvlCreator));
            hintButton.interactable = true;
            scrabButton.interactable = true;
            lotusButton.interactable = true;
            AudioManager.instance.PlaySound(4);
        }
    }
    void TimeChallenges(int challengeNum) 
    {
        if (gameOn)
        {
            if (PlayerPrefs.HasKey("DAILYCHALLENGE" + challengeNum))
            {
                CheckDailyReset();
            }
        }
    }
    IEnumerator ScoreUpdate(int newScore,float time,Vector2 pos)
    {
        GameObject score_Mover = Instantiate(scoreAdded_Box, pos, Quaternion.identity, score_Text.transform.parent.transform);
        score_Mover.GetComponentInChildren<Text>().text = "+" + newScore.ToString();

        score += newScore;
        Generic_Timer.totalTime += time;   
        yield return new WaitForSeconds(0.7f);
        score_Text.text = score.ToString();
        AudioManager.instance.PlaySound(6);
        int HS = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > HS)
        {
            PlayerPrefs.SetInt("HIGHSCORE", score);
        }
        if (Generic_Timer.totalTime > 60)
        {
            Generic_Timer.totalTime = 60;
        }
    }
    public void ScoreSystem(int wordLength,Vector2 mousePos)
    {
        scoreAdded_Box.SetActive(true);
     
        switch (wordLength)
        {
            case 2:                             
               StartCoroutine(ScoreUpdate(200, 2, mousePos));
                break;
            case 3:                               
                StartCoroutine(ScoreUpdate(300, 3, mousePos));
                break;
            case 4:                             
                StartCoroutine(ScoreUpdate(400, 4, mousePos));
                break;
            case 5:               
               
                StartCoroutine(ScoreUpdate(500, 5, mousePos));
                break;
            case 6:               
               
                StartCoroutine(ScoreUpdate(600, 6, mousePos));
                break;
            case 7:              
               
                StartCoroutine(ScoreUpdate(700, 6, mousePos));
                break;
            case 8:               
               
                StartCoroutine(ScoreUpdate(800, 6, mousePos));
                break;
            case 9:              
               
                StartCoroutine(ScoreUpdate(900, 6, mousePos));
                break;
            case 10:              
                
                StartCoroutine(ScoreUpdate(1000, 6, mousePos));
                break;
        }

        CheckDailyReset();
    }
    void CheckDailyReset()
    {
        string lastPlayedDate = PlayerPrefs.GetString("LASTPLAYEDDATEKEY", "");
        string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

        if (lastPlayedDate != todayDate)
        {
            int totalChallenge = PlayerPrefs.GetInt("TOTALDAILYCHALLENGE");
            if ((totalChallenge < 3))
            {
                for (int i = 0; i < 3; i++)
                {
                    StartCoroutine(DailyChallenge1(i));
                }
            }
            else
            {
                PlayerPrefs.SetString("LASTPLAYEDDATEKEY", todayDate);
            }
            PlayerPrefs.Save();
        }
        else 
        {
            print("DailyChallengeCompleted");
        }
    }
    public void FirstChallengeFinder(int Challenge_Id, int Challenge_Num)
    {
        if (PlayerPrefs.HasKey("FIRST_CHALLENGE_ID"))
        {
            //int first_Chl = PlayerPrefs.GetInt("FIRST_CHALLENGE_ID");
            //if (first_Chl == 0)
            //{
                if (currentWord == Challenges_Description[0])
                {
                    ChallengeComplete(Challenge_Id, Challenge_Num);
                }
            //}
            //else if (first_Chl == 1)
            //{
            //    if (currentWord.Length == 4)
            //    {
            //        ChallengeComplete(Challenge_Id, Challenge_Num);
            //    }
            //}
            //else if (first_Chl == 2)
            //{
            //    if (currentWord.Length == 5)
            //    {
            //        ChallengeComplete(Challenge_Id, Challenge_Num);
            //    }
            //}
            //else if (first_Chl == 3)
            //{
            //    if (currentWord.Length == 6)
            //    {
            //        ChallengeComplete(Challenge_Id, Challenge_Num);
            //    }
            //}
            //else if (first_Chl == 4)
            //{
            //    if (currentWord.Length == 7)
            //    {
            //        ChallengeComplete(Challenge_Id, Challenge_Num);
            //    }
            //}
            //else if (first_Chl == 5)
            //{
            //    if (currentWord.Length == 8)
            //    {
            //        ChallengeComplete(Challenge_Id, Challenge_Num);
            //    }
            //}

        }
    }
    public IEnumerator DailyChallenge1(int Challenge_Id)
    {
        if (PlayerPrefs.HasKey("DAILYCHALLENGE" + Challenge_Id))
        {
            int Challenge_Num = PlayerPrefs.GetInt("DAILYCHALLENGE" + Challenge_Id);
            switch (Challenge_Num)
            {
                case 0:                    
                    FirstChallengeFinder(Challenge_Id, Challenge_Num);                                                            
                    break;
                case 1:
                    if (score >= 1500)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    
                    break;
                case 2:
                    if (ChallengeTime >= 90)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    
                    break;
                case 3:
                    if (consistent_Word >= 10)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);                     
                    }
                    
                    break;
                case 4:
                    if (usedHint == 1)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 5:
                    if (usedHint == 0&& score >= 2000)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 6:
                    int wordCount= 0;
                    for (int i = 0; i < words_Lenght.Count; i++)
                    {
                        if (words_Lenght[i].Length >=5)
                        {
                            wordCount++;                         
                        }
                    }
                    if (wordCount==3)
                    {                      
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 7:
                    if (ChallengeTime >= 120)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 8:
                    if (score >= 2500)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 9:
                    if (ChallengeTime >= 180)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 10:
                    if (finded_Words.Count >= 25)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 11:
                    if (currentWord.Contains("z") || currentWord.Contains("q"))
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 12:
                    if (currentWord.Length >= 10)
                    {
                        ChallengeComplete(Challenge_Id, Challenge_Num);
                    }
                    break;
                case 13:
                    if (hS_Manager.highScores.Count != 0)
                    {
                        if (score > hS_Manager.highScores[0])
                        {
                            ChallengeComplete(Challenge_Id, Challenge_Num);
                        }
                    }
                    break;
                
            }
        }
             
        yield return new WaitForSeconds(2f);
        coinsAdded_Box.SetActive(false);
        challenge_Box.SetActive(false);

    }
  

    void ChallengeComplete(int ch_Num,int chg_Id)
    {
        if (gameOn)
        {
            coinsAdded_Box.SetActive(true);
            challenge_Box.SetActive(true);
            int coins = PlayerPrefs.GetInt("COINS");
            coins += 25;
            coins_Text.text = coins.ToString();
            PlayerPrefs.SetInt("COINS", coins);
            int totalChallenge = PlayerPrefs.GetInt("TOTALDAILYCHALLENGE");
            totalChallenge++;
            PlayerPrefs.SetInt("TOTALDAILYCHALLENGE", totalChallenge);
            PlayerPrefs.DeleteKey("DAILYCHALLENGE" + ch_Num);
            if (chg_Id == 0) 
            {
                challenge_Box.GetComponentInChildren<Text>().text = "Word of the day " + "(" + Challenges_Description[chg_Id]+")";
            }
            else
            {
                challenge_Box.GetComponentInChildren<Text>().text = Challenges_Description[chg_Id];
            }
            AudioManager.instance.PlaySound(6);
        }
    }
    public void SubscribeToEventOnPointerEnter()
    {
        OnCastLetter += OnNewLetterCasted;
    }
    private void LinksManager(GameObject lastLetter, GameObject currentLetter)
    {
        if (lastLetter != null&&activeLetters.Contains(lastLetter))
        {
            //check the target is behin or above
            Vector3 dir = (currentLetter.transform.position - lastLetter.transform.position).normalized;
            float dot = Vector3.Dot(lastLetter.transform.up, dir);
            float angletodir = Vector3.SignedAngle(lastLetter.transform.forward, dir, Vector3.up);

            if (angletodir == 90 && dot == 0)
            {
                //normal right
                lastLetter.GetComponent<SingleLetter>().linkers[0].SetActive(true);

            }
            else if (angletodir == -90 && dot == 0)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[2].SetActive(true);
            }
            else if (angletodir == 90 && dot == 1)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[3].SetActive(true);
            }
            else if ((angletodir == 90 && dot == -1)||(lastLetter.name == "B_2" && currentLetter.name== "E_5"))
            {
                lastLetter.GetComponent<SingleLetter>().linkers[1].SetActive(true);
            }
            else if (angletodir == 90 && dot > -1 && dot < -0.5f)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[5].SetActive(true);
            }
            else if (angletodir == -90 && dot > -1 && dot < -0.5f)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[6].SetActive(true);
            }
            else if (angletodir == 90 && dot < 1 && dot > 0.5f)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[4].SetActive(true);
            }
            else if (angletodir == -90 && dot < 1 && dot > 0.5f)
            {
                lastLetter.GetComponent<SingleLetter>().linkers[7].SetActive(true);
            }
        }
    }
    private void checkIfBestRecord(string wordKey)
    {
        int lastMins = 0;
        int lastSecs = 0;
        string lastRecord = PlayerPrefs.GetString(wordKey);
        MatchCollection matches = Regex.Matches(lastRecord, @"(\d+):(\d+)");

        foreach (Match match in matches)
        {
            lastMins = int.Parse(match.Groups[1].Value);
            lastSecs = int.Parse(match.Groups[2].Value);
        }
        if (minutes == lastMins)
        {
            if (seconds > lastSecs)
            {
                //we have a new record
                PlayerPrefs.SetString(wordKey, string.Format("{0:0}:{1:00}", minutes, seconds));
                bestTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
            else
            {
                //we go with the last record
                bestTime.text = lastRecord;
            }
        }
        else
        {
            if (minutes > lastMins)
            {
                //we have a new record
                PlayerPrefs.SetString(wordKey, string.Format("{0:0}:{1:00}", minutes, seconds));
                bestTime.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
            else
            {
                //we go with the last record
                bestTime.text = lastRecord;
            }
        }
    }

    public void useHint()
    {
        //3 types of hints 
        //0 = focus on first letter
        //1 = focus on last letter
        //2 = give textual hint

        //pick a random one
        int x = UnityEngine.Random.Range(0, hints.Count);
        hintBox.SetActive(false);
        hintBox.SetActive(true);
        if (hints.Count <= 0)
        {
            hintInfo = "There is no more hints to give..";
            hintButton.interactable = false;
            hintBox.GetComponentInChildren<Text>().text = hintInfo;
            return;
        }
        switch (hints[x])
        {
            case "1stLetter":
                foreach (SingleLetter l in lvlCreator.lettersGrid)
                {
                    if (l.Value == LevelCreator.levelWord[0].ToString())
                    {
                        l.GetComponent<Image>().sprite = selectedLetterSprite;
                        hintInfo = "First Letter Revealed !";
                        hintBox.GetComponentInChildren<Text>().text = hintInfo;
                        hints.Remove(hints[x]);

                        return;
                    }
                }
                break;
            case "descTextual":
                hintInfo = PlayerPrefs.GetString(LevelCreator.levelWord + "Desc");
                hintBox.GetComponentInChildren<Text>().text = hintInfo;
                hints.Remove(hints[x]);
                break;
            case "2ndLetter":
                foreach (SingleLetter l in lvlCreator.lettersGrid)
                {
                    if (l.Value == LevelCreator.levelWord[LevelCreator.levelWord.Length - 1].ToString())
                    {
                        l.GetComponent<Image>().sprite = selectedLetterSprite;
                        hintInfo = "Last Letter Revealed !";
                        hintBox.GetComponentInChildren<Text>().text = hintInfo;
                        hints.Remove(hints[x]);
                        return;
                    }
                }
                break;
        }



    }

    public void pauseBtn()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void unPauseBtn()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void CoinsShopBtn()
    {
        GameObject tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
        tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        AudioManager.instance.PlaySound(0);
    }
    public void Retry()
    {
        Generic_Timer.totalTime = 60;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.PlaySound(0);
    }
    public void GoToHome()
    {
        Generic_Timer.totalTime = 60;
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PlaySound(0);
    }
    public void HintPowerUp()
    {
        usedHint++;
        int hint_Count = PlayerPrefs.GetInt("HINT_POWERUP");
        if (hint_Count > 0)
        {
            hint_Count--;
            PlayerPrefs.SetInt("HINT_POWERUP", hint_Count);
            hintButton.interactable = false;
            scrabButton.interactable = false;
            lotusButton.interactable = false;
            hintButton.GetComponentInChildren<Text>().text = hint_Count.ToString();
            for (int i = 0; i < lvlCreator.hintObjs.Count; i++)
            {
                lvlCreator.hintObjs[i].GetComponent<Animator>().Play("Hint");
                //lvlCreator.hintObjs[i].GetComponent<Image>().sprite = selectedLetterSprite;
            }
            AudioManager.instance.PlaySound(2);
        }
        else
        {
            GameObject tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.GetComponent<Popup>().LeftAndRightClick();
            // hintBox.SetActive(true);
            // hintInfo = "There is no more hints to give..";
            //// hintButton.interactable = false;
            // hintBox.GetComponentInChildren<Text>().text = hintInfo;
        }
        CheckDailyReset();
        
    }
    public void LotusPowerUp()
    {
        usedHint++;
        int hint_Count = PlayerPrefs.GetInt("LOTUS_POWERUP");
        if (hint_Count > 0)
        {
            hint_Count--;
            PlayerPrefs.SetInt("LOTUS_POWERUP", hint_Count);
            hintButton.interactable = false;
            scrabButton.interactable = false;
            lotusButton.interactable = false;
            lotusButton.GetComponentInChildren<Text>().text = hint_Count.ToString();
            lotusPowerPanel.SetActive(true);
            StartCoroutine(ShuffleList(lvlCreator.grid));
            AudioManager.instance.PlaySound(5);
        }
        else
        {
            GameObject tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.GetComponent<Popup>().LeftAndRightClick();
        }
        CheckDailyReset();
       
    }
    public void ScrabPowerUp()
    {
        
        usedHint++;
        int hint_Count = PlayerPrefs.GetInt("SCRAB_POWERUP");
        if (hint_Count > 0)
        {
            isScarab = true;
            hint_Count--;
            PlayerPrefs.SetInt("SCRAB_POWERUP", hint_Count);
            hintButton.interactable = false;
            scrabButton.interactable = false;
            lotusButton.interactable = false;
            scrabButton.GetComponentInChildren<Text>().text = hint_Count.ToString();
            for (int i = 0; i < lvlCreator.lettersGrid.Count; i++)
            {
                lvlCreator.lettersGrid[i].GetComponent<Animator>().Play("Hint");
                //lvlCreator.hintObjs[i].GetComponent<Image>().sprite = selectedLetterSprite;
            }
            AudioManager.instance.PlaySound(3);
        }
        else
        {
            GameObject tempObj = Instantiate(coins_Shop, transform.position, Quaternion.identity, mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.GetComponent<Popup>().LeftAndRightClick();
            // hintBox.SetActive(true);
            // hintInfo = "There is no more hints to give..";
            //// hintButton.interactable = false;
            // hintBox.GetComponentInChildren<Text>().text = hintInfo;
        }
        CheckDailyReset();
        AudioManager.instance.PlaySound(0);
    }
    public IEnumerator ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex =UnityEngine.Random.Range(i, list.Count);
            // Swap elements
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < lvlCreator.lettersGrid.Count; i++)
        {
            lvlCreator.lettersGrid[i].GetComponentInChildren<SingleLetter>().Value = lvlCreator.grid[i].ToString();
            lvlCreator.lettersGrid[i].GetComponentInChildren<Text>().text = lvlCreator.grid[i].ToString();
            lvlCreator.lotusPowerGrid[i].GetComponentInChildren<Text>().text = lvlCreator.grid[i].ToString();
        }
        yield return new WaitForSeconds(2f);
        lotusPowerPanel.SetActive(false);
        hintButton.interactable = true;
        scrabButton.interactable = true;
        lotusButton.interactable = true;
    }

    //public void ToggleBtn()
    //{
    //    if (isToggle)
    //    {
    //        isToggle = false;
    //        right_Man.SetActive(false);
    //        // right_Toggle.SetActive(false);
    //        right_Setting_Btn.SetActive(false);

    //        left_Man.SetActive(true);
    //        //left_Toggle.SetActive(true);
    //        left_Setting_Btn.SetActive(true);

    //        gridFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(280, 0);
    //    }
    //    else
    //    {
    //        isToggle = true;

    //        right_Man.SetActive(true);
    //        // right_Toggle.SetActive(true);
    //        right_Setting_Btn.SetActive(true);

    //        left_Man.SetActive(false);
    //        //left_Toggle.SetActive(false);
    //        left_Setting_Btn.SetActive(false);

    //        gridFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(-280, 0);
    //    }
    //}
    public void GameOver()
    {
        gameOn = false;
        end_Time = Time.time;
        int HS = PlayerPrefs.GetInt("HIGHSCORE");
        highScore_Text.text = HS.ToString();
        gameOverScore_Text.text = score.ToString();
        losePanel.SetActive(true);

        float timePlayed= end_Time- start_Time;
        hS_Manager.AddScore(score, timePlayed);

        for (int i = 0; i < finded_Words.Count; i++)
        {
            if (i< 6)
            {
                GameObject wordBox = Instantiate(word_Box, transform.position, Quaternion.identity, gameOver_Content.transform);
                wordBox.GetComponentInChildren<Text>().text = finded_Words[i];
            }
            else 
            {
                break;
            }
        }
        AudioManager.instance.PlaySound(1);
    }
    public void SettingBtn()
    {
        
        GameObject tempObj = Instantiate(settingsPopup, transform.position, Quaternion.identity, mainCanvas.transform);
        tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        Time.timeScale = 0f;
        AudioManager.instance.PlaySound(0);
    }
    public IEnumerator Next(string key)
    {
        isNextWork = false;
        foreach (var item in activeLetters)
        {        
            item.transform.GetChild(0).GetComponent<Text>().enabled = false;                   
            foreach (var linker in item.GetComponent<SingleLetter>().linkers)
            {
                linker.SetActive(false);
            }
        }
       
        yield return new WaitForSeconds(1f);
        hintButton.interactable = true;
        foreach (var item in activeLetters)
        {

            //For Dissolve
            // item.GetComponent<DissolveController>().isDissolving = false;
            // item.GetComponent<DissolveController>().dissolveAmount = 1f;
            // item.GetComponent<Image>().material = null;
            //For Rock
            item.GetComponent<Image>().enabled = true;
            //For Rock and Dissolve
            item.transform.GetChild(0).GetComponent<Text>().enabled = true;
            item.GetComponent<Image>().sprite = item.GetComponent<SingleLetter>().unSelected_Sprite;
            item.GetComponentInChildren<Text>().color = tmpCol;
            
        }
        
       
        foreach (var item in lvlCreator.lettersGrid)
        {
            AnimatorStateInfo stateInfo = item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            // Current animation ka naam check karna
            if (stateInfo.IsName("Hint"))
            {
                item.GetComponent<Animator>().SetTrigger("Idle");
            }
        }
        hintButton.interactable = true;
        scrabButton.interactable = true;
        lotusButton.interactable = true;
        LevelCreator.CreatWord(key);
        formedWord = "";
        currentWord = "";
        //usedHint = 0;
    }

    public IEnumerator LoadWords()
    {
        string fileName = "wordlist1.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        print(path);
        if (!File.Exists(path))
        {
            Debug.LogError("File not found in persistentDataPath, copying from StreamingAssets...");
            StartCoroutine(CopyFileFromStreamingAssets(fileName, path));
        }
        else
        {
            
            LoadAllWords(path);
        }


        //string filePath = Path.Combine(Application.streamingAssetsPath, "wordlist.txt");

        //if (File.Exists(filePath))
        //{
        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            wordSet.Add(line.Trim().ToLower()); // Convert to lowercase for case-insensitive check
        //        }
        //    }
        //    Debug.Log("File Loaded! Total Words: " + wordSet.Count);
        //}
        //else
        //{
        //    Debug.LogError("File not found: " + filePath);
        //}

        yield return null;
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

        using (UnityWebRequest www = UnityWebRequest.Get(sourcePath))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                File.WriteAllBytes(destinationPath, www.downloadHandler.data);
                Debug.Log("File copied successfully.");
                LoadAllWords(destinationPath);
            }
            else
            {
                Debug.LogError("Error copying file: " + www.error);
            }
        }
    }

    // Check if a word exists in the loaded data
    public bool WordExists(string word)
    {
        return wordSet.Contains(word.ToLower()); // Fast O(1) lookup
    }
}




