//using System;
//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using Random = UnityEngine.Random;
//using System.Linq;

//using UnityEngine.Serialization;


//namespace WordSearch
//{
//    public class WS_WordList : Singleton<WS_WordList>
//    {
//        public GameObject selecWords_Panel;
//        public GameObject WordRef, phoneme_Grapheme_List;
//        public Text progress_Text;
       
//        public List<string> words = new List<string>();
//        public List<GameObject> allButton = new List<GameObject>();
//        public List<AudioClip> AllAudioClipList = new List<AudioClip>();
//        public List<string> selectedWordsList = new List<string>();
//        public List<AudioClip> selectedAudioClipList = new List<AudioClip>();
//        public RectTransform phonic_container, words_container;
//        public GameObject goToSettingsPanelButton;
//        public Sprite selectAll, NotSelectAll, base_Sprite;
//        public Image selectButton;
//        public Text selectAllText;
//        private List<AudioClip> Myaudios = new List<AudioClip>();
//        public int counter;
//        public List<WordGameDataModel> allGameDataModel = new List<WordGameDataModel>();
//        public List<WordGameDataModel> _gameData = new List<WordGameDataModel>();
//        [FormerlySerializedAs("selectedWord")]
//        //public List<WordGameDataModel> AllWordsData = new List<WordGameDataModel>();
//        public List<WordGameDataModel> selectedData = new List<WordGameDataModel>();
//        public WS_GamePlayController gameplayHandler;
//        public GameObject phonic_Panel, words_Panel;
//        public static bool isWordList;
//        private void Awake()
//        {
//            AllGamesCommomData.isBackFromGameplay = false;
//        }
//        private void OnEnable()
//        {
           
//            selectAllWords = false;
//            selectButton.sprite = NotSelectAll;
//            selectAllText.color = Color.white;
//            selectAllWords = true;
//            SelectAllButton(); 
//            if (!AllGamesCommomData.isBackFromGameplay)
//            {
//                if (GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
//                {
//                    phonic_Panel.gameObject.SetActive(true);
//                    CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.APIManager.GetSelectedGraphemeData(GetPhonemeData));
//                }
//                else
//                {
//                    words_Panel.gameObject.SetActive(true);
//                    CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.APIManager.GetSelectedData(initialize));
//                }
//            }

//        }

       
//        void initialize(SelectRoot rt)
//        {
//            words.Clear();
//            if (allButton.Count > 0)
//            {
//                allButton.ForEach(a => Destroy(a.gameObject));
//                allButton.Clear();
//            }
           
//            goToSettingsPanelButton.SetActive(false);
//            selectedData.Clear();
//            selectAllWords = false;
//            _gameData.Clear();
//            gameplayHandler.allGameDataModel.Clear();
//            _gameData.AddRange(allGameDataModel);
//            SpawnRandomWords(rt);        
//            words_container.GetComponent<ContentSizeFitter>().SetLayoutVertical();
//            words_container.anchoredPosition = new Vector2(0, 0);
//            selectButton.sprite = NotSelectAll;
//            selectAllText.color = Color.white;
//            gameplayHandler.selectedWord_Count = 0;
//            gameplayHandler.wordsCatagory = Catagory.SelectWords;

//            CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.GlobalUiManager.AddValueOverTime(1, 3));
//        }
//        void GetPhonemeData(SelecSoundRoot rt)
//        {
//            words.Clear();
//            for (int i = 0; i < phonic_container.childCount; i++)
//            {
//                Destroy(phonic_container.GetChild(i).gameObject);
//            }
//            // loading_Panel.SetActive(false);
//            if (allButton.Count > 0)
//            {
//                allButton.ForEach(a => Destroy(a.gameObject));
//                allButton.Clear();
//            }

//            selectAllWords = false;

//            selectedData.Clear();
//            SpawnRandomSound(rt);

//            phonic_container.anchoredPosition = new Vector2(0, 0);
            
//            CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.GlobalUiManager.AddValueOverTime(1, 3));
//        }
//        public void SpawnRandomWords(SelectRoot rt)
//        {

//           // int tempWord_Count = rt.dataObject.phonicZoomWords.Count;// gameplayHandler.selectWords.Count;          
//            for (int i = 0; i < rt.dataObject.phonicZoomWords.Count; i++)
//            {
//              //  words[i] = rt.dataObject.phonicZoomWords[i].wordName.ToLower();
//                words.Add(rt.dataObject.phonicZoomWords[i].wordName/*.ToLower()*/);
//            }
           
//            List<int> n_List = new List<int>();
//            n_List.Clear();
//            int a = 0;
//            while (a < words.Count)
//            {
//                int randomIndex = Random.Range(0, words.Count);
//                if (!n_List.Contains(randomIndex))
//                {
//                    string name = words[randomIndex];
//                    n_List.Add(randomIndex);

//                    //  AllWordsData.Add(_gameData[randomIndex]);
//                    SpawnWord(name);
//                    // SunScribeClickToAllButtons();
//                    a++;
//                }
//            }
//            progress_Text.text = "0/10"/* + words.Count*/;
//        }
//        public void SpawnRandomSound(SelecSoundRoot rt)
//        {
//            SpawnPhoneme(rt);
//            //words = new String[rt.dataObject.phonemeList.Count];
//            //for (int i = 0; i < rt.dataObject.phonemeList.Count; i++)
//            //{
//            //    words[i] = rt.dataObject.phonemeList[i].name.ToLower();
//            //    SpawnPhoneme(rt, words[i], i);
//            //}
//            //progress_Text.text = "0/" + words.Length;
//        }
//        private void SpawnWord(string word)
//        {

//            GameObject wordObject = Instantiate(WordRef, words_container);

//            wordObject.GetComponentInChildren<Text>().text = word/*.ToLower()*/;

//            if (!wordObject.GetComponent<WS_Word>())
//            {
//                wordObject.AddComponent<WS_Word>();
//            }
//          //  wordObject.GetComponent<WS_Word>().wordClip=audio;

//            allButton.Add(wordObject);


//        }
//        private void SpawnPhoneme(SelecSoundRoot rt/*, string phoneme, int grapheme_Id*/)
//        {


//            for (int a = 0; a < rt.dataObject.phonemeList.Count; a++)
//            {

//                GameObject wordObject = Instantiate(phoneme_Grapheme_List, phonic_container);
//                wordObject.transform.GetChild(0).GetComponent<Text>().text = "Phoneme: " + rt.dataObject.phonemeList[a].name/*.ToLower()*/;

//                RectTransform rect = wordObject.transform.GetChild(1).GetComponent<RectTransform>();
//                for (int i = 0; i < rt.dataObject.phonemeList[a].graphemes.Count; i++)
//                {
//                    // words_Count++;
//                    words.Add(rt.dataObject.phonemeList[a].graphemes[i].graphemeName/*.ToLower()*/);
//                    if (i <= 5)
//                    {
//                        GameObject graphemeObject = Instantiate(WordRef, rect);
//                       // graphemeObject.GetComponentInChildren<Text>().text = rt.dataObject.phonemeList[a].graphemes[i].graphemeName.ToLower();
//                        if (!graphemeObject.GetComponent<WS_Word>())
//                        {
//                            graphemeObject.AddComponent<WS_Word>();
//                        }
//                        graphemeObject.GetComponent<WS_Word>().wordText.text = rt.dataObject.phonemeList[a].graphemes[i].graphemeName/*.ToLower()*/;
//                        graphemeObject.GetComponent<WS_Word>().grapheme_Id = rt.dataObject.phonemeList[a].graphemes[i].id;
//                        allButton.Add(graphemeObject);
//                    }
//                }
//            }
//            progress_Text.text = "0/10"/* + words.Count*/;
//            CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.GlobalUiManager.AddValueOverTime(1, 3));
//        }
//        public void ShuffleWords()
//        {
//            progress_Text.text = "0/10"/* + words.Count*/;
//            GlobalAppController.Instance.APIManager.selectedData.Clear();
//            GlobalAppController.Instance.APIManager.selected_GraphemeIds.Clear();
//            if (allButton.Count > 0)
//            {
//                allButton.ForEach(a => Destroy(a.gameObject));
//                allButton.Clear();
//            }

//            GlobalAppController.Instance.GlobalUiManager.LoadingPanel();
//            if (GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
//            {
//                CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.APIManager.GetSelectedGraphemeData(GetPhonemeData));
//            }
//            else
//            {
//                CoroutineManager.StartStaticCoroutine(GlobalAppController.Instance.APIManager.GetSelectedData(initialize));
//            }

//        }
//        public void onClickShuffle()
//        {
//            Enum.TryParse<AppDataHandler.Categories>("selected",
//              out GlobalAppController.Instance.AppDataHandler.currentCategory);

//            GlobalAppController.Instance.AppDataHandler.limit = "30";
//            GlobalAppController.Instance.GameLoop.OnLoadGameData();
//            if (allButton.Count > 0)
//            {
//                allButton.ForEach(a => Destroy(a.gameObject));
//                allButton.Clear();             
//            }

//            //GlobalAppController.Instance.AppDataHandler.AddEventOnWordDataLoaded(GetLoadedData);
//            //GlobalAppController.Instance.GameLoop.AddOnStartEvent(initialize);

//            // for (int i = 0; i < container.transform.childCount; i++)
//            // {
//            //     Destroy(container.transform.GetChild(i).gameObject);
//            // }

//            // //for (int i = 0; i < allButton.Count; i++)
//            // //{
//            // //    // allButton[i].gameObject.SetActive(false);
//            // //    gameplayHandler.selectWords[i] = "";
//            // //}
//            // allButton.Clear();
//            //// AllWordsData.Clear();
//            // GlobalAppController.Instance.GlobalAudioManager.playClick();
//            // selectedAudioClipList.Clear();
//            // selectedWordsList.Clear();
//            // selectedData.Clear();
//            // selectButton.sprite = NotSelectAll;
//            // selectAllText.color = Color.white;
//            // goToSettingsPanelButton.SetActive(false);
//            //// AllWordsData.Clear();
//            // selectAllWords = false;
//            // gameplayHandler.selectedWord_Count = 0;
//            // //if (counter == 2)
//            // //{
//            // // counter = 0;
//            // words = new string[0];
//            // AllWordsList.Clear();
//            // Myaudios.Clear();
//            // AllWordsData.Clear();
//            //  SpawnRandomWords();
//            //}
//            //else
//            //{
//            // counter++;
//            //var remainingWords = words.Except(AllWordsList);
//            //remainingWords = remainingWords.OrderBy(word => Random.value);
//            //AllWordsData.Clear();
//            //foreach (string word in remainingWords.Take(10))
//            //{
//            //    SpawnWord(word);
//            //    _gameData.ForEach(a =>
//            //    {
//            //        if (word == a.name)
//            //        {
//            //            AllWordsData.Add(a);
//            //        }
//            //    });
//            //}
//            // SunScribeClickToAllButtons();
//            // }
//            // goToSettingsPanelButton.SetActive(false);
//        }
//        public void GoToSettingPanel()
//        {
//            GlobalUiManager.isDataLoaded = true;           
//        }
//        [HideInInspector]
//        public bool checkCounter;
//        //public void CheckOnSelectAllButtManually()
//        //{
//        //    for (int i = 0; i < allButton.Count; i++)
//        //    {
//        //        if (!allButton[i].GetComponent<WS_Word>().isSelected)
//        //        {
//        //            selectButton.sprite = NotSelectAll;
//        //            selectAllText.color = Color.white;
//        //            break;
//        //        }
//        //        else
//        //        {
//        //            selectButton.sprite = selectAll;

//        //            selectAllText.color = Color.green;
//        //        }
//        //    }
//        //}

//        private bool selectAllWords;
//        public void SelectAllButton()
//        {
//            GlobalAppController.Instance.APIManager.selectedData.Clear();
//            GlobalAppController.Instance.APIManager.selected_GraphemeIds.Clear();
//            GlobalAppController.Instance.GlobalAudioManager.playClick();
//            selectedData.Clear();
//            gameplayHandler.allGameDataModel.Clear();
//            if (!selectAllWords)
//            {
//                for (int i = 0; i < allButton.Count; i++)
//                {                
//                    allButton[i].GetComponent<Image>().sprite = UIHandler.Instance.selectedSelectionPanel;
//                }
//                selectButton.sprite = selectAll;
//                selectAllText.color = Color.green;
//                selectAllWords = true;
//                selectedData.AddRange(allGameDataModel);            
//                allButton.ForEach(a => a.GetComponent<WS_Word>().isSelected = true);
//                _gameData.ForEach(a => gameplayHandler.allGameDataModel.Add(a));
//                goToSettingsPanelButton.SetActive(true);
//                foreach (var word in words)
//                {
//                    GlobalAppController.Instance.APIManager.selectedData.Add(word);
//                }
//                foreach (var word in allButton)
//                {
//                    GlobalAppController.Instance.APIManager.selected_GraphemeIds.Add(word.GetComponent<WS_Word>().grapheme_Id);
//                }
//                progress_Text.text = words.Count + "/" + words.Count;
//            }
//            else
//            {
//                for (int i = 0; i < allButton.Count; i++)
//                {
//                    allButton[i].GetComponent<Image>().sprite = UIHandler.Instance.unselectedSelectionPanel;               
//                }
//                allButton.ForEach(a => a.GetComponent<WS_Word>().isSelected = false);
//                selectedWordsList.Clear();
//                selectButton.sprite = NotSelectAll;
//                selectAllText.color = Color.white;
//                selectAllWords = false;
//                selectedData.Clear();            
//                goToSettingsPanelButton.SetActive(false);
//                progress_Text.text = "0/10"/* + words.Count*/;
//            }           
//        }
      
//    }
//}
