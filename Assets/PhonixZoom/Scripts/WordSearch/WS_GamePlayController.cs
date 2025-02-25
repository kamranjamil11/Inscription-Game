using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static TransitionListener;
using System.Linq;
using System.Collections;
using System.ComponentModel;

using TMPro;

using UnityEngine.PlayerLoop;


namespace WordSearch
{
    //public enum Catagory { None, RandomWords, WordsForMyYearsGroup, SelectWords, SpellingList, Practice }
    public class WS_GamePlayController : MonoBehaviour
    {
        //public Generic_Timer gn_Timer;
        //public UIHandler ui_Handler;
        public GameObject next_Btn, back_Btn, loading_Panel;
        public GameObject boy_Avatar, girl_Avatar;
        // public Catagory wordsCatagory;

        public int selectedWord_Count;

        List<string> words = new List<string>();

        public List<string> selectWords = new List<string>();

        public int CurrentDifficulty, CurrentRepeat;
        public GameObject wordBoxRef;
        public RectTransform wordBoxRefContainer;
        public RectTransform starsContainer;
        public List<GameObject> CurrntWordList = new List<GameObject>();
        public List<GameObject> CurrntStarList = new List<GameObject>();
        string currentword;

        string SpellCheckingWord;

        [HideInInspector]
        public int WordListCounter;

        public List<string> WrongWordList;
        public string CurrentWord, InputByPlayer;

        //public List<WordGameDataModel> allGameDataModel = new List<WordGameDataModel>();
        //public List<WordGameDataModel> _gameData = new List<WordGameDataModel>();
        //public List<WordGameDataModel> wrongWordsData = new List<WordGameDataModel>();
        //public List<WordGameDataModel> practiceData = new List<WordGameDataModel>();

        //public List<WordGameDataModel> wordsToPlay = new List<WordGameDataModel>();
        private bool practiceBool;
        public GameObject RightWrongScreen;

        public Text score_Text, star_Text;
        public int word_Counter, round_Count;
        internal int current_Score, current_Stars, stars_Counter;
        internal int right_Words_Counter, wrong_Words_Counter;
        public GameObject timer_Popup;
        bool isConsuctive;
        int consective_Counter;

        public List<string> words_data = new List<string>();
        public List<AudioClip> words_audio = new List<AudioClip>();
        // public List<GameObject> words_Box = new List<GameObject>();
        public WordListLoader wordListLoader;
       // public WS_WordList word_List;
        public GameObject word_Box, container;
        public int setWords_Count, setWords_Lentgh;
        private void Start()
        {
            // isPractice = false;
            // Get the screen width and height
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            Debug.Log("Screen Resolution: " + screenWidth + " x " + screenHeight);
            if (screenWidth != 2316 && screenHeight != 904)
            {
                boy_Avatar.transform.localScale = new Vector2(0.5f, 0.5f);
                girl_Avatar.transform.localScale = new Vector2(0.5f, 0.5f);
                boy_Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
                girl_Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            }
            else
            {
                boy_Avatar.transform.localScale = new Vector2(1f, 1f);
                girl_Avatar.transform.localScale = new Vector2(1, 1);
                boy_Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220f, 50f);
                girl_Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-220f, 50f);
            }
        }
        //void SelectSoundData() 
        //{
        //    Initialize();
        //    _gameData.Clear();
        //    for (int i = 0; i < GlobalAppController.Instance.APIManager.audioClips.Count; i++)
        //    {
        //        if (GlobalAppController.Instance.APIManager.selectedData.Contains(GlobalAppController.Instance.APIManager.audioClips[i].name))
        //        {
        //            WordGameDataModel wordData = new WordGameDataModel();
        //           // wordData.name = GlobalAppController.Instance.APIManager.audioClips[i].name;
        //           // wordData.audioClip = GlobalAppController.Instance.APIManager.audioClips[i];
        //            wordData.graphemeWord = GlobalAppController.Instance.APIManager.grapheme_GameWord[i];
        //            wordData.graphemeWord_AudioClip = GlobalAppController.Instance.APIManager.grapheme_GameWord_audioClips[i];
        //            _gameData.Add(wordData);
        //        }
        //    }
        //    SetWordsLength();
        //}
        private void OnEnable()
        {
            //  if (GlobalAppController.Instance.AppDataHandler.currentCategory == AppDataHandler.Categories.selected)
            //  {
            //      AllGamesCommomData.isBackFromGameplay = true;
            //  }
            ////  if (GlobalAppController.Instance.AppDataHandler.currentCategory == AppDataHandler.Categories.selected &&
            //GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
            //  {

            //      SelectSoundData();
            //  }
            //  else
            //  {
            // GlobalAppController.Instance.AppDataHandler.AddEventOnWordDataLoaded(GetLoadedData);
            // GlobalAppController.Instance.GameLoop.AddOnStartEvent(Initialize);
            // }


        }
        //private void OnDisable()
        //{

        //    GlobalAppController.Instance.GameLoop.RemoveOnStartEvent(Initialize);          
        //    GlobalAppController.Instance.AppDataHandler.RemoveEventOnWordDataLoaded(GetLoadedData);
        //}
        //void GetLoadedData(List<WordGameDataModel> wordGameDataModels)
        //{
        //    allGameDataModel.Clear();   
        //    allGameDataModel.AddRange(wordGameDataModels);
        //    GlobalAppController.Instance.GameLoop.OnGameStart();
        //}

        bool initialized = true;
        public void Initialize()
        {
            isConsuctive = false;
            setWords_Count = 0;
            round_Count = 0;
            word_Counter = 0;
            current_Score = 0;
            current_Stars = 0;
            consective_Counter = 0;
            right_Words_Counter = 0;
            wrong_Words_Counter = 0;
            score_Text.text = current_Score.ToString();
            star_Text.text = current_Stars.ToString();

            //words_Box.ForEach(obj => 
            //{
            //    obj.SetActive(false);
            //});
            //_gameData.Clear();
            //practiceData.AddRange(wrongWordsData);
            //WordListCounter = 0;

            //_gameData.AddRange(allGameDataModel);


            // wrongWordsData.Clear();
            // initialized = false;

            words.Clear();
            SetWordsLength();
        }

        void SetWordsLength()
        {
            int a = 5;
            //if (_gameData.Count > 4)
            //{
            //    a = _gameData.Count - a;
            //}
            //else
            //{
            //    a = 0;
            //}
            setWords_Lentgh = a;
            //if (_gameData.Count < 10)
            //{



            //}
            //else 
            //{
            //    a = _gameData.Count - a;
            //}


        }
        private void Update()
        {

            //if (GlobalUiManager.isDataLoaded)
            //{
            //    gn_Timer.totalTime = 360;
            //    gn_Timer.isStop = false;
            //    GetWord();
            //    // GetStars();
            //    GlobalUiManager.isDataLoaded = false;
            //}
        }

        //void practiceFunc()
        //    {
        //        practiceBool = true;
        //        WordListCounter = 0;
        //        wordsToPlay.Clear();
        //        wordsToPlay.AddRange(wrongWordsData);
        //        // GetWord();
        //        // GetStars();
        //        // timerStarSpellerScript.taskNumber = (WordListCounter + 1).ToString();
        //        // timerStarSpellerScript.TotalTask = (wordsToPlay.Count).ToString();
        //        // timerStarSpellerScript.Task.text = timerStarSpellerScript.taskNumber + "/" + timerStarSpellerScript.TotalTask;
        //        // StarSpellerEconomy.Instance.wrongWord.Clear();
        //        // Generic_audioManager.Instance.clip_SSp = GameManager.Instance.finalSoundList;

        //        Debug.Log("Practice");
        //    }

        //public void CheckSpelling()
        //{
        //    int wordCheckCounter = 0;
        //    for (int i = 0; i < CurrntWordList.Count; i++)
        //    {
        //        if (CurrntWordList[i].transform.GetChild(0).gameObject.GetComponent<Text>().text != "")
        //        {
        //            wordCheckCounter++;
        //            if (wordCheckCounter == CurrntWordList.Count)
        //            {
        //                // OnCompleteWord();
        //                // NextButton.SetActive(true);
        //                //  starsContainer.gameObject.SetActive(false);
        //                string temp_SpellChecking = null;
        //                for (int j = 0; j < CurrntWordList.Count; j++)
        //                {
        //                    temp_SpellChecking += CurrntWordList[j].transform.GetChild(0).gameObject.GetComponent<Text>().text;
        //                }
        //                if (temp_SpellChecking == currentword)
        //                {
        //                    //int booster_Time = Convert.ToInt32(total_Time);
        //                    //if (fill_Image.fillAmount > 0.03f && fill_Image.fillAmount < 0.1f)
        //                    //{
        //                    //    booster_Time = 1;
        //                    //}
        //                    //if (booster_Time > 0)
        //                    //{
        //                    //    StartCoroutine(TimerBoosterEffect(booster_Time.ToString()));
        //                    //}
        //                }


        //            }
        //            else
        //            {
        //                starsContainer.gameObject.SetActive(true);

        //            }
        //        }
        //    }
        //}
        //public void GetRandomWords()
        //{
        //    wordsToPlay.AddRange(_gameData);

        //}
        //  public static bool isPractice;
        //  int wrongWords_Counter;
        //  public List<string> practiceWrong_Words = new List<string>();
        // public List<AudioClip> practiceWrong_Audios = new List<AudioClip>();
        void GetWord()
        {
            CurrentWord = "";
            words_data.Clear();
            words_audio.Clear();
            // Root rt = GlobalAppController.Instance.APIManager.rt;
            for (int a = 0; a < container.transform.childCount; a++)
            {
                Destroy(container.transform.GetChild(a).gameObject);
            }

            print("setWords_Lentgh:" + setWords_Lentgh);
            int i = setWords_Count;
            //for (i = 0; i < _gameData.Count /*- setWords_Lentgh*/; i++)
            //{
            //    if (i < 5)
            //    {
            //        Instantiate(word_Box, transform.position, Quaternion.identity, container.transform);


            //        //  if (GlobalAppController.Instance.AppDataHandler.currentGameType != Game_type.phoneme)
            //        // {
            //        if (_gameData[i].name != null)
            //        {
            //            _gameData[i].name.Replace("_", "");
            //            _gameData[i].name.Replace("" + i + "", "");
            //            words_data.Add(_gameData[i].name);
            //            words_audio.Add(_gameData[i].audioClip);
            //            //    }
            //            //}
            //            //else
            //            //{
            //            //    //for (int j = 0; j < 1;)
            //            //{
            //            //    int rnd = Random.Range(0, rt.dataObject.phonicZoomSounds[i].graphemeWords.Count);
            //            //    if (!words_data.Contains(rt.dataObject.phonicZoomSounds[i].graphemeWords[rnd]))
            //            //    {
            //            //        words_data.Add(rt.dataObject.phonicZoomSounds[i].graphemeWords[rnd]);
            //            //        j++;
            //            //        break;
            //            //    }
            //            //}
            //            words_data.Add(_gameData[i].graphemeWord);
            //            words_audio.Add(_gameData[i].graphemeWord_AudioClip);
            //            // words_audio.Add(_gameData[i].audioClip);
            //            // }
            //            // words_Box[i].SetActive(true);
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }

            //    // ui_Handler.grid_Panel.SetActive(true);
            //    wordListLoader.CreateGrid();
            //    CurrentWord = currentword;

            //}

            //void PlaySound()
            //    {
            //        GlobalAppController.Instance.GlobalAudioManager.playSoundOfWord();
            //    }
            //public void BlanksForHardAndSuper()
            //{
            //    if (CurrntWordList.Count <= 14)
            //    {
            //        GameObject item = Instantiate(wordBoxRef, wordBoxRefContainer);
            //        CurrntWordList.Add(item);
            //        wordBoxRefContainer.gameObject.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            //        wordBoxRefContainer.gameObject.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
            //    }
            //}

            //public RectTransform superStarContainer1, superStarContainer2, superStarContainer3;
            //void GetStars()
            //{
            //    //if (GameManager.Instance.difficulty == GameManager.Difficulty.super)
            //    //{
            //    //    for (int i = 0; i < SuperStringSplitter.Length; i++)
            //    //    {
            //    //        for (int j = 0; j < KeyBoardWordOBJ.A2Z.Count; j++)
            //    //        {
            //    //            if (KeyBoardWordOBJ.A2Z[j].GetComponent<KeyWords>().alphabet == SuperStringSplitter[i].ToString())
            //    //            {
            //    //                if (j <= 9)
            //    //                {
            //    //                    GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[j], superStarContainer1);
            //    //                    item.transform.localScale = new Vector2(1.08f, 1.08f);
            //    //                    CurrntStarList.Add(item);
            //    //                }
            //    //                else if (j > 9 && j <= 18)
            //    //                {
            //    //                    GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[j], superStarContainer2);
            //    //                    item.transform.localScale = new Vector2(1.08f, 1.08f);
            //    //                    CurrntStarList.Add(item);
            //    //                }
            //    //                else if (j < 26)
            //    //                {
            //    //                    GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[j], superStarContainer3);
            //    //                    item.transform.localScale = new Vector2(1.08f, 1.08f);
            //    //                    CurrntStarList.Add(item);
            //    //                }
            //    //                else
            //    //                {
            //    //                    GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[j], superStarContainer2);
            //    //                    item.transform.localScale = new Vector2(1.08f, 1.08f);
            //    //                    CurrntStarList.Add(item);
            //    //                }
            //    //            }
            //    //        }
            //    //    }


            //    //}
            //    //else
            //    //{

            //    //    stringSplitter = currentword.ToCharArray();

            //    //    for (int i = 0; i < currentword.Length; i++)
            //    //    {
            //    //        for (int j = 0; j < KeyBoardWordOBJ.A2Z.Count; j++)
            //    //        {
            //    //            if (KeyBoardWordOBJ.A2Z[j].GetComponent<KeyWords>().alphabet == stringSplitter[i].ToString())
            //    //            {
            //    //                GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[j], starsContainer);
            //    //                item.transform.localScale = new Vector2(1.3f, 1.3f);
            //    //                CurrntStarList.Add(item);
            //    //            }
            //    //        }
            //    //    }




            //    //    for (int i = 0; i < (int)GameManager.Instance.difficulty * StarsMultiplayer; i++)
            //    //    {
            //    //        GameObject item = Instantiate(KeyBoardWordOBJ.A2Z[Random.Range(0, 26)], starsContainer);
            //    //        item.transform.localScale = new Vector2(1.3f, 1.3f);
            //    //        CurrntStarList.Add(item);
            //    //    }
            //    //    for (int i = 0; i < CurrntStarList.Count; i++)
            //    //    {
            //    //        // Generate a random index
            //    //        int randomIndex = Random.Range(i, CurrntStarList.Count);
            //    //        // Swap the game objects at the random index and at the current index in the list
            //    //        GameObject temp = CurrntStarList[i];
            //    //        CurrntStarList[i] = CurrntStarList[randomIndex];
            //    //        CurrntStarList[randomIndex] = temp;
            //    //    }
            //    //    // Reparent the game objects to the current game object in the shuffled order
            //    //    for (int i = 0; i < CurrntStarList.Count; i++)
            //    //    {
            //    //        CurrntStarList[i].transform.parent = transform;
            //    //        CurrntStarList[i].transform.SetParent(KeyBoardWordOBJ.gameObject.transform);
            //    //        // KeyBoardWordOBJ.gameObject.transform.SetParent(CurrntStarList[i].transform);
            //    //    }
            //    //}
            //    //  KeyBoardWordOBJ.ShuffleAlphabets();
            //}
            //public void NextWordButton()
            //{
            //    GlobalAppController.Instance.GlobalAudioManager.playClick();
            //    CheckSpellWords();
            //    OnCompleteWord();
            //}
            void OnCompleteWord()
            {
                for (int i = 0; i < CurrntWordList.Count; i++)
                {

                    Destroy(CurrntWordList[i].gameObject);

                }
                CurrntWordList.Clear();

                for (int i = 0; i < CurrntStarList.Count; i++)
                {


                    Destroy(CurrntStarList[i].gameObject);

                }
                CurrntStarList.Clear();
            }
            //[HideInInspector]
            // public bool IsRight;
            //public void NextWord()
            //{

            //    GlobalAppController.Instance.GlobalAudioManager.playClick();
            //    WordListCounter += 1;
            //    word_Counter += 1;

            //    if (isPractice)
            //    {
            //        if (wrongWords_Counter < practiceWrong_Words.Count - 1)
            //        {
            //            wrongWords_Counter++;
            //            GetWord();
            //            GetStars();
            //            GlobalAppController.Instance.MenuController.PopPage();
            //        }
            //        else
            //        {
            //            wrongWords_Counter = 0;
            //            RightWrongScreen.SetActive(false);
            //            this.gameObject.SetActive(true);
            //            OnListComplete();
            //            GlobalAppController.Instance.MenuController.PopPage();
            //        }

            //    }
            //    else
            //    {
            //        if (WordListCounter >= wordsToPlay.Count)
            //        {
            //            RightWrongScreen.SetActive(false);
            //            this.gameObject.SetActive(true);
            //            OnListComplete();
            //            GlobalAppController.Instance.MenuController.PopPage();
            //        }
            //        else
            //        {
            //            GetWord();
            //            GetStars();
            //            GlobalAppController.Instance.MenuController.PopPage();
            //        }
            //    }
            //    starsContainer.gameObject.SetActive(true);

            //}

            //public void NextWord1()
            //{               
            //    word_Counter += 1;              
            //    if (word_Counter >= words.Count)
            //    {
            //        RightWrongScreen.SetActive(false);
            //        this.gameObject.SetActive(true);
            //        OnListComplete();
            //        GlobalAppController.Instance.MenuController.PopPage();
            //    }
            //    else
            //    {

            //        GetWord();
            //        GetStars();
            //        GlobalAppController.Instance.MenuController.PopPage();
            //    }
            //    starsContainer.gameObject.SetActive(true);
            //}
            void PlaySecondGrid()
            {
                back_Btn.SetActive(false);
                //if (_gameData.Count > 5)
                //{
                //    _gameData.RemoveRange(0, 5);
                //}
                //else
                //{
                //    _gameData.RemoveRange(0, _gameData.Count);
                //}

                round_Count = 0;
                // GlobalAppController.Instance.GlobalAudioManager.playClick();
                //words_Box.ForEach(obj =>
                //{
                //    obj.SetActive(false);
                //});
                //if (isPractice)
                //{
                //    if (wrongWords_Counter < practiceWrong_Words.Count - 1)
                //    {
                //        wrongWords_Counter++;
                //        GetWord();
                //       // GetStars();
                //        GlobalAppController.Instance.MenuController.PopPage();
                //    }
                //    else
                //    {
                //        wrongWords_Counter = 0;
                //        RightWrongScreen.SetActive(false);
                //        this.gameObject.SetActive(true);
                //        OnListComplete();
                //        GlobalAppController.Instance.MenuController.PopPage();
                //    }

                //}
                //else
                //{

                setWords_Count = 5;
                setWords_Lentgh = 0;
                next_Btn.SetActive(false);

                wordListLoader._loadOnStart = true;
                // ui_Handler.boardManager.ClearLetterUnits();
                // ui_Handler.lineManager.ClearLines();

                GetWord();
                // GetStars();
                //foreach (TextMeshProUGUI item in ui_Handler.boardManager.wordReference.textList)
                //{
                //    item.fontStyle = FontStyles.Normal;
                //    item.faceColor = Color.white;
                //}

                //  }
                //  starsContainer.gameObject.SetActive(true);

            }
            //IEnumerator TimerBoosterEffect(string time_Mul)
            //    {
            //        //isTimer_On = false;
            //       // booster_Text.text = time_Mul;
            //        timer_Popup.SetActive(true);
            //        yield return new WaitForSecondsRealtime(1.5f);
            //        timer_Popup.SetActive(false);
            //    }
            //void CheckSpellWords()
            //{
            //    InputByPlayer = "";
            //    SpellCheckingWord = null;
            //    for (int i = 0; i < CurrntWordList.Count; i++)
            //    {
            //        SpellCheckingWord += CurrntWordList[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;

            //        InputByPlayer += CurrntWordList[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;
            //    }

            //    if (SpellCheckingWord == currentword)
            //    {
            //        Debug.Log("Right");
            //        if (!isPractice)
            //        {

            //        }
            //        IsRight = true;
            //    }
            //    else
            //    {
            //        if (!isPractice)
            //        {
            //            if (!practiceWrong_Words.Contains(currentword))
            //            {

            //              //  EndScreenData.wrongCounter++;
            //                                    print("OnListComplete : " + EndScreenData.wrongAnswer +" , "+EndScreenData.wrongCounter);

            //            practiceWrong_Words.Add(currentword);
            //                practiceWrong_Audios.Add(GlobalAppController.Instance.GlobalAudioManager.wordSound);
            //            }

            //        }
            //        IsRight = false;
            //    }

            //}
            //public void PracticeWordsGameplay()
            //{
            //    word_Counter = 0;
            //    isPractice = true;             
            //    EndScreenData.isPracticeBox = true;
            //    Initialize();                
            //}
            //void OnListComplete()
            //{

            //    GoToEndScreen();
            //}

            //void GoToEndScreen()
            //{

            //    GlobalAppController.Instance.GlobalAudioManager.playGamesuccess();
            //    GlobalAppController.Instance.GlobalAudioManager.WellDone();

            //    EndScreenData.rightAnswer = right_Words_Counter;
            //    EndScreenData.wrongAnswer = wrongWordsData.Count;             
            //    EndScreenData.wrongWords.Clear();
            //    initialized = true;
            //    UIHandler.isRandom = false;
            //    Debug.Log(wrongWordsData.Count + " :wrongWords Data Count, endscreen wrong words count: " + EndScreenData.wrongWords.Count);
            ////if (EndScreenData.rightAnswer >= EndScreenData.wrongAnswer)
            ////{
            ////    GlobalAppController.Instance.GlobalAudioManager.playGamesuccess();
            ////}
            ////else
            ////{
            ////    GlobalAppController.Instance.GlobalAudioManager.playGameFailure();
            ////}
            ////wrongWordsData.ForEach(a =>
            ////    {
            ////        EndScreenData.wrongWords.Add(a.name);
            ////    });
            //    GlobalAppController.Instance.GameLoop.OnGameEnd();
            //}
            // public List<GameObject> temp;

            //public void RedoAllSpelling()
            //{
            //    //if (!isTimer_On)
            //    //{
            //    //    isTimer_On = true;
            //    //    //total_Time = 5;
            //    //}
            //    for (int i = 0; i < wordBoxRefContainer.gameObject.transform.childCount; i++)
            //    {
            //        temp.Add(wordBoxRefContainer.gameObject.transform.GetChild(i).gameObject);
            //    }
            //    for (int i = 0; i < temp.Count; i++)
            //    {
            //      //  temp[i].GetComponent<Spellword>().OnClickButton();
            //    }
            //    temp.Clear();
            //}
            //public void GamePlayBack()
            //{
            //    if (!UIHandler.isRandom)
            //    {
            //        GlobalAppController.Instance.MenuController.PopPage();
            //    }
            //    Reset();
            //    UIHandler.isRandom = false;
            //    OnCompleteWord();
            //    GlobalAppController.Instance.GameLoop.OnGameSelection();
            //}
            void Reset()
            {
                round_Count = 0;
                word_Counter = 0;
                initialized = true;
                WrongWordList.Clear();
                //_gameData.Clear();
                //wrongWordsData.Clear();
                //practiceData.Clear();
                //wordsToPlay.Clear();

            }

            void ScoreUpdate(bool isTrue)
            {
                if (isTrue)
                {
                    right_Words_Counter++;
                    consective_Counter++;
                    if (isConsuctive)
                    {
                        current_Score += 4 * consective_Counter;
                    }
                    else
                    {
                        current_Score += 4;
                    }

                    stars_Counter++;
                    if (stars_Counter == 5)
                    {
                        stars_Counter = 0;
                        current_Stars += 2;
                        star_Text.text = current_Stars.ToString();
                    }
                    isConsuctive = true;
                }
                score_Text.text = current_Score.ToString();
            }

            void ReplayWithSameSetting()
            {
                back_Btn.SetActive(false);
                //gn_Timer.totalTime = 360;
                // gn_Timer.isStop = false;
                wordListLoader._loadOnStart = true;
                //isPractice = false;         
                // practiceWrong_Words.Clear();
                // ui_Handler.boardManager.ClearLetterUnits();           
                // ui_Handler.lineManager.ClearLines();
                //words_Box.ForEach(item =>
                //{
                //    item.gameObject.SetActive(false);
                //    item.gameObject.SetActive(true);
                //});
                Reset();

                //  if (GlobalAppController.Instance.AppDataHandler.currentCategory == AppDataHandler.Categories.selected &&
                //GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
                //  {

                //      SelectSoundData();
                //  }
                //  else 
                //  {
                Initialize();
                // }        
                GetWord();
                // GetStars();
                //foreach (TextMeshProUGUI item in ui_Handler.boardManager.wordReference.textList)
                //{
                //    item.fontStyle = FontStyles.Normal;
                //    item.faceColor = Color.white;
                //}


                // ui_Handler.boardManager.wordReference.Populate1(words_data, words_audio);
            }
        }
    }
}
//[System.Serializable]
//        public class StarSpellerGameData
//        {
//            public string name;
//            public SSData ss_Data;
//        }
//        [System.Serializable]
//        public class SSData
//        {
//            public Sprite[] word_Sp;
//            public String[] words;
//            public AudioClip[] word_Audio;

//        }
    


