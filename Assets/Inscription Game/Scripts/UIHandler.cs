
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class UIHandler : MonoBehaviour
    {
    public GameObject loadingScreen;
        public Text gameHighScoreText;
       // public BoardManager boardManager;
       // public LineManager lineManager;
       // public GameObject IntroPanel;
       // public GameObject DifficultyPanel;
        public GameObject mainCanvas;
        public GameObject settingsPopup;
       
       // public GameObject EndingScreenPanel;
        //public GameObject grid_Panel;
       // public Sprite unselectedSelectionPanel, selectedSelectionPanel;


        private void Start()
        {
            

        }



    public void PlayButton()
    {

        loadingScreen.SetActive(true);

    }
    public void SettingBtn()
    {
        Instantiate(settingsPopup, transform.position,Quaternion.identity, mainCanvas.transform);
        Time.timeScale = 0f;
    }
    //public void CancelSetting()
    //{
    //    if (tempObj != null)
    //    {
    //        Destroy(tempObj);
    //    }
    //}

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
