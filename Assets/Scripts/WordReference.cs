using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WordSearch;


public class WordReference : MonoBehaviour
{
    public WS_GamePlayController gamePlayController;    
    public List<TextMeshProUGUI> textList;
    public AudioSource  audio_Play;
   // public List<GameObject> audio_btn;

    public void Populate(Words wordlist)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            HintBehavior hintBehavior = textList[i].EnsureComponent<HintBehavior>();
            textList[i].fontStyle = FontStyles.Normal;
            textList[i].faceColor = Color.white;
            if (i < wordlist.words.Length)
            {
                textList[i].text = wordlist.words[i].word;
              
                hintBehavior.word = wordlist.words[i];
                
            }
            else
            {
                textList[i].text = string.Empty;
            }
        }
    }

     void GetTextRef() 
    {
        for (int i = 0; i < gamePlayController.container.transform.childCount; i++)
        {
            textList[i] =gamePlayController.container.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }
    public void Populate1(string word, List<AudioClip> audioList)
    {
        GetTextRef();
       // for (int i = 0; i < textList.Count; i++)
       // {
           // HintBehavior hintBehavior = textList[i].EnsureComponent<HintBehavior>();
            textList[0].fontStyle = FontStyles.Normal;
            textList[0].faceColor = Color.white;
           // if (i < wordList.Count)
           // {
                textList[0].text = word;
              //  audio_btn[i].GetComponent<AudioSource>().clip = audioList[i];
                //AudioClip audio = audioList[i];
                //textList[i].transform.parent.GetComponent<Button>().onClick.RemoveAllListeners();
                //textList[i].transform.parent.GetComponent<Button>().onClick.AddListener(() => PlaySound(audio));
        //        // hintBehavior.word = wordlist[i];
        //    }
        //    else
        //    {
        //        textList[i].text = string.Empty;
        //    }
        //}
      //  gamePlayController.back_Btn.SetActive(true);
    }
    public void Strike(string word)
    {
        foreach (TextMeshProUGUI item in textList)
        {
            if (item.text.ToLower() == word.ToLower())
            {
                item.fontStyle = FontStyles.Strikethrough;
                item.faceColor = Color.gray;
            }
        }
    }
    public void PlaySound(AudioClip audio)
    {
        Debug.Log("Play_Sound");
      //  audio_Play.clip= audio;
        //audio_Play.Play();
       // GlobalAppController.Instance.GlobalAudioManager.playWordSound(audio);
    }
    private void AlertHintClick(string word)
    {
        Debug.Log($"TEST: {word}");
    }
}
