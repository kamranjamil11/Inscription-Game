
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class UIHandler : MonoBehaviour
    {
    public GameObject loadingScreen;
        public Text gameHighScoreText;
        public BoardManager boardManager;
        public LineManager lineManager;
        public GameObject IntroPanel;
        public GameObject DifficultyPanel;
        public GameObject SelectionPanel;
        public GameObject SettingsPanel;
       
        public GameObject EndingScreenPanel;
        public GameObject grid_Panel;
        public Sprite unselectedSelectionPanel, selectedSelectionPanel;


        private void Start()
        {
            

        }
       


        public void PlayButton()
        {

        loadingScreen.SetActive(true);


        }
        public void BackFromIntroButton()
        {


        }
        public void BackToIntroPanel()
        {

        }

        public void GoToSelectionPanel()
        {

        }

        public void GoBackToDifficulty()
        {


        }
        public static bool isRandom;
       
        
       
        

       
        public void toMainmenu()
        {
          
        }
        
    }
