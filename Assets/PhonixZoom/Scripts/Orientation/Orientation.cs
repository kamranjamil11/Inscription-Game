using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{
    public class Orientation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
           // StartCoroutine(SwitchChange());
        }

        IEnumerator SwitchChange()
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
