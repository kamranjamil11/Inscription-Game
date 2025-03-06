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

public class GameController : MonoBehaviour
    {
        public EventHandler<OncCastLetterArgs> OnCastLetter;
        public class OncCastLetterArgs : EventArgs
        {
            public SingleLetter castedLetter;
        }
        private List<GameObject> activeLetters;
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
        public GameObject gridFrame;
        public GameObject right_Setting_Btn, left_Setting_Btn;
        public GameObject right_Man, left_Man;
        public GameObject right_Toggle, left_Toggle;
        public GameObject mainCanvas;
        public GameObject settingsPopup;
        public Text score_Text, gameOverScore_Text,highScore_Text;
        public Text coins_Text;
        public Text correctWord;
        public Text bestTime;
        public Text currTime;
        public Text countDownText;
        public float totalTime = 60f;
        private float timeLeft;
        private bool gameOn = true;
        private bool isToggle;
        private int minutes;
        private int seconds;
        public static int score;
        
        private Color tmpCol;
        public GameObject notEnoughCoinsPanel;
        public GameObject hintPrefab,hintBox;
        private string hintInfo;
        public List<string> hints = new List<string>() { "1stLetter", "2ndLetter", "descTextual" };
        public HashSet<string> wordSet = new HashSet<string>(); // Fast lookup storage
        public AudioSource sfx;
        public Button hintButton;
        public Generic_Timer generic_Timer;
        public LevelCreator LevelCreator;
        public HighScoreManager hS_Manager;
    private void Start()
    {
        score_Text.text = score.ToString();
        int coins = PlayerPrefs.GetInt("COINS");
        coins_Text.text = coins.ToString();
        PlayerPrefs.SetInt("HINT_POWERUP", 5);
        hintButton.GetComponentInChildren<Text>().text = "5";
        activeLetters = new List<GameObject>();
        lvlCreator = FindObjectOfType<LevelCreator>();
        timeLeft = totalTime;
        tmpCol = lvlCreator.lettersGrid[0].GetComponentInChildren<Text>().color;

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
            e.castedLetter.gameObject.GetComponent<Image>().sprite = selectedLetterSprite;
            e.castedLetter.gameObject.GetComponentInChildren<Text>().color = Color.white;
            e.castedLetter.gameObject.GetComponent<Animator>().SetTrigger("Press");
            sfx.Play();
            sfx.pitch = sfx.pitch + 0.22f;
            formedWord += e.castedLetter.Value;

            if (activeLetters.Count > 1)
            {
                lastLetter = activeLetters[activeLetters.Count - 2];
            }
            currLetter = activeLetters[activeLetters.Count - 1];
            LinksManager(lastLetter, currLetter);
        }

        private void Update()
        {
            if (gameOn)
            {
                timeLeft -= Time.deltaTime;
                minutes = Mathf.FloorToInt(timeLeft / 60f);
                seconds = Mathf.FloorToInt(timeLeft % 60f);
                countDownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            }

            if (timeLeft <= 0)
            {
                gameOn = false;
                losePanel.SetActive(true);
                countDownText.text = "00:00";
            }
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
            bool isFound = WordExists(formedWord);
            //if (formedWord == LevelCreator.levelWord)
            if (isFound)
            {
                    print("WordComplete:");
                    //show win popup
                   // winPanel.SetActive(true);
                   // correctWord.text = LevelCreator.levelWord;
                    gameOn = false;
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
                ScoreSystem(formedWord.Length);
                formedWord = "";
                Next();

            }
                else
                {
                    results.Clear();
                    foreach (GameObject selectedLetter in activeLetters)
                    {
                        selectedLetter.GetComponent<Image>().sprite = unSelectedLetterSprite;
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
        public void ScoreSystem(int wordLength) 
        {
            switch (wordLength)
            {
                case 2:
                    score += 200;
                    break;
                case 3:
                    score += 300;
                    break;
                case 4:
                    score += 400;
                    break;
                case 5:
                    score += 500;
                    break;
                case 6:
                    score += 600;
                    break;
                case 7:
                    score += 700;
                    break;
                case 8:
                    score += 800;
                    break;
                case 9:
                    score += 900;
                    break;
                case 10:
                    score += 1000;
                    break;
            }
            score_Text.text= score.ToString();
            int HS = PlayerPrefs.GetInt("HIGHSCORE");
            if (score > HS) 
            {
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
           
        }
        public void SubscribeToEventOnPointerEnter()
        {
            OnCastLetter += OnNewLetterCasted;
        }
        private void LinksManager(GameObject lastLetter, GameObject currentLetter)
        {
            if (lastLetter != null)
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
                else if (angletodir == 90 && dot == -1)
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
        public void Retry()
        {
            Generic_Timer.totalTime = 60;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void GoToHome()
        {
            Generic_Timer.totalTime = 60;
            SceneManager.LoadScene("MainMenu");
        }
        public void HintPowerUp()
        {
           int hint_Count= PlayerPrefs.GetInt("HINT_POWERUP");
            if (hint_Count > 0)
            {
                hint_Count--;
                PlayerPrefs.SetInt("HINT_POWERUP", hint_Count);
                hintButton.GetComponentInChildren<Text>().text = hint_Count.ToString();
                for (int i = 0; i < lvlCreator.hintObjs.Count; i++)
                {
                lvlCreator.hintObjs[i].GetComponent<Animator>().Play("Hint");
                    //lvlCreator.hintObjs[i].GetComponent<Image>().sprite = selectedLetterSprite;
                }
            }
            else 
            {
                GameObject tempObj = Instantiate(hintPrefab, transform.position, Quaternion.identity, mainCanvas.transform);
                tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
               // hintBox.SetActive(true);
               // hintInfo = "There is no more hints to give..";
               //// hintButton.interactable = false;
               // hintBox.GetComponentInChildren<Text>().text = hintInfo;
            }
        }
        public void ToggleBtn()
        {
            if (isToggle)
            {
                isToggle=false;
                right_Man.SetActive(false);
                right_Toggle.SetActive(false);
                right_Setting_Btn.SetActive(false);

                left_Man.SetActive(true);
                left_Toggle.SetActive(true);
                left_Setting_Btn.SetActive(true);

                gridFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(280,0);
            }
            else 
            {
                isToggle = true;
                              
                right_Man.SetActive(true);
                right_Toggle.SetActive(true);
                right_Setting_Btn.SetActive(true);

                left_Man.SetActive(false);
                left_Toggle.SetActive(false);
                left_Setting_Btn.SetActive(false);

                gridFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(-280, 0);
            }
        }
        public void GameOver() 
        {
            int HS = PlayerPrefs.GetInt("HIGHSCORE");
            highScore_Text.text = HS.ToString();
            gameOverScore_Text.text = score.ToString();
            losePanel.SetActive(true);
            hS_Manager.AddScore(score);
        }
        public void SettingBtn()
        {
           GameObject tempObj= Instantiate(settingsPopup, transform.position, Quaternion.identity, mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        public void Next() 
        {
            int lvl=PlayerPrefs.GetInt("LEVEL_NUMBER");
            lvl++;
            PlayerPrefs.SetInt("LEVEL_NUMBER", lvl);
            Generic_Timer.totalTime += 30;
        foreach (var item in lvlCreator.lettersGrid)
        {
            item.GetComponent<Image>().sprite = unSelectedLetterSprite;
            item.GetComponentInChildren<Text>().color = tmpCol;
            item.GetComponent<Animator>().SetTrigger("Idle");

        }
            LevelCreator.CreatWord();
        }


    


    

   

    IEnumerator LoadWords()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "wordlist.txt");

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    wordSet.Add(line.Trim().ToLower()); // Convert to lowercase for case-insensitive check
                }
            }
            Debug.Log("File Loaded! Total Words: " + wordSet.Count);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
    }

    // Check if a word exists in the loaded data
    public bool WordExists(string word)
    {
        return wordSet.Contains(word.ToLower()); // Fast O(1) lookup
    }
}

    


