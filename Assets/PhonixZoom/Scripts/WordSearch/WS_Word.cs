
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//namespace WordSearch
//{

//    public class WS_Word : MonoBehaviour
//    {
//        public int grapheme_Id;
//        public Text wordText;
//        private Button button;
//        public Image spriteOfWord;
//        [HideInInspector]
//        public bool isSelected;
//        public WS_WordList words;
//        private void Start()
//        {
//            isSelected = false;
//            GetComponent<Button>().onClick.RemoveAllListeners();
//            GetComponent<Button>().onClick.AddListener(OnCheck);
//            words= FindObjectOfType<WS_WordList>();
//        }

//        public void OnCheck()
//        {
//            if (!isSelected)
//            {
//                isSelected = true;
//                if (GlobalAppController.Instance.AppDataHandler.currentGameType == Game_type.phoneme)
//                {
//                    OnClickSoundBtn(true);
//                }
//                else
//                {
//                    OnClick(true);
//                }
//                GetComponent<Image>().sprite = UIHandler.Instance.selectedSelectionPanel;

//            }
//            else
//            {
//                isSelected = false;
//                if (GlobalAppController.Instance.AppDataHandler.currentGameType == Game_type.phoneme)
//                {
//                    OnClickSoundBtn(false);
//                }
//                else
//                {
//                    OnClick(false);
//                }
//                GetComponent<Image>().sprite = UIHandler.Instance.unselectedSelectionPanel;
//            }
//        }
//        void OnClick(bool isTrue)
//        {
//           // WS_WordList words = FindObjectOfType<WS_WordList>();
//            SelectRoot rt = GlobalAppController.Instance.APIManager.select_RT;
//            for (int i = 0; i < rt.dataObject.phonicZoomWords.Count; i++)
//            {
//                if (wordText.text == rt.dataObject.phonicZoomWords[i].wordName/*.ToLower()*/)
//                {
//                    if (isTrue)
//                    {
//                        // words.gameplayHandler.allGameDataModel.Add(words._gameData[i]);
//                        GlobalAppController.Instance.APIManager.selectedData.Add(wordText.text);
//                    }
//                    else
//                    {
//                        // words.gameplayHandler.allGameDataModel.Remove(words._gameData[i]);
//                        GlobalAppController.Instance.APIManager.selectedData.Remove(wordText.text);
//                    }
//                    break;
//                }
//            }
//            if (GlobalAppController.Instance.APIManager.selectedData.Count >= 10)
//            {
//                  words.progress_Text.text = GlobalAppController.Instance.APIManager.selectedData.Count + "/" + GlobalAppController.Instance.APIManager.selectedData.Count;
//                words.goToSettingsPanelButton.SetActive(true);
//            }
//            else
//            {
//                words.goToSettingsPanelButton.SetActive(false);
//                words.progress_Text.text = GlobalAppController.Instance.APIManager.selectedData.Count + "/10";
//            }

//           // words.progress_Text.text = GlobalAppController.Instance.APIManager.selectedData.Count + "/" + words.words.Count;
//        }
//        void OnClickSoundBtn(bool isTrue)
//        {
           
//            if (isTrue)
//            {
//                GlobalAppController.Instance.APIManager.selectedData.Add(wordText.text);
//                GlobalAppController.Instance.APIManager.selected_GraphemeIds.Add(grapheme_Id);
//            }
//            else
//            {
//                GlobalAppController.Instance.APIManager.selectedData.Remove(wordText.text);
//                GlobalAppController.Instance.APIManager.selected_GraphemeIds.Remove(grapheme_Id);
//            }
//            if (GlobalAppController.Instance.APIManager.selectedData.Count >= 10)
//            {
//                words.progress_Text.text = GlobalAppController.Instance.APIManager.selectedData.Count + "/" + GlobalAppController.Instance.APIManager.selectedData.Count;
//                words.goToSettingsPanelButton.SetActive(true);
//            }
//            else
//            {
//                words.goToSettingsPanelButton.SetActive(false);
//            words.progress_Text.text = GlobalAppController.Instance.APIManager.selectedData.Count + "/10"/* + words.words.Count*/;

//            }
//        }
//    }
//}