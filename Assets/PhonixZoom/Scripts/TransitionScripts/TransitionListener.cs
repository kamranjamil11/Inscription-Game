using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class TransitionListener : MonoBehaviour
{
   
    public Page nextPage;
    public GameObject Prev_Page;
   private void OnEnable()
   {
      GetComponent<Button>().onClick.AddListener(TransitionToDo);
        
   }

   void TransitionToDo()
   {
            
      //GlobalAppController.Instance.GlobalAudioManager.playClick();
      //  GlobalAppController.Instance.MenuController.PushPage(nextPage);
        if (Prev_Page != null) 
        {
            Prev_Page.SetActive(false);
        }
   }

   private void OnDisable()
   {
      GetComponent<Button>().onClick.RemoveListener(TransitionToDo);
   }
}
