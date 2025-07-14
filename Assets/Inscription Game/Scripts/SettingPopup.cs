using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using UnityEngine.UIElements;

public class SettingPopup : MonoBehaviour
    {
    public static bool isPortrait;
    public Slider musicValue;
    public Slider soundValue;
    public Text direction_Txt;
   
    AudioManager audioManager;
    GameController gameController;
    UIHandler ui_Handler;
    
    private void Start()
    {
        audioManager=GameObject.FindObjectOfType<AudioManager>();
        if (SceneManager.GetActiveScene().name == "MainMenu") 
        {
            ui_Handler = FindObjectOfType<UIHandler>();
        }
        else
        {
            gameController = FindObjectOfType<GameController>();
        }
        
        GetMusicAndSoundValue();
        
    }
    public void CancelSetting()
    {
        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
        AudioManager.instance.PlaySound(0);
    }
    
    public void MusicSlider()
    {
        audioManager.musicSource.volume = musicValue.value;
        PlayerPrefs.SetFloat("MUSIC", musicValue.value);
    }
    public void SoundSlider()
    {
        audioManager.SoundSource.volume = soundValue.value;
        PlayerPrefs.SetFloat("SOUND", soundValue.value);
    }
    public void GetMusicAndSoundValue()
    {     
        if (!PlayerPrefs.HasKey("MUSIC"))
        {
            musicValue.value = 1;
        }
        else
        {
            musicValue.value = PlayerPrefs.GetFloat("MUSIC");
        }
        if (!PlayerPrefs.HasKey("SOUND"))
        {
            soundValue.value = 1;
        }
        else
        {
             soundValue.value = PlayerPrefs.GetFloat("SOUND");
        }
    }

    public void SwitchDirection()
    {
        if (!isPortrait)
        {
            isPortrait=true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
           
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                ui_Handler.landscape_UI.SetActive(true);
                ui_Handler.portrait_UI.SetActive(false);
                GameObject tempSetting = Instantiate(ui_Handler.settingsPopup, transform.position, Quaternion.identity, ui_Handler.mainCanvas.transform);   

                tempSetting.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                Vector3 pos = tempSetting.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                tempSetting.GetComponent<RectTransform>().localPosition = pos;
            }
            else
            {
                gameController.portrait_Objs.SetActive(false);
                gameController.landscape_Objs.SetActive(true);
                gameController.gridFrame.transform.localScale = new Vector2(1f, 1f);
                GameObject tempSetting = Instantiate(gameController.settingsPopup, transform.position, Quaternion.identity, gameController.mainCanvas.transform);
                tempSetting.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                Vector3 pos = tempSetting.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                tempSetting.GetComponent<RectTransform>().localPosition = pos;

                gameController.gridFrame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                Vector3 pos1 = gameController.gridFrame.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                gameController.gridFrame.GetComponent<RectTransform>().localPosition = pos1;
                gameController.header.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,-106f);
                AdManager.Instance.HideBanner();
            }

        
            Debug.Log("Landscape mode detected");
        }
        else
        {
            isPortrait = false;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;
          //  direction_Txt.text = "ON";
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                ui_Handler.landscape_UI.SetActive(false);
                ui_Handler.portrait_UI.SetActive(true);
               GameObject tempSetting= Instantiate(ui_Handler.settingsPopup_Portrait, transform.position, Quaternion.identity, ui_Handler.mainCanvas.transform);
               
                tempSetting.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                Vector3 pos = tempSetting.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                tempSetting.GetComponent<RectTransform>().localPosition = pos;
            }
            else
            {
                gameController.portrait_Objs.SetActive(true);
                gameController.landscape_Objs.SetActive(false);
                gameController.gridFrame.transform.localScale = new Vector2(1.8f, 2f);
                GameObject tempSetting = Instantiate(gameController.settingsPopup_Portrait, transform.position, Quaternion.identity, gameController.mainCanvas.transform);
                tempSetting.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                Vector3 pos = tempSetting.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                tempSetting.GetComponent<RectTransform>().localPosition = pos;

                gameController.gridFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -265);
                Vector3 pos1 = gameController.gridFrame.GetComponent<RectTransform>().anchoredPosition;
                pos.z = 0;
                gameController.gridFrame.GetComponent<RectTransform>().localPosition = pos1;
                gameController.header.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,9.5f);
                AdManager.Instance.ShowBanner();
            }
            Debug.Log("Portrait mode detected");
        }
       
        CancelSetting();
        Time.timeScale = 0f;
    }

    public void OnRestore() 
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") 
        {
          //  ui_Handler.RemoveAdsCompleted();
          ui_Handler.IAP_Restore.Restore();
        }
    }
}


