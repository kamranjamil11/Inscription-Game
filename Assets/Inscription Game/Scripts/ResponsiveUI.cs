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
   void Start()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.Portrait;
        
    }

    

    
}
