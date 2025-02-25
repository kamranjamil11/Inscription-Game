using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TouchScreen
{
    public class DestroyTouchAnim : MonoBehaviour
    {
        public float timeToDestroy = .5f;
        public bool disable;
        private void OnEnable()
        {
            //print("OnEnable");
            if (disable)
            {
                StartCoroutine(DisableThisObject());
            }
            else
            {
                StartCoroutine(DestroyThisObj());
            }
            
            
        }
        IEnumerator DestroyThisObj()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(this.gameObject);
        }

        IEnumerator DisableThisObject()
        {
            yield return new WaitForSeconds(timeToDestroy);
            this.gameObject.SetActive(false);
        }
    }
}