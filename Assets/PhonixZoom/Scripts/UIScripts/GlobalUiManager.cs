//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;


//public class GlobalUiManager : MonoBehaviour, IInitializable
//{
//    public static DateTime spaceRace_StartTime;
//    public static bool isMySchoolSpaceRace;
//    public EndScreenData endScreenData;
//    public DownloadinPannel downloadingPannel;
//    public generic_SettingPannel settingPannel;
//    public GameObject Loading, back_Btn, next_Btn, next_Btn1, loading_Data_Message;
//    public GameObject data_Not_Completed;
//    public MessagePopUpscript messagePopup;
//    public UiDataHolder currentUiHolder;
//    public InternetManager internetManager;
//    public List<UiDataHolder> allDataHolders;
//    public Text userName;
//    public static bool isDataLoaded;
//    public static bool isRhymingWords;

//    public delegate void ClosePopUp();

//    public static ClosePopUp onClosePopUp;

//    public delegate void EnablePopup();

//    public static EnablePopup onEnablePopUp;

//    public delegate void EnableDownloadingPannel();

//    public static EnableDownloadingPannel onEnableDownloading;
//    public static bool isListCreated;
//    public static bool isListPlay = true;
//    public float targetValue = 30f; // The value you want to add
//    public float duration = 3f;      // The duration over which to add the value
//    private float currentValue = 0;
//    public static GlobalUiManager instance;
//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
       
//    }
//    private void Start()
//    {
//    }
//    public void Initialize()
//    {
//        DontDestroyOnLoad(this.gameObject);
//        if (!PlayerPrefs.HasKey("MusicVal"))
//        {
//            PlayerPrefs.SetFloat("SoundVal", 1f);
//            PlayerPrefs.SetFloat("MusicVal", 0.5f);
//        }

//        GlobalAppController.Instance.GlobalAudioManager.Music.volume = PlayerPrefs.GetFloat("MusicVal");
//        GlobalAppController.Instance.GlobalAudioManager.Sound.volume = PlayerPrefs.GetFloat("SoundVal");
//        GlobalAppController.Instance.GlobalAudioManager.unMuteable.volume = PlayerPrefs.GetFloat("SoundVal");
//        GlobalAppController.Instance.GameLoop.AddOnEndEvent(PopUpGameEndScreen);
//        onClosePopUp += PopUpClosed;
//        onEnableDownloading += PopUpDownloadingPannel;
//        SettingButtonListener.onEnableSetting += OpenSetting;
//        internetManager.onInternetConnected.AddListener(ShowNointerNet);
//        internetManager.onInternetDisconnected.AddListener(HideNoInterNet);
//        GlobalAppController.Instance.GameLoop.AddOnLoadDataEvent(OpenLoading);

//    }
//    public void OnDisable()
//    {
//        GlobalAppController.Instance.GameLoop.RemoveOnEndEvent(PopUpGameEndScreen);
//        onClosePopUp -= PopUpClosed;
//        onEnableDownloading -= PopUpDownloadingPannel;
//        SettingButtonListener.onEnableSetting -= OpenSetting;
//        internetManager.onInternetConnected.RemoveListener(ShowNointerNet);
//        internetManager.onInternetDisconnected.RemoveListener(HideNoInterNet);
//        GlobalAppController.Instance.GameLoop.RemoveOnLoadDataEvent(OpenLoading);
//    }
   
//    public void PopUpGameEndScreen()
//    {
//        endScreenData.gameObject.SetActive(true);
//        onEnablePopUp?.Invoke();
//    }

//    public void PopUpDownloadingPannel()
//    {
//        downloadingPannel.gameObject.SetActive(true);
//    }
//    public void Next()
//    {
//        if (GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
//        {
            
//            GlobalAppController.Instance.GlobalAudioManager.SelectedSoundPlay(GlobalAppController.Instance.GlobalAudioManager.selectSounds);
//        }
//        else
//        {
//            GlobalAppController.Instance.GlobalAudioManager.SelectedSoundPlay(GlobalAppController.Instance.GlobalAudioManager.selectWords);
//        }
//        GlobalUiManager.onClosePopUp?.Invoke();
//    }
//    public void Next1()
//    {
//        //int words_Limit = Convert.ToInt32(GlobalAppController.Instance.AppDataHandler.limit);


//        if (GlobalAppController.Instance.APIManager.DownloadCounterToShow != 0 ||!isListPlay)
//        {
//            isDataLoaded = true;
//            onClosePopUp?.Invoke();
//            SpellingList spellingList = FindObjectOfType<SpellingList>();
//            if (spellingList != null)
//            {
//                GlobalAppController.Instance.GlobalAudioManager.PlayListTitle();
//            }
//        }
//        else
//        {
//            data_Not_Completed.SetActive(true);
//            back_Btn.SetActive(true);
//            next_Btn1.SetActive(false);
//        }

//    }

//    public void BackBtn() 
//    {
//        GlobalUiManager.onClosePopUp?.Invoke();
//        back_Btn.SetActive(false);       
//    }
//    public void PopUpClosed()
//   {
//        print("PopUpClosed");
//      endScreenData.gameObject.SetActive(false);
//      downloadingPannel.gameObject.SetActive(false);
//      settingPannel.gameObject.SetActive(false);
//      Loading.gameObject.SetActive(false);
//      messagePopup.gameObject.SetActive(false);
//   }

//   public void OpenLoading(int id,string cat,string limit,int type)
//   {
//        isDataLoaded = false;
        
//        next_Btn1.gameObject.SetActive(false);
//        loading_Data_Message.SetActive(true);
//        Loading.gameObject.SetActive(true);
//      Debug.Log(id + " current id" + cat + " current Cat " + limit + "  Limit " + type + " :Type Of Game");
//   }
//    public void LoadingPanel()
//    {
//        Loading.SetActive(true);
//        loading_Data_Message.SetActive(true);
//    }
//    public void OpenMessagePopUp()
//   {
//      messagePopup.gameObject.SetActive(true);
//   }
//   public void OpenSetting()
//   {
//        GlobalAppController.Instance.GlobalAudioManager.SelectedSoundPlay(GlobalAppController.Instance.GlobalAudioManager.settingClip);
//      settingPannel.gameObject.SetActive(true);
//   }

//   void ShowNointerNet()
//   {
//      internetManager.interNetPannel.SetActive(true);
//   }

//   void HideNoInterNet()
//   {
//      internetManager.interNetPannel.SetActive(false);
//   }
//    public IEnumerator AddValueOverTime(float valueToAdd, float time)
//    {
//        float startValue = currentValue;
//        float endValue = startValue + valueToAdd;
//        float elapsedTime = 0f;
//      loading_Data_Message.SetActive(true);
//        while (elapsedTime < time && GlobalAppController.Instance.APIManager.DownloadCounterToShow < 30)
//        {
//            elapsedTime += Time.deltaTime;
//            currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / time);
//            GlobalAppController.Instance.APIManager.DownloadCounterToShow++;
//            Debug.Log("Current Value: " + currentValue);
//            yield return null;
//        }
//        if (GlobalAppController.Instance.APIManager.DownloadCounterToShow >= 30)
//        {
//            next_Btn.SetActive(true);
//            loading_Data_Message.SetActive(false);
//        }
//        currentValue = endValue;
//        Debug.Log("Final Value: " + currentValue);
//    }

//    public void GetSpaceRace() 
//    {
//        SpaceRaceRoot sr_RT = GlobalAppController.Instance.APIManager.spaceRace_RT;
//         string scene_Name = SceneManager.GetActiveScene().name;
//        scene_Name = scene_Name.Replace(" ", "");
//        if (sr_RT.message != "No Space Race Found")
//        {
//            if (GlobalAppController.Instance.AppDataHandler.currentGameType == AppDataHandler.Game_type.phoneme)
//            {
//                for (int i = 0; i < sr_RT.dataObject.spaceRaceGamePhonics.Count; i++)
//                {
//                    sr_RT.dataObject.spaceRaceGamePhonics[i] = sr_RT.dataObject.spaceRaceGamePhonics[i].Replace(" ", "");
//                }
//                if (sr_RT.dataObject.spaceRaceGamePhonics.Contains(scene_Name))
//                {
//                    EndScreenData.isSpaceRace = true;
//                    spaceRace_StartTime = DateTime.Now;
//                }
//                else
//                {
//                    EndScreenData.isSpaceRace = false;
//                }
//            }
//            else
//            {
//                for (int i = 0; i < sr_RT.dataObject.spaceRaceGameSpelling.Count; i++)
//                {
//                    sr_RT.dataObject.spaceRaceGameSpelling[i] = sr_RT.dataObject.spaceRaceGameSpelling[i].Replace(" ", "");
//                }
//                if (sr_RT.dataObject.spaceRaceGameSpelling.Contains(scene_Name))
//                {
//                    EndScreenData.isSpaceRace = true;
//                    spaceRace_StartTime = DateTime.Now;
//                }
//                else
//                {
//                    EndScreenData.isSpaceRace = false;
//                }
//            }
//        }
//        print("isSpaceRace: " + EndScreenData.isSpaceRace);
//    }
//}
