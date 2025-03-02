using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BiffeProd
{
    public class MusicManager : MonoBehaviour
    {

        public static MusicManager instance;
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
   
