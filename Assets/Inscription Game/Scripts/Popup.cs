using BiffeProd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Popup : MonoBehaviour
{
   
    bool isArrow;
    public Text coinsText, powerUp_Txt;
    GameController gm_Controller;
    UIHandler uiHandler;
    public GameObject coinsTilte, powerUpTilte, coinsShop, powerShop,loading_Panel;
    GameObject tempCoinsShop = null;
    private void Start()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        if (coinsText != null)
        {
            coinsText.text = UIHandler.FormatNumber(coins);
        }
        int powerUps = PlayerPrefs.GetInt("HINT_POWERUP");
        if (powerUp_Txt != null)
        {
            powerUp_Txt.text = powerUps.ToString();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            uiHandler = GameObject.FindObjectOfType<UIHandler>();
        }
        else
        {
            gm_Controller = GameObject.FindObjectOfType<GameController>();
        }
        AdManager.Instance.HideBanner();
    }
    public void BackToGamePaly()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlaySound(0);
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            AdManager.Instance.ShowBanner();
        }
        Destroy(this.gameObject);
       
    }
    public void BackToMainScreen()
    {      
        AudioManager.instance.PlaySound(0);
        Destroy(this.gameObject);
    }
    public void AddPowerUps(string key)
    {
        int coins = PlayerPrefs.GetInt("COINS");
        Button powerButton = null;
        int requir_Coins=0;
        if (key == "SCRAB_POWERUP")
        {
            requir_Coins = 200;
            if (SceneManager.GetActiveScene().name != "MainMenu")
                powerButton = gm_Controller.scrabButton;
        }
        else if (key == "HINT_POWERUP") 
        {
            requir_Coins = 300; 
            if (SceneManager.GetActiveScene().name != "MainMenu")
                powerButton = gm_Controller.hintButton;
        }
        else if (key == "LOTUS_POWERUP")
        {
            requir_Coins = 400;
            if (SceneManager.GetActiveScene().name != "MainMenu")
                powerButton = gm_Controller.lotusButton;
        }
        if (coins >= requir_Coins)
        {
            coins -= requir_Coins;
            PlayerPrefs.SetInt("COINS", coins);
            coinsText.text = UIHandler.FormatNumber(coins);

            int powerUps = PlayerPrefs.GetInt(key);
            powerUps++;
            PlayerPrefs.SetInt(key, powerUps);
            
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                uiHandler.coins_Text.text =UIHandler.FormatNumber(coins);
                uiHandler.coins_Text_Portrait.text = UIHandler.FormatNumber(coins);
                GameObject tempObj = null;
                if (!SettingPopup.isPortrait)
                {
                    tempObj = Instantiate(uiHandler.congrats_Popup_Portrait, transform.position, Quaternion.identity, uiHandler.mainCanvas.transform);
                }
                else
                {
                    tempObj = Instantiate(uiHandler.congrats_Popup, transform.position, Quaternion.identity, uiHandler.mainCanvas.transform);
                }
                tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have successfully added a 1 power Up.";
            }
            else
            {
                gm_Controller.coins_Text.text = UIHandler.FormatNumber(coins);
                gm_Controller.coins_Text_Portrait.text = UIHandler.FormatNumber(coins);

                powerButton.GetComponentInChildren<Text>().text = powerUps.ToString();
                GameObject tempObj = null;
                if (!SettingPopup.isPortrait)
                {
                    tempObj = Instantiate(gm_Controller.congrats_Popup_Portrait, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
                }
                else
                {
                    tempObj = Instantiate(gm_Controller.congrats_Popup, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
                }
               // GameObject tempObj = Instantiate(gm_Controller.congrats_Popup, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
                tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have successfully added a 1 power Up Pack.";
            }
            AudioManager.instance.PlaySound(6);
            FirebaseData.instance.DataSaveFun();
        }
        else
        {
            LeftAndRightClick();           
        }
        AudioManager.instance.PlaySound(0);
       
    }
    public void AddCoins(int coins)
    {
        int cns = PlayerPrefs.GetInt("COINS");
        cns += coins;
        PlayerPrefs.SetInt("COINS", cns);
        if (coinsText != null)
        {
            coinsText.text = UIHandler.FormatNumber(cns);
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            uiHandler.coins_Text.text = UIHandler.FormatNumber(cns);
            uiHandler.coins_Text_Portrait.text = UIHandler.FormatNumber(cns);
            GameObject tempObj = null;
            if (!SettingPopup.isPortrait)
            {
                tempObj = Instantiate(uiHandler.congrats_Popup_Portrait, transform.position, Quaternion.identity, uiHandler.mainCanvas.transform);
            }
            else
            {
                tempObj = Instantiate(uiHandler.congrats_Popup, transform.position, Quaternion.identity, uiHandler.mainCanvas.transform);
            }
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have got a "+ coins + " new coins pack.";
        }
        else
        {
            gm_Controller.coins_Text.text = UIHandler.FormatNumber(cns); 
            gm_Controller.coins_Text_Portrait.text = UIHandler.FormatNumber(cns);

            GameObject tempObj = null;
            if (!SettingPopup.isPortrait)
            {
                tempObj = Instantiate(gm_Controller.congrats_Popup_Portrait, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
            }
            else
            {
                tempObj = Instantiate(gm_Controller.congrats_Popup, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
            }
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have got a " + coins + " new coins pack.";
        }
        loading_Panel.SetActive(false); 
        AudioManager.instance.PlaySound(6);
        FirebaseData.instance.DataSaveFun();
    }
    public void LeftAndRightClick()
    {
        if (isArrow)
        {
            isArrow = false;
            coinsTilte.SetActive(true);
            powerUpTilte.SetActive(false);
            coinsShop.SetActive(true);
            powerShop.SetActive(false);
        }
        else
        {
            isArrow = true;
            coinsTilte.SetActive(false);
            powerUpTilte.SetActive(true);
            coinsShop.SetActive(false);
            powerShop.SetActive(true);
        }
        AudioManager.instance.PlaySound(0);
    }
    public void loadingActive()
    {
        loading_Panel.SetActive(true);
    }
    public void Continue()
    {
        PlayerPrefs.DeleteAll();
        AudioManager.instance.PlaySound(0);
        SceneManager.LoadScene("LoadingScene");
    }
}


