////using Base;
//using DG.Tweening;
//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
////using NewAPI;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UIElements;
//using Button = UnityEngine.UI.Button;
////using UnityEngine.UIElements;

//namespace SecretSound
//{
//    public class MenuAnimationHandle : Singleton<MenuAnimationHandle>
//    {
//        //public Sprite buttonBG; 
//        public RectTransform[] panels;
//        public float TransitonTimer;
//        [Header("Menu Panel")]
//        public GameObject MenuPanel;        
//        public GameObject playButton;
//        public GameObject SoundBtn;
//        public List<GameObject> MenubuttonList;
//        [Header("SoundSelectionScreen")]
//        public GameObject SoundSelectionScreenPanel;
//        public List<GameObject> SoundSelectionButtonList;
//        [Header("GameplayScreen")]
//        public GameObject gameplayScreenPanel;
//        public List<GameObject> GameplayButtonList;
//        public List<GameObject> GameplayIconsList;
        
//        [Header("AllButton")]
//        public List<GameObject> buttonClickList;
//        public GameObject fireworks;

//        [Header("AudioSource")]
//        public AudioSource audioSource;
//        private void Start()
//        {
//            foreach (RectTransform panel in panels)
//            {
//                panel.anchoredPosition = new Vector2(Screen.width, 0);

//            }

//         //   MoveInFromRight(MenuPanel, null, null, MenubuttonList, buttonClickList, PlaySoundOnAnimationComplete());
//            // This function call is for debugging of complete panel animation remove it in actual game and uncomment above line.
//            //GameCompleteScreen();
//            // IconPopInSoundSelection();
//        }


//        public void MoveInFromLeft(GameObject comingInPanel, GameObject goingOutPanel, List<GameObject> CurrentScreenBtn,
//            List<GameObject> NextScreenBtn, List<GameObject> allButtons, AudioClip initSound) {

//            comingInPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width, 0);

//            //comingInPanel.transform.localPosition = new Vector3(-1920, 0, 0);
//            comingInPanel.SetActive(true);
//            comingInPanel.GetComponent<RectTransform>().DOAnchorPosX(0, TransitonTimer)
//                 .OnStart(delegate
//                 {

//                     if (NextScreenBtn != null)
//                     {
//                         for (int i = 0; i < NextScreenBtn.Count; i++)
//                         {
//                             NextScreenBtn[i].SetActive(false);
//                         }
//                     }

//                     if (allButtons != null)
//                     {
//                         for (int i = 0; i < allButtons.Count; i++)
//                         {
//                             allButtons[i].GetComponent<Button>().interactable = false;
//                         }
//                     }


//                     if (goingOutPanel != null)
//                     {
//                         //goingOutPanel.transform.localPosition = new Vector3(0, 0, 0);
//                         goingOutPanel.GetComponent<RectTransform>().DOAnchorPosX(Screen.width, TransitonTimer)
//                         .OnComplete(delegate
//                         {
//                             goingOutPanel.SetActive(false);
//                         }).OnStart(delegate
//                         {

//                             if (CurrentScreenBtn != null)
//                             {
//                                 for (int i = 0; i < CurrentScreenBtn.Count; i++)
//                                 {
//                                     CurrentScreenBtn[i].GetComponent<Animator>().SetTrigger("OnDisAppear");
//                                 }
//                             }
//                         });
//                     }
//                 })
//                .OnComplete(delegate
//                {
                    
//                        if (NextScreenBtn != null)
//                        {
//                            for (int i = 0; i < NextScreenBtn.Count; i++)
//                            {
//                                NextScreenBtn[i].SetActive(true);
//                                NextScreenBtn[i].GetComponent<Animator>().SetTrigger("OnAppear");
//                            }
//                        }
//                        if (initSound != null)
//                        {
//                            GlobalAppController.Instance.GlobalAudioManager.playWordSound(initSound);
//                        }
//                        if (allButtons != null)
//                        {
//                            for (int i = 0; i < allButtons.Count; i++)
//                            {
//                                allButtons[i].GetComponent<Button>().interactable = true;
//                            }
//                        }

//                        if (SoundSelectionScreenPanel.activeInHierarchy)
//                        { IconPopInSoundSelection(0); }


                    

//                });
               

//        }
//        public void MoveInFromRight(GameObject comingInPanel, GameObject goingOutPanel, List<GameObject> CurrentScreenBtn,
//            List<GameObject> NextScreenBtn,List<GameObject> allButtons,AudioClip initSound) {

//            comingInPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width, 0);
//            // comingInPanel.transform.localPosition = new Vector3(1920, 0, 0);
//            comingInPanel.SetActive(true);
//            comingInPanel.GetComponent<RectTransform>().DOAnchorPosX(0, TransitonTimer)
//                .OnStart(delegate {
//                    if (allButtons != null)
//                    {
//                        for (int i = 0; i < allButtons.Count; i++)
//                        {
//                            allButtons[i].GetComponent<Button>().interactable = false;
//                        }
//                    }
//                    if (NextScreenBtn != null)
//                    {

//                        for (int i = 0; i < NextScreenBtn.Count; i++)
//                        {
//                            NextScreenBtn[i].SetActive(false);
//                        }
//                    }
//                    if (goingOutPanel != null)
//                    {
//                        // goingOutPanel.transform.localPosition = new Vector3(0, 0, 0);
//                        goingOutPanel.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width, TransitonTimer).OnComplete(delegate {
//                            goingOutPanel.SetActive(false);

//                        }).OnStart(delegate {

//                            if (CurrentScreenBtn != null)
//                            {
//                                for (int i = 0; i < CurrentScreenBtn.Count; i++)
//                                {
//                                    CurrentScreenBtn[i].GetComponent<Animator>().SetTrigger("OnDisAppear");
//                                }

//                            }
//                        });
//                    }




//                })
//                .OnComplete(delegate {
                    
//                        if (initSound != null)
//                        {
//                            GlobalAppController.Instance.GlobalAudioManager.playWordSound(initSound);
//                        }
//                        if (allButtons != null)
//                        {
//                            for (int i = 0; i < allButtons.Count; i++)
//                            {
//                                allButtons[i].GetComponent<Button>().interactable = true;
//                            }
//                        }
//                        if (NextScreenBtn != null)
//                        {

//                            for (int i = 0; i < NextScreenBtn.Count; i++)
//                            {
//                                NextScreenBtn[i].SetActive(true);
//                                NextScreenBtn[i].GetComponent<Animator>().SetTrigger("OnAppear");
//                            }
//                        }

//                        //SecretSound-SoundSelectionScreen
//                        if (SoundSelectionScreenPanel.activeInHierarchy)
//                        { IconPopInSoundSelection(0); }
//                        if (GameCompletePanel.activeInHierarchy)
//                        {
//                            //Debug.Log("Opening");
//                            GameCompleteScreen();
//                        }
                  



//                });
                

//        }

//        /// <summary>
//        /// Secret Sound Initial Sounds
//        /// </summary>
//        public AudioClip PlaySoundOnAnimationComplete()
//        {
//            Invoke("AnimateSound",1.5f);
//           // Generic_audioManager.Instance.playOneTime(Generic_audioManager.Instance.ss_Title);
//           GlobalAppController.Instance.GlobalAudioManager.playTitle();
//           return GlobalAppController.Instance.GlobalAudioManager.currentGamesAudio.IntroDuction; //AudioManager.Instance.InitAudioClip();

//        }
//        void AnimateSound()
//        {
//            SoundBtn.GetComponent<Animator>().SetTrigger("PlaySound");
//        }

//        /// <summary>
//        /// Sound Selection In SecretSound
//        /// </summary>
//        public List<GameObject> soundsSelectionList;
//        public void IconPopInSoundSelection(int i)
//        {
//            ////AudioManager.Instance.PlayPopSound();
//            //soundsSelectionList[i].transform.DOPunchScale(new Vector3(.1f, .1f, .1f), .5f, 10, 1).OnComplete(delegate{
//            //    i += 1;
//            //if(i<6)
//            //    {
//            //        IconPopInSoundSelection(i);
//            //    }
//            //});
//        }

//        public void ResetSoundBtnsScale()
//        {
//            foreach(GameObject btn in SoundSelectionButtonList)
//            {
//                btn.transform.localScale = new Vector3(1, 1, 1);
//            }
//        }

//        /// <summary>
//        /// Game Complete Screen Animation And Functionality
//        /// </summary>
//        /// 
//        public GameObject GameCompletePanel;

//        IEnumerator AfterFireworks(float _delay)
//        {
//            yield return new WaitForSeconds(_delay);
//            fireworks.SetActive(true);
//            yield return new WaitForSeconds(0.3f);
//            CompleteGamePanelAnimation.Instance.correct.SetActive(true);
//            //CompleteGamePanelAnimation.Instance.correct.transform.DOMoveX(2500, 0.5f, true).From()/*.SetEase(Ease.OutBack)*/;
//            //CompleteGamePanelAnimation.Instance.correct.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-160.9f, -45.1f), 0.5f);
//            CompleteGamePanelAnimation.Instance.wrong.SetActive(true);
//            //CompleteGamePanelAnimation.Instance.wrong.GetComponent<RectTransform>().DOAnchorPos(new Vector2(161.1f, -45.1f), 0.5f);
//            //CompleteGamePanelAnimation.Instance.wrong.transform.DOMoveX(3043, 0.5f, true).From()/*.SetEase(Ease.OutBack)*/;
//            CompleteGamePanelAnimation.Instance.total.SetActive(true);
//            //CompleteGamePanelAnimation.Instance.trophyBox.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-530, -88),0.5f);
//            //CompleteGamePanelAnimation.Instance.starBox.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-205, -89),0.5f);
//            //CompleteGamePanelAnimation.Instance.trophyBox.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-468.6f, -78.2f), 0.5f);
//            ///CompleteGamePanelAnimation.Instance.starBox.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-178, -78.2f), 0.5f);
//            CompleteGamePanelAnimation.Instance.total.transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(3, -231.8f), 0.5f)/*.SetEase(Ease.OutBack)*/.OnComplete(delegate
//            {
//                CountingAnimation();
//            });
//            EnableLastTwoButton();
//            CompleteGamePanelAnimation.Instance.Exitbuttons[0].transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(249.3f, 128), 0.5f);
//            CompleteGamePanelAnimation.Instance.Exitbuttons[1].transform.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-248.6f, 128), 0.5f);
//        }
//        public void GameCompleteScreen()
//        {
//            //Debug.Log("Open");
//            CompleteGamePanelAnimation.Instance.alien.SetActive(true);
//            CompleteGamePanelAnimation.Instance.rays.SetActive(true);
//            CompleteGamePanelAnimation.Instance.rays.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
//            CompleteGamePanelAnimation.Instance.badge.SetActive(true);
//            CompleteGamePanelAnimation.Instance.badge.transform.DOScale(new Vector3(1, 1, 1), 0.3f).OnComplete(delegate {

//            CompleteGamePanelAnimation.Instance.middleStar.SetActive(true);
//            StartCoroutine(AfterFireworks(0.3f));
              
//            });
            


         

//        }
//        //public void CountingAnimation()
//        //{

//        //}

//        public void EnableLastTwoButton()
//        {
//            for (int  i = 0;  i < CompleteGamePanelAnimation.Instance.Exitbuttons.Count;  i++)
//            {
//                CompleteGamePanelAnimation.Instance.Exitbuttons[i].SetActive(true);
//            }
//            ResetScale();
//        }

//        int numberOfTrophies;
//        int numberOfStar;

//        public void CountingAnimation()
//        {
//            if (SecretSoundEconomy.Instance.totalTrophyPoints > 0)
//            {
//                DeleteablePrefs(SecretSoundEconomy.Instance.totalStarPoints, SecretSoundEconomy.Instance.totalTrophyPoints);
//                numberOfTrophies = SecretSoundEconomy.Instance.totalTrophyPoints / 10;
//                numberOfStar = SecretSoundEconomy.Instance.totalStarPoints / 3;
//                StartCoroutine(GenerateAnimObjects());
//            }
//            //else
//            //{
//            //    EnableLastTwoButton();
//            //}
//        }

//        public GameObject trophy, star;

//        public GameObject MainStar, MainTrophy;
//        IEnumerator GenerateAnimObjects()
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                yield return new WaitForSeconds(0.2f);
//                SecretSoundEconomy.Instance.totalTrophyPoints -= numberOfTrophies;
//                SecretSoundEconomy.Instance.totalStarPoints -= numberOfStar;
//                if (SecretSoundEconomy.Instance.totalStarPoints - numberOfStar < 0)
//                {
//                    SecretSoundEconomy.Instance.totalStarPoints = 0;
//                }
//                if (SecretSoundEconomy.Instance.totalTrophyPoints - numberOfTrophies < 0)
//                {
//                    SecretSoundEconomy.Instance.totalTrophyPoints = 0;
//                }
//                EndGameScreen.Instance.ReducingPointsOnAnim();
//                Instantiate(trophy, GameCompletePanel.transform);
//                Instantiate(star, GameCompletePanel.transform);

//            }
//            SecretSoundEconomy.Instance.totalTrophyPoints = 0;
//            SecretSoundEconomy.Instance.totalStarPoints = 0;
//            EndGameScreen.Instance.ReducingPointsOnAnim();
//            ScreenHandlers.Instance.gameCompleteScreenObj.FinalScore();
//            //EnableLastTwoButton();
//        }

//        IEnumerator ResetScale()
//        {
//            yield return new WaitForSeconds(2);
//            MainStar.transform.DOScale(1, 1);// = Vector3.one; //(1, 1);
//            MainTrophy.transform.DOScale(1, 1);
//        }


//        public void DeleteablePrefs(int stars,int points)
//        {
//            PlayerPrefs.SetInt("StarsInThisGame", stars);
//            PlayerPrefs.SetInt("PointsInThisGame", points);

//        }


//    }
//}