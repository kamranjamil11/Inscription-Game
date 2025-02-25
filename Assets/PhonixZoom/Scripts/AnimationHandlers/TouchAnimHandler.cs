//using System;
////using Base;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace TouchScreen
//{
//    public class TouchAnimHandler : Singleton<TouchAnimHandler>
//    {

//        public GameObject TouchRef;
//        public Transform touchParent;


//        private void Awake()
//        {
//            DontDestroyOnLoad(this.gameObject);
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            //if(Input.touchCount > 0)
//            //{
//            //    Touch touch = Input.GetTouch(0); 
//            //    Vector3 touchPostion = Camera.main.ScreenToWorldPoint(touch.position);
//            //    GameObject click = Instantiate(TouchRef, touchParent);
//            //    click.transform.position = touchPostion;

//            //}
            
//            if (Input.GetMouseButtonDown(0))
//            {
//                touchParent= FindObjectOfType<Canvas>().transform;
//                Vector3 mousePos = Input.mousePosition;
//                //Vector3 touchPostion = Camera.main.ScreenToWorldPoint(mousePos);
//                GameObject click = Instantiate(TouchRef, touchParent);
//                click.transform.position = mousePos;
//            }

          
//        }
//    }
//}