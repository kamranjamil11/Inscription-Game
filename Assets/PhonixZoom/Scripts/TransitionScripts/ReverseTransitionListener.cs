//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;


//public class ReverseTransitionListener : MonoBehaviour
//{
//   private void OnEnable()
//   {
//      GetComponent<Button>().onClick.AddListener(ReverseTransition);
//   }

//   void ReverseTransition()
//   {

//      GlobalAppController.Instance.GlobalAudioManager.playClick();
//        GlobalAppController.Instance.MenuController.PopPage();
          
//    }

//   private void OnDisable()
//   {
//      GetComponent<Button>().onClick.RemoveListener(ReverseTransition);
//   }
//}
