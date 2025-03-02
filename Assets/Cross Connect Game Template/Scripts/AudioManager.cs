using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
    {
   // public Slider musicValue;
   // public Slider soundValue;
    public AudioSource musicSource;
    public AudioSource SoundSource;
    //public GameObject[] sounds;
   // private AudioSource music;

    public static AudioManager instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GetMusicAndSoundValue();
        //  music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        // changeMusicState();
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
    //public void MusicSlider()
    //{
    //    musicSource.volume = musicValue.value;
    //    PlayerPrefs.SetFloat("MUSIC", musicValue.value);
    //}
    //public void SoundSlider()
    //{
    //    SoundSource.volume = soundValue.value;
    //    PlayerPrefs.SetFloat("SOUND", soundValue.value);
    //}
    public void GetMusicAndSoundValue()
    {
        if (!PlayerPrefs.HasKey("MUSIC"))
        {
            musicSource.volume = 1;          
        }
        else
        {
            musicSource.volume = PlayerPrefs.GetFloat("MUSIC");           
        }
        if (!PlayerPrefs.HasKey("SOUND"))
        {          
            SoundSource.volume = 1;
        }
        else
        {          
            SoundSource.volume = PlayerPrefs.GetFloat("SOUND");
        }

    }
}


