using BiffeProd;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Popup : MonoBehaviour
    {
    
   
    public Text coinsText,powerUp_Txt;
    GameController gm_Controller;
    UIHandler uiHandler;
    private void Start()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
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
        
    }
    public void BackToGamePaly()
    {       
        Destroy(this.gameObject);
        AudioManager.instance.PlaySound(0);
    }
    public void AddPowerUps()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        if (coins >= 300)
        {
            coins -= 300;
            PlayerPrefs.SetInt("COINS", coins);
            coinsText.text = coins.ToString();
            gm_Controller.coins_Text.text = coins.ToString();
            int powerUps = PlayerPrefs.GetInt("HINT_POWERUP");
            powerUps++;
            PlayerPrefs.SetInt("HINT_POWERUP", powerUps);
            gm_Controller.hintButton.GetComponentInChildren<Text>().text= powerUps.ToString();
            GameObject tempObj = Instantiate(gm_Controller.congrats_Popup, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have successfully added a power Up.";
        }
        else 
        {                     
            GameObject tempObj = Instantiate(gm_Controller.coins_Shop, transform.position, Quaternion.identity, gm_Controller. mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;         
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
            coinsText.text = cns.ToString();
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            uiHandler.coins_Text.text = cns.ToString();
            GameObject tempObj = Instantiate(uiHandler.congrats_Popup, transform.position, Quaternion.identity, uiHandler.mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have got a new coins pack.";
        }
        else
        {
            gm_Controller.coins_Text.text = cns.ToString();
            GameObject tempObj = Instantiate(gm_Controller.congrats_Popup, transform.position, Quaternion.identity, gm_Controller.mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            tempObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You have got a new coins pack.";
        }

        AudioManager.instance.PlaySound(0);
    }
}


