using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using UnityEngine.UIElements;

public class SettingPopup : MonoBehaviour
    {
    
    public Slider musicValue;
    public Slider soundValue;
    
    public GameObject right_Toggle, left_Toggle;
    private bool isToggle;
    //public AudioSource musicSource;
    // public AudioSource SoundSource;

    AudioManager audioManager;
    GameController gameController;
    private void Start()
    {
        audioManager=GameObject.FindObjectOfType<AudioManager>();
        gameController= GameObject.FindObjectOfType<GameController>();
        if (gameController.isToggle)
        {
            right_Toggle.SetActive(false);
            left_Toggle.SetActive(true);
        }
        else
        {
            right_Toggle.SetActive(true);
            left_Toggle.SetActive(false);
        }
        GetMusicAndSoundValue();
    }
    public void CancelSetting()
    {
        Time.timeScale = 1.0f;
        Destroy(this.gameObject);
        
    }
    //private void changeMusicState()
    //    {
    //        if (PlayerPrefs.GetInt("music") == 0)
    //        {
    //            music.enabled = true;
    //        }
    //        else
    //        {
    //            music.enabled = false;
    //        }
    //    }
    //    //private void Update()
    //    //{
    //    //    sounds = GameObject.FindGameObjectsWithTag("Sound");
    //    //    changeSoundState();
    //    //    changeMusicState();
    //    //}
    //private void changeSoundState()
    //    {
    //        if (PlayerPrefs.GetInt("sound") == 0)
    //        {
    //            foreach (GameObject Sound in sounds)
    //            {
    //                if (!Sound.GetComponent<AudioSource>().enabled)
    //                {
    //                    Sound.GetComponent<AudioSource>().enabled = true;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            foreach (GameObject Sound in sounds)
    //            {
    //                if (Sound.GetComponent<AudioSource>().enabled)
    //                {
    //                    Sound.GetComponent<AudioSource>().enabled = false;
    //                }
    //            }
    //        }
    //    }
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

    public void ToggleBtn()
    {
       
        if (gameController.isToggle)
        {          
            right_Toggle.SetActive(true);          
            left_Toggle.SetActive(false);         
        }
        else
        {          
            right_Toggle.SetActive(false);          
            left_Toggle.SetActive(true);          
        }
        gameController.ToggleBtn();
    }

}


