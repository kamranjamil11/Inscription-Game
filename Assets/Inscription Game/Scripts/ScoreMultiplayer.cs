using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMultiplayer : MonoBehaviour
{
    public RectTransform uiElement; // Assign in Inspector
    public RectTransform targetPosition;
    public float speed = 500f;
    GameController gameController;
    // Start is called before the first frame update
    private void Awake()
    {
        uiElement = gameObject.transform.GetComponent<RectTransform>();
        gameController = FindObjectOfType<GameController>();
        targetPosition = gameController.score_Text.GetComponent<RectTransform>();
        StartCoroutine(MoveUI(targetPosition.anchoredPosition,0.8f));
    }

    // Update is called once per frame
    void Update()
    {
        float diff=Vector2.Distance(targetPosition.anchoredPosition, uiElement.anchoredPosition);
       // print(diff);
        if (diff<50)
        {
            Destroy(this.gameObject);
        }

    }
    IEnumerator MoveUI(Vector2 target, float duration)
    {
        Vector2 startPos = uiElement.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            uiElement.anchoredPosition = Vector2.Lerp(startPos, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        uiElement.anchoredPosition = target;
    }
}
