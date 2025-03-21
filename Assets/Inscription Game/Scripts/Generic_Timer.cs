
using BiffeProd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generic_Timer : MonoBehaviour
{
    public GameController game_Controller;
    public static float totalTime = 60;
    public static bool isStop = false;
    public Text timeText;

    private void Start()
    {
        isStop=true;
    }
    void Update()
    {
        if (isStop)
        {
            totalTime -= Time.deltaTime;

            if (totalTime <= 0)
            {
                isStop = false;
                game_Controller.GameOver();
               
            }
            else
            {
                updateTimer(totalTime);
            }
        }
    }

    void updateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.RoundToInt(time % 60);
        string formatedSeconds = seconds.ToString();
        timeText.text = /*minutes.ToString("00") +*/ ":" + seconds.ToString("00");
    }
}
