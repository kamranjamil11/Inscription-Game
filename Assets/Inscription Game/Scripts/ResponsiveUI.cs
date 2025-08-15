using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResponsiveUI : MonoBehaviour
{
    private bool isPortrait;
    
    public static ResponsiveUI instance;

    LoadingScreen ls;
    UIHandler ui_Handler;
    GameController gm_Controller;
    public GameObject landscape_UI, portrait_UI;
   void Start()
    {
        if (!PlayerPrefs.HasKey("ISUSER_ENTER"))
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else
        {
            ChangeOrientation();
        }
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



}
