using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class SettingPopup : MonoBehaviour
    {
    public Slider musicValue;
    public Slider soundValue;
    //public AudioSource musicSource;
   // public AudioSource SoundSource;

    AudioManager audioManager;
    private void Start()
    {
        audioManager=GameObject.FindAnyObjectByType<AudioManager>();
        GetMusicAndSoundValue();
    }
    public void CancelSetting()
    {
        
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

}


