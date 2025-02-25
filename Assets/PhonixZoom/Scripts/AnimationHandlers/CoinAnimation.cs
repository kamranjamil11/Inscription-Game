using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace StarSpeller
{

    public class CoinAnimation : MonoBehaviour
    {
        public AnimationCurve curve;
        public float animationTime = 1.0f;
        public Vector3 destination;
        private float startTime;
        public string initPostionObjectName;
        public string FinaltPostionObjectName;
        private Transform initPosition;
        private Vector3 startPosition;
        GameObject destinationObject;
        void OnEnable()
        {
            destinationObject = GameObject.FindGameObjectWithTag(FinaltPostionObjectName);
            destination = destinationObject.transform.position;
            startTime = Time.time;
            initPosition = GameObject.FindGameObjectWithTag(initPostionObjectName).gameObject.transform;
            startPosition.x = initPosition.position.x+ (Random.Range(-50, 50));
            startPosition.y = initPosition.position.y + (Random.Range(-50, 50));
        }
       
        void LateUpdate()
        {
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / animationTime;

            float curveValue = curve.Evaluate(normalizedTime);

            transform.position = Vector3.Lerp(startPosition, destination, curveValue);
            if (transform.position == destination)
            {
                DestroyObject();
            }
        }
        void DestroyObject()
        {
           // destinationObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1, 1);
            Destroy(this.gameObject);
        }
    }
}