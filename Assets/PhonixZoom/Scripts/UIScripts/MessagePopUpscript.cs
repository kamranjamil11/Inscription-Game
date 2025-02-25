using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopUpscript : MonoBehaviour
{
   public Text Message;
   private void OnEnable()
   {
      Message.text = "Un Able To Fetch Data";
   }

   public void okayButton()
   {
      //GlobalAppController.Instance.MenuController.PopPage();
      //GlobalUiManager.onClosePopUp?.Invoke();
   }
}
