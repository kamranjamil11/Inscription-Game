using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace BiffeProd
{
    public class CustomTransitionManager : MonoBehaviour
    {
        private static GameObject transitionCanvas;
        private GameObject transitionOverlay;

        private void Awake()
        {
            print(gameObject.name);
            transitionCanvas = new GameObject("TransitionCanvas");
            var canvas = transitionCanvas.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 5;
            DontDestroyOnLoad(transitionCanvas);
        }

        public static void LoadLevelWithTransition(string levelName, float duration, Color fadeColor)
        {
            var transitionObj = new GameObject("Transition");
            transitionObj.AddComponent<CustomTransitionManager>();
            transitionObj.GetComponent<CustomTransitionManager>().StartFade(levelName, duration, fadeColor);
            transitionObj.transform.SetParent(transitionCanvas.transform, false);
            transitionObj.transform.SetAsLastSibling();
        }

        private void StartFade(string levelName, float duration, Color fadeColor)
        {
            StartCoroutine(RunFade(levelName, duration, fadeColor));
        }

        private IEnumerator RunFade(string levelName, float duration, Color fadeColor)
        {
            var backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, fadeColor);
            backgroundTexture.Apply();

            transitionOverlay = new GameObject();
            var image = transitionOverlay.AddComponent<Image>();
            var rect = new Rect(0, 0, backgroundTexture.width, backgroundTexture.height);
            var sprite = Sprite.Create(backgroundTexture, rect, new Vector2(0.5f, 0.5f), 1);
            image.material.mainTexture = backgroundTexture;
            image.sprite = sprite;
            var newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);

            transitionOverlay.transform.localScale = new Vector3(1, 1, 1);
            transitionOverlay.GetComponent<RectTransform>().sizeDelta = transitionCanvas.GetComponent<RectTransform>().sizeDelta;
            transitionOverlay.transform.SetParent(transitionCanvas.transform, false);
            transitionOverlay.transform.SetAsFirstSibling();

            var time = 0.0f;
            var halfDuration = duration / 2.0f;
            while (time < halfDuration)
            {
                time += Time.deltaTime;
                image.canvasRenderer.SetAlpha(Mathf.InverseLerp(0, 1, time / halfDuration));
                yield return new WaitForEndOfFrame();
            }

            image.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForEndOfFrame();

            SceneManager.LoadScene(levelName);

            time = 0.0f;
            while (time < halfDuration)
            {
                time += Time.deltaTime;
                image.canvasRenderer.SetAlpha(Mathf.InverseLerp(1, 0, time / halfDuration));
                yield return new WaitForEndOfFrame();
            }

            image.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForEndOfFrame();

            Destroy(transitionCanvas);
        }
    }
}
    