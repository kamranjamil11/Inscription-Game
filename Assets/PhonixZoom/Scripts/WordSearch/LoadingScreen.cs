using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen; // Assign a UI panel as the loading screen
    public Slider progressBar; // Assign a UI Slider
    private float currentValue = 0;
    int DownloadCounterToShow = 0;
    public Text percentage_Text;
    public string sceneName;
    private void Start()
    {
        // LoadScene("MainMenu");
        StartCoroutine(CountTo100(5));
    }
    IEnumerator CountTo100(float duration)
    {
        float elapsedTime = 0f;
        int startValue = 1;
        int endValue = 100;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, progress));

            if (percentage_Text != null)
                percentage_Text.text = currentValue.ToString()+"%"; // Update UI text
            if ( currentValue >= 100)
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
            yield return null; // Wait for next frame
        }

        //// Ensure final value is exactly 100
        //if (percentage_Text != null)
        //    percentage_Text.text = "100";
    }
    public IEnumerator AddValueOverTime(float valueToAdd, float time)
    {
        float startValue = currentValue;
        float endValue = startValue + valueToAdd;
        float elapsedTime = 0f;
       // loading_Data_Message.SetActive(true);
        while (elapsedTime < time && DownloadCounterToShow < 100)
        {
            elapsedTime += Time.deltaTime;
            currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / time);
            DownloadCounterToShow++;
            percentage_Text.text = DownloadCounterToShow.ToString() + "%";
            Debug.Log("elapsedTime: " + elapsedTime);
            yield return null;
        }
        if (elapsedTime>=5&&DownloadCounterToShow >= 100)
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        currentValue = endValue;
       // Debug.Log("Final Value: " + currentValue);
    }
    public void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevent auto-switching until loading is complete

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress
            progressBar.value = progress; // Update UI slider

            if (operation.progress >= 0.9f)
            {
                // Optionally add a delay or UI prompt before switching
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
