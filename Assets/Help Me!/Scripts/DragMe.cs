//using DG.Tweening;
//using System;
//using System.Collections.Generic;
////using System.Drawing;
//using System.Globalization;
//using System.Linq;
//using TMPro;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;


//namespace PhonicZoomApp
//{
//    [RequireComponent(typeof(Image))]
//    [ExecuteInEditMode]
//    public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//    {
//        public bool dragOnSurfaces = true;
//        public bool drag;
//        Canvas canvas;
//        public GameObject m_DraggingIcon;
//        private RectTransform m_DraggingPlane;

//        public UnityEngine.UI.Extensions.UILineRenderer Line;
//        RectTransform startObject;
//        RectTransform endObject;

//        public GameObject Dragger;
//        Vector2 startPos;
//        public List<GameObject> placeHolders;
//        public Sprite dragIconImg;

//        private Vector3 offset;
//        private Vector3 screenPoint;
//        private void Awake()
//        {
//            Dragger = this.gameObject;
//            //m_DraggingIcon = this.gameObject;
//            startPos = this.transform.localPosition;

//        }
//        public void OnBeginDrag(PointerEventData eventData)
//        {

//            canvas = transform.root.gameObject.GetComponent<Canvas>();
//            if (canvas == null)
//                return;

//            // We have clicked something that can be dragged.
//            // What we want to do is create an icon for this.
//            m_DraggingIcon = new GameObject("icon");
//            //		m_DraggingIcon.transform.parent = GameObject.Find ("Shape").transform;
//            if (GlobalAppController.Instance.AppDataHandler.currentGameId != AppDataHandler.Game_id.FillTheGap)
//            {
//                m_DraggingIcon.transform.parent = gameObject.transform.parent.gameObject.transform;
//            }
//            else 
//            {
//                m_DraggingIcon.transform.parent = gameObject.transform.parent.root.gameObject.transform;
//            }
//            //		m_DraggingIcon.transform.SetParent(canvas.transform, false);
//            //		m_DraggingIcon.transform.SetAsLastSibling();

//            var image = m_DraggingIcon.AddComponent<Image>();

//            image.sprite = dragIconImg;
//            //image.SetNativeSize();
//            startObject = m_DraggingIcon.GetComponent<RectTransform>();

//            if (dragOnSurfaces)
//                m_DraggingPlane = transform as RectTransform;
//            else
//                m_DraggingPlane = canvas.transform as RectTransform;

//            SetDraggedPosition(eventData);
//        }

//        public void OnDrag(PointerEventData data)
//        {


//            if (m_DraggingIcon != null)
//                SetDraggedPosition(data);


//            // Apply the clamped position back to the object
//            //if (gameObject.name == "BlankWord(Clone)")
//            //{
//            //    //Vector2 blankPos = transform.parent.GetComponent<WordScan>().blank_pos;
//            //    //if (Dragger)
//            //    //    Dragger.transform.position = new Vector3(Mathf.Clamp(m_DraggingIcon.transform.position.x, blankPos.x - 120, blankPos.x + 100),
//            //    //Dragger.transform.position.y, Dragger.transform.position.z);

//            //}
//            //else
//            //{
//                if (Dragger)
//                    Dragger.transform.position = m_DraggingIcon.transform.position;

//           // }

//        }

//        public void SetDraggedPosition(PointerEventData data)
//        {
//            //Line.gameObject.SetActive (true);
//            if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
//                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

//            endObject = m_DraggingIcon.GetComponent<RectTransform>();
//            Vector3 globalMousePos;
//            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
//            {


//                endObject.position = globalMousePos;
//                endObject.rotation = m_DraggingPlane.rotation;
//                drag = true;
//                //Line.OnRebuildRequested();
//                //Line.Rebuild(CanvasUpdate.PreRender);
//                //Line.enabled=true;
//                //Line.Points[0] = GetComponent<RectTransform>().anchoredPosition;
//                //Line.Points[1] = endObject.anchoredPosition;
//                //Line.SetAllDirty();

//            }
//        }


//        int i = 0;
//        int counter;
//        public void OnEndDrag(PointerEventData eventData)
//        {
//            print("OnEndDrag");
//            drag = false;
//            if (m_DraggingIcon != null)
//                Destroy(m_DraggingIcon);
            
//          //  print(Vector3.Distance(placeHolders[0].transform.position, transform.position));

//            //GameObject snap = placeHolders.Where(x => Vector3.Distance(x.transform.position, transform.position) < 20).SingleOrDefault();
//            GameObject snap = placeHolders.Where(x => Vector3.Distance(x.transform.position, transform.position) < 50).SingleOrDefault();
//           // print("Snapping Object: " + snap.name);
           
//            if (GlobalAppController.Instance.AppDataHandler.currentGameId == AppDataHandler.Game_id.FillTheGap)
//            {
//                FG_GamePlay fG_GamePlay = FindObjectOfType<FG_GamePlay>();
//                fG_GamePlay.optionParent.GetComponent<GridLayoutGroup>().enabled = false;
//                fG_GamePlay.optionParent.GetComponent<GridLayoutGroup>().enabled = true;
//            }
//            if (snap != null)
//            {
//                snap.SetActive(true);
//                snap.transform.GetChild(0).GetComponent<Text>().text = transform.GetChild(0).GetComponent<Text>().text;
//              //  print("snap:" + Dragger.transform.position);
//              //  snap.transform.position = new Vector2(Dragger.transform.position.x, snap.transform.position.y);
//              //  transform.localPosition = startPos;
//                //transform.position = snap.transform.position;

//                // print("Snapping Object: " + snap + " , " + transform.position + " , " + snap.transform.position + " , " + snap.GetComponent<FillTheGap.FG_Button>().BlankNumber);
//                if (GlobalAppController.Instance.AppDataHandler.currentGameId != AppDataHandler.Game_id.FillTheGap)
//                {


//                    GameObject obj = Instantiate(this.gameObject, snap.transform.position, Quaternion.identity);
//                    // obj.transform.localScale = new Vector2(0.9f,0.9f); 
//                    obj.name = GetComponentInChildren<Text>().text;
//                    obj.GetComponent<Button>()?.onClick.Invoke();
//                    Destroy(obj.GetComponent<Button>());
//                    FillTheGap.FG_GameManager.FG_GameManagerInstance.spawningButtons.Add(obj);
//                    obj.transform.SetParent(this.transform.parent);
//                    obj.SetActive(false);
//                    if (snap.GetComponent<FillTheGap.FG_Button>().BlankNumber != 5)
//                    {
//                        print(FillTheGap.FG_GameManager.FG_GameManagerInstance.fillPoint.Count);
//                        if (!FG_GamePlay.isPracticeMode)
//                        {
//                            foreach (GameObject item in FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.options)//options
//                            {
//                                item.GetComponent<DragMe>().placeHolders[0] =
//                                    FillTheGap.FG_GameManager.FG_GameManagerInstance.fillPoint[snap.GetComponent<FillTheGap.FG_Button>().BlankNumber + 1];

//                            }
//                            //FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.options.ForEach(item => item.GetComponent<DragMe>()?.placeHolders[0] = FillTheGap.FG_GameManager.FG_GameManagerInstance.fillPoint[snap.GetComponent<FillTheGap.FG_Button>().BlankNumber + 1]);

//                            FillTheGap.FG_GameManager.FG_GameManagerInstance.fillPoint.Where(
//                                x => x.GetComponent<FillTheGap.FG_Button>().BlankNumber <= 5 &&
//                                 x.GetComponent<FillTheGap.FG_Button>().BlankNumber == snap.GetComponent<FillTheGap.FG_Button>().BlankNumber + 1
//                                ).FirstOrDefault().GetComponent<Button>()?.onClick.Invoke();

//                        }
//                        else
//                        {

//                            //var gameData = FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata;

//                            //for (int i = 0; i < gameData.options.Count; i++)
//                            //{
//                            //	var blankButton = gameData.options[i];
//                            //	var placeHolder = blankButton.GetComponent<DragMe>().placeHolders[0];
//                            //	var blankNumber = placeHolder.GetComponent<FillTheGap.FG_Button>().BlankNumber;

//                            //	int currentIndex = gameData.wrongBlank.FindIndex(x => x.GetComponent<FillTheGap.FG_Button>().BlankNumber == blankNumber);
//                            //	if(currentIndex+1 <= gameData.wrongBlank.Count)
//                            //                      {
//                            //		blankButton.GetComponent<DragMe>().placeHolders[0] = gameData.wrongBlank[currentIndex + 1];
//                            //		print("Practice Mode Tracking: Activate New"+ placeHolder + " , "+ gameData.wrongBlank[currentIndex + 1].name +" , "+ currentIndex + 1);
//                            //		gameData.wrongBlank[currentIndex+1].GetComponent<Button>()?.onClick.Invoke();
//                            //	}

//                            //}

//                            #region Dead
//                            //for (int i = 0; i < FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.blankButtons.Count; i++)
//                            //                  {

//                            //	//FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.blankButtons[i].GetComponent<DragMe>().placeHolders[0]=
//                            //	//	FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.wrongBlank[i];



//                            //	//FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.wrongBlank.Where(
//                            //	//x => x.GetComponent<FillTheGap.FG_Button>().BlankNumber ==
//                            //	//FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.blankButtons[i].GetComponent<DragMe>().placeHolders[0].GetComponent<FillTheGap.FG_Button>().BlankNumber
//                            //	//).FirstOrDefault();


//                            //	//---------------Find Index

//                            //	int currentIndex = FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.wrongBlank.FindIndex(
//                            //   x => x.GetComponent<FillTheGap.FG_Button>().BlankNumber ==
//                            //   FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.blankButtons[i].GetComponent<DragMe>().placeHolders[0].GetComponent<FillTheGap.FG_Button>().BlankNumber
//                            //   );

//                            //	FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.blankButtons[i].GetComponent<DragMe>().placeHolders[0] =
//                            //		FillTheGap.FG_GameManager.FG_GameManagerInstance.gamePlaydata.wrongBlank[currentIndex++];
//                            //}
//                            #endregion
//                        }
//                    }
//                }
//                else
//                {
//                    // counter++;
//                    //Root rt = GlobalAppController.Instance.APIManager.rt;
//                    FG_GamePlay fG_GamePlay = FindObjectOfType<FG_GamePlay>();

//                    snap.GetComponent<BlankWord>().BlankWordCounter();
//                    GameObject current_TextObj = snap.transform.parent.gameObject;
//                    string org_Word = null;
//                    string sentence = current_TextObj.GetComponent<WordScan>().main_Sentence;
//                    string[] textArray = sentence.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
//                    string tgr_Word = null;
//                    string space_Count = null;
//                   // string temp_Word = current_TextObj.GetComponent<WordScan>().targetWord;
//                    //Char[] char_Array =  temp_Word.ToCharArray();
//                    //for (int i = 0; i < char_Array.Length+10; i++)
//                    //{
//                    //    space_Count = space_Count+" ";
//                    //}
//                    tgr_Word = transform.GetComponentInChildren<Text>().text;
//                    for (int j = 0; j < textArray.Length; j++)
//                    {
//                        if (textArray[j].ToLower() == current_TextObj.GetComponent<WordScan>().blanks)
//                        {
//                            textArray[j] = /*space_Count*/ tgr_Word;
//                        }
//                        org_Word += " " + textArray[j];
//                    }
//                    current_TextObj.GetComponent<WordScan>().paragraphText.text = org_Word;
//                    TMP_Text txt = current_TextObj.GetComponent<WordScan>().paragraphText;

                    
                   
                    
                   
//                    HighlightWord(tgr_Word, txt);
//                    //string newBlank = "\n";
//                    //newBlank = temp_Word + newBlank + current_TextObj.GetComponent<WordScan>().blanks;
//                    //temp_Word = temp_Word.Replace(temp_Word, newBlank);
//                    //print("temp_Word:" + temp_Word);
//                    //for (int j = 0; j < textArray.Length; j++)
//                    //{
//                    //    if (textArray[j].ToLower() == tgr_Word.ToLower())
//                    //    {
//                    //        textArray[j] = temp_Word;
//                    //    }
//                    //    org_Word += " " + textArray[j];
//                    //}



//                    if (fG_GamePlay.blankWord_Counter == fG_GamePlay.temp_List.Count)
//                        {
//                            fG_GamePlay.SubmitBtn.SetActive(true);
//                        }
                   
//                    fG_GamePlay.Shuffle();
//                     snap.SetActive(false);
//                }
              
//            }
//            else
//            {
//              //  if (gameObject.name != "BlankWord(Clone)")
//                //{
//                    transform.localPosition = startPos;
//               // }
                
//            }


//            //		if (MatchingCollisonManager.collisionChecker) 
//            //		{
//            //			Dragger.transform.parent.GetChild(2).GetComponent<MatchingCollisonManager>().DestroyDragger ();
//            //			MatchingCollisonManager.collisionChecker = false;
//            //			Line.Points [1] = GetComponent<RectTransform> ().anchoredPosition;	
//            //			Matching_ShapesManager.NumberOfShapes--;
//            //			Debug.Log (Matching_ShapesManager.NumberOfShapes);
//            //			if (Matching_ShapesManager.NumberOfShapes <= 0) 
//            //			{
//            //
//            //				FindObjectOfType<Matching_ShapesManager> ().LevelComplete ();
//            //			}
//            //
//            //
//            //		}
//            //		else 
//            //		{
//            //			 
//            //			Dragger.transform.position = transform.position;
//            //			Line.gameObject.SetActive (false);
//            //
//            //		}



//        }
//        public void OnGUI()
//        {

//            // DrawBoxAroundPoints(endObject.position, startObject.position);
//            //if (endObject )
//            //{
//            //    Line.Points[0] = GetComponent<RectTransform>().anchoredPosition;
//            //    Line.Points[1] = endObject.anchoredPosition;
//            //    Line.SetAllDirty();
//            //}

//        }

//        void DrawBoxAroundPoints(Vector2 p0, Vector2 p1, float height = 6, float extraLength = 0)
//        {
//            // Draw a thin, rotated box around the line between the given points.
//            // Our approach is to rotate the GUI transformation matrix around the center
//            // of the line, and then draw an unrotated (horizontal) box at that point.
//            float width = (p1 - p0).magnitude + extraLength;
//            Vector2 center = (p0 + p1) * 0.5f;
//            Rect horizontalRect = new Rect(center.x - width / 2, center.y - height / 2, width, height);
//            float angle = Mathf.Atan2(p1.y - p0.y, p1.x - p0.x) * Mathf.Rad2Deg;

//            Matrix4x4 savedMatrix = GUI.matrix;
//            Vector3 centerScreen = GUIUtility.GUIToScreenPoint(center);
//            GUI.matrix =
//                Matrix4x4.TRS(centerScreen, Quaternion.identity, Vector3.one)
//                    * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one)
//                    * Matrix4x4.TRS(-centerScreen, Quaternion.identity, Vector3.one)
//                    * GUI.matrix;

//            GUI.Box(horizontalRect, "");
//            GUI.matrix = savedMatrix;
//        }

//        static public T FindInParents<T>(GameObject go) where T : Component
//        {
//            if (go == null) return null;
//            var comp = go.GetComponent<T>();

//            if (comp != null)
//                return comp;

//            Transform t = go.transform.parent;
//            while (t != null && comp == null)
//            {
//                comp = t.gameObject.GetComponent<T>();
//                t = t.parent;
//            }
//            return comp;
//        }
//        TMP_TextInfo textInfo = null;
//        string Sentence = "";
//        public void HighlightWord(/*int wordIndex, */string word, /*Color color,*/ TMP_Text txt)
//        {
//           // temp_Word = old_Sentenc;
//           // tapCounter++;
//            Sentence = "";
//            txt.ForceMeshUpdate();
//            string[] temp_Words = txt.text.Split(' ');//Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            
//            // int wordIndex = 0;
//            for (int j = 0; j < txt.textInfo.wordCount; j++)
//            {
//                string tm_word = txt.textInfo.wordInfo[j].GetWord()/*.ToLower()*/;
//                //int word_Index = Array.IndexOf(temp_Words, tm_word);
//                if (temp_Words[j]/*.ToLower()*/ == word/*.ToLower()*/)
//                {
//                    //wordIndex = j;
//                    ReplaceWordAtIndex(j, "<color=#" + ColorUtility.ToHtmlStringRGB(Color.green) + ">" + tm_word + "</color>", txt);
//                }
//                else
//                {
//                    ReplaceWordAtIndex(j, "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + tm_word + "</color>", txt);
//                }

//            }
           
                    

            


//            //gamePlayHandler.tapCounterText.GetComponent<Text>().text = "Tap Count: " + tapCounter.ToString();

//        }
//        public void ReplaceWordAtIndex(int wordIndex, string newWord, TMP_Text txt)
//        {
//            //textInfo = txt.textInfo;
//            //print("txt:"+ txt.text);
//            //if (wordIndex < 0 || wordIndex >= textInfo.wordCount)
//            //{
//            //    // Debug.LogError("Invalid word index: " + wordIndex);
//            //    return;
//            //}

//            //TMP_WordInfo wordInfo = textInfo.wordInfo[wordIndex];
//            //int firstCharIndex = wordInfo.firstCharacterIndex;
//            //int lastCharIndex = wordInfo.lastCharacterIndex;
//            //// textInfo.characterInfo[firstCharIndex].index;
//            //int startIndex = textInfo.characterInfo[firstCharIndex].index;
//            //int endIndex = textInfo.characterInfo[lastCharIndex].index + 1;

//            //string oldWord = txt.text.Substring(startIndex, endIndex - startIndex);
//            //string newText = txt.text.Remove(startIndex, endIndex - startIndex).Insert(startIndex, newWord);

//            Sentence += newWord + " ";
//            txt.text = Sentence;
//          //  Debug.Log("Replaced word '" + oldWord + "' at index " + wordIndex + " with '" + newWord + "'");

//        }
//    }
//}