using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Samples.Purchasing.GooglePlay.FraudDetection;

public class FirebaseData : MonoBehaviour
{
    public DataToSave dataToSave;
    public string userId;
    DatabaseReference dbReference;
    public static FirebaseData instance;    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(DatabaseFetching());
    }
    IEnumerator DatabaseFetching() 
    {
        yield return new WaitUntil(()=>FirebaseAuthManager.isFirebaseInitiliazed);
        print("Firebase initialized.");
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
       // userId = PlayerPrefs.GetString("USERID");
       // DateLoadFunc();
    }
    public void DateLoadFunc()
    {
        StartCoroutine(DataLoadEnum());
    }
    IEnumerator DataLoadEnum() 
    {
       var serverData= dbReference.Child("users").Child(userId).GetValueAsync();
        yield return new WaitUntil(predicate:()=> serverData.IsCompleted);
        print("Process is Completed");

        DataSnapshot snapshot = serverData.Result;
        string jsonData=snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            print("Server data found");
            dataToSave = JsonUtility.FromJson<DataToSave>(jsonData);

            PlayerPrefs.SetInt("COINS", dataToSave.coins);
            PlayerPrefs.SetInt("SCRAB_POWERUP", dataToSave.scarabPowers);
            PlayerPrefs.SetInt("HINT_POWERUP", dataToSave.eyeHorusPowers);
            PlayerPrefs.SetInt("LOTUS_POWERUP", dataToSave.lotusPowers);

            UIHandler uIHandler =GameObject.FindAnyObjectByType<UIHandler>();
            uIHandler.coins_Text.text = dataToSave.coins.ToString();
            uIHandler.coins_Text_Portrait.text = dataToSave.coins.ToString();
        }
        else 
        {
            print("no data found");
        }


    }
    public void DataSaveFun()
    {
        int coins = PlayerPrefs.GetInt("COINS");
        int scarabsCount = PlayerPrefs.GetInt("SCRAB_POWERUP");
        int eyeHorusCount = PlayerPrefs.GetInt("HINT_POWERUP");
        int LotusCount = PlayerPrefs.GetInt("LOTUS_POWERUP");

        dataToSave.coins = coins;
        dataToSave.scarabPowers = scarabsCount;
        dataToSave.eyeHorusPowers = eyeHorusCount;
        dataToSave.lotusPowers = LotusCount;

        string json=JsonUtility.ToJson(dataToSave);
        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }

}
[System.Serializable]
public class DataToSave 
{
    public string userName;
    public int coins;
    public int scarabPowers;
    public int eyeHorusPowers;
    public int lotusPowers;
}