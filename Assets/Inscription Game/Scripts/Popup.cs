using BiffeProd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Popup : MonoBehaviour
    {


    public Text coinsText;
    private void Start()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }
    public void BackToGamePaly()
    {       
        Destroy(this.gameObject);        
    }
    public void AddPowerUps()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        if (coins >= 2000)
        {
            coins -= 2000;
            PlayerPrefs.SetInt("COINS", coins);
            coinsText.text = coins.ToString();
        }
        else 
        {
            GameController gm_Controller=GameObject.FindObjectOfType<GameController>();
           // gm_Controller.notEnoughCoinsPanel.SetActive(true);
            GameObject tempObj = Instantiate(gm_Controller.notEnoughCoinsPanel, transform.position, Quaternion.identity, gm_Controller. mainCanvas.transform);
            tempObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}


