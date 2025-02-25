//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public enum GameState
//{
//    None,
//    Loading,
//    Enter,
//    Selection,
//    Start,
//    End
//}
//public class GameLoop : MonoBehaviour,IInitializable
//{
//   // public EndScreenData endScreenData;   
//    private GameState state;
//    // public delegate void OnGameEnterState();
//    // public static OnGameEnterState enterOfGame;
//    // public delegate void OnGameSelectionState();
//    // public static OnGameSelectionState selectionOfGame;
//    public delegate void OnGameStartState();
//    public static OnGameStartState startOfGame;

//    public delegate void OnGameEndState();
//    public static OnGameEndState endOfGame;

//    public delegate void OnGameEnterState();

//    public static OnGameEnterState enterOfGame;

//    public delegate void OngameSelectionState();

//    public static OngameSelectionState selectionOfGame;

//    public delegate void OnLoadData(int id,string Cat,string limit,int type);

//    public static OnLoadData loadOfdata;
//    //public delegate void OpenLoadingData(int id, string Cat, string limit, int type);

//    //public static OpenLoadingData openLoaddata;

//    public LO_GamePlayHandler lo_gamecontroller;
//    public static GameLoop instance;
//    public void GetSceneRef()
//    {
//        lo_gamecontroller = FindObjectOfType<LO_GamePlayHandler>();
//    }
//    void Awake()
//    {
//        // If an instance already exists and it's not this one, destroy this one
//        if (instance != null && instance != this)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//    }
   
//    public void Initialize()
//    {        
       

//        //if (instance == null)
//        //{
//        //    instance = this;
//        //}
//        //else
//        //{
//        //    Debug.Log(gameObject.name);
//        //    Destroy(gameObject);
//        //}
//        print("GameLoopInitialize:");
//        startOfGame = null;
//        loadOfdata = null;
//        endOfGame = null;
//        enterOfGame = null;
//        selectionOfGame = null;
//    }
//    private void OnGamestateChange()
//    {
//        switch (GetCurrentGameState())
//        {
//            case GameState.None:
//                break;
//            case GameState.Enter:
//                GameEnter();
//                break;
//            case GameState.Selection:
//                GameSelection();
//                break;
//            case GameState.Start:
//                GameStart();
//                break;
//            case GameState.End:
//                GameEnd();
//                break;
//            case GameState.Loading:              
//                DataLoading();
//                break;
//            default:
//                throw new ArgumentOutOfRangeException(nameof(state), state, null);
//        }
//    }

//    void DataLoading()
//    {
//        loadOfdata?.Invoke((int)GlobalAppController.Instance.AppDataHandler.currentGameId,
//            GlobalAppController.Instance.AppDataHandler.currentCategory.ToString(),
//            GlobalAppController.Instance.AppDataHandler.limit,
//            (int)GlobalAppController.Instance.AppDataHandler.currentGameType);
//    }
//    void GameEnter()
//    {
//        enterOfGame?.Invoke();
//    }
//    void GameSelection()
//    {
//        selectionOfGame?.Invoke();
//    }
//    void GameStart()
//    {
//        print("BlackHole GoToPractice");
//        startOfGame?.Invoke();
       
//    }
//    private void GameEnd()
//    {
//        endOfGame?.Invoke();
//        //GlobalAppController.Instance.AppDataHandler.PostGameResult(
//        //    GlobalAppController.Instance.PlayerController.StudentProfile.student_id,
//        //    EndScreenData.rightAnswer.ToString(),
//        //    EndScreenData.wrongAnswer.ToString()
//        //);
//    }
    

//    public GameState GetCurrentGameState()
//    {
//        return state;
//    }

//    public void OnLoadGameData()
//    {
       
//        state = GameState.Loading;
//        OnGamestateChange();
//    }
//    public void OnGameExit()
//    {
//        state = GameState.None;
//        OnGamestateChange();
//    }
    
//    public void OngameEnter()
//    {
//        state = GameState.Enter;
//        OnGamestateChange();
//    }

//    public void OnGameStart()
//    {
//        state = GameState.Start;
//        print("BlackHole GoToPractice");
//        OnGamestateChange();
//    }

//    public void OnGameEnd()
//    {
//        state = GameState.End;
//        OnGamestateChange();
//    }

//    public void OnGameSelection()
//    {
//        state = GameState.Selection;
//        OnGamestateChange();
//    }

//    public void AddOnStartEvent(OnGameStartState eve)
//    {
//        Debug.Log("Del: AddOnStartEvent");
//        startOfGame += eve;
//    }

//    public void RemoveOnStartEvent(OnGameStartState eve)
//    {
//        Debug.Log("Del: Unsub_AddOnStartEvent");
//        startOfGame -= eve;
       
//    }
//    public void AddOnEndEvent(OnGameEndState eve)
//    {
//        endOfGame += eve;
//    }

//    public void RemoveOnEndEvent(OnGameEndState eve)
//    {
//        endOfGame -= eve;
//    }
//    public void AddOnEnterEvent(OnGameEnterState eve)
//    {
//        enterOfGame += eve;
//    }
    
//    public void RemoveOnEnterEvent(OnGameEnterState eve)
//    {
//        enterOfGame -= eve;
//    }
//    public void AddOnSelectionEvent(OngameSelectionState eve)
//    {
//        selectionOfGame += eve;
//    }
    
//    public void RemoveOnSelectionEvent(OngameSelectionState eve)
//    {
//        selectionOfGame -= eve;
//    }
//    public void AddOnLoadDataEvent(OnLoadData eve)
//    {
//        print("OnLoadGameData");
//        loadOfdata += eve;
//    }
    
//    public void RemoveOnLoadDataEvent(OnLoadData eve)
//    {
//        loadOfdata -= eve;
//    }
//    //public void AddLoadingDataEvent(OpenLoadingData eve)
//    //{
//    //    openLoaddata += eve;
//    //}

//    //public void RemoveLoadingDataEvent(OpenLoadingData eve)
//    //{
//    //    openLoaddata -= eve;
//    //}
//    public void OnDisable()
//    {
        
//    }

//}
