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
        //CheckOrientation();
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

    }



    //void Update()
    //{
    //    if (IsPortrait() != isPortrait)
    //    {
    //        CheckOrientation();
    //    }
    //}

    //bool IsPortrait()
    //{
    //    return Screen.height >= Screen.width;
    //}

    //public void CheckOrientation() 
    //{
    //    isPortrait = IsPortrait();
    //    ApplyOrientation(isPortrait);
    //}
    //void ApplyOrientation(bool portrait)
    //{
    //    if (SceneManager.GetActiveScene().name == "LoadingScene") 
    //    {
    //        ls = FindObjectOfType<LoadingScreen>();
    //        if (portrait)
    //        {
    //            ls.loadingScreen.SetActive(false);
    //            ls.loadingScreen_Portrait.SetActive(true);
    //            Debug.Log("Portrait mode detected");
    //            // status_Text.text = "Switched to Portrait: " + a;
    //            // TODO: Show portrait UI, adjust layout, etc.
    //        }
    //        else
    //        {
    //            ls.loadingScreen.SetActive(true);
    //            ls.loadingScreen_Portrait.SetActive(false);
    //            Debug.Log("Landscape mode detected");
    //            //status_Text.text = "Switched to Landscape: " + a;
    //            // TODO: Show landscape UI, adjust layout, etc.
    //        }
    //    }
    //    else if(SceneManager.GetActiveScene().name == "MainMenu")
    //    {
    //         ui_Handler = FindObjectOfType<UIHandler>();
    //        if (portrait)
    //        {
    //            ui_Handler.landscape_UI.SetActive(false);
    //            ui_Handler.portrait_UI.SetActive(true);               
    //        }
    //        else
    //        {
    //            ui_Handler.landscape_UI.SetActive(true);
    //            ui_Handler.portrait_UI.SetActive(false);
    //        }
    //    }
    //    else if(SceneManager.GetActiveScene().name == "Game")
    //    {
    //         gm_Controller = FindObjectOfType<GameController>();
    //        if (portrait)
    //        {
    //            // ls.loadingScreen.SetActive(false);
    //            // ls.loadingScreen_Portrait.SetActive(true);
    //            gm_Controller.gridFrame.transform.localScale = new Vector2(1.8f,1.8f);
    //            Debug.Log("Portrait mode detected");

    //        }
    //        else
    //        {
    //            // ls.loadingScreen.SetActive(true);
    //            // ls.loadingScreen_Portrait.SetActive(false);
    //            gm_Controller.gridFrame.transform.localScale = Vector2.one;
    //            Debug.Log("Landscape mode detected");

    //        }
    //    }

    //}


}
