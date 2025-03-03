using System.Collections;
using UnityEngine;

namespace BiffeProd
{
    public class RateApp : MonoBehaviour
    {
        public string packageName = "com.example.myapp";
        public void OpenPlayStorePage()
        {
            string url = "market://details?id=" + packageName;

            // If the Play Store app is not available, open the Play Store website
            string fallbackUrl = "http://play.google.com/store/apps/details?id=" + packageName;

            // Open the Play Store URL or fallback URL
            Application.OpenURL(url);

            // Delay opening the fallback URL to allow the Play Store app to open
            StartCoroutine(OpenFallbackURL(fallbackUrl));
        }

        private IEnumerator OpenFallbackURL(string url)
        {
            yield return new WaitForSeconds(1.5f); // Adjust the delay as needed

            // Open the fallback URL in case the Play Store app did not open
            Application.OpenURL(url);
        }
    }
}
