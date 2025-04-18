using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ScoreMultiplayer : MonoBehaviour
{
    public enum ObjType {score,scarab }
    public ObjType obj_Type;
    public RectTransform uiElement; // Assign in Inspector
    public RectTransform targetPosition;
    public float speed = 500f;
    GameController gameController;
    public Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        if (obj_Type == ObjType.score)
        {
            uiElement = gameObject.transform.GetComponent<RectTransform>();
           
            targetPosition = gameController.score_Text.GetComponent<RectTransform>();
            StartCoroutine(MoveUI(targetPosition.anchoredPosition, 0.8f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (obj_Type == ObjType.score)
        {
            float diff = Vector2.Distance(targetPosition.anchoredPosition, uiElement.anchoredPosition);
            // print(diff);
            if (diff < 50)
            {
                Destroy(this.gameObject);
            }
        }
        else 
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate image1 around Z axis to face image2
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
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
    public IEnumerator ScarabMoveUI(Vector2 target, float duration,GameObject scarabBtn,LevelCreator lvlCreator)
    {
        RectTransform scarab = gameObject.GetComponent<RectTransform>();
        Vector2 startPos = scarab.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
           
            scarab.anchoredPosition = Vector2.Lerp(startPos, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Generic_Timer.isStop = true;
        scarab.anchoredPosition = target;
        foreach (var item in lvlCreator.lettersGrid)
        {
            AnimatorStateInfo stateInfo = item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            // Current animation ka naam check karna
            if (stateInfo.IsName("Hint"))
            {
                item.GetComponent<Animator>().SetTrigger("Idle");
            }
        }
        scarabBtn.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        scarabBtn.transform.GetChild(2).gameObject.SetActive(false);
        char randomUpperChar = (char)Random.Range('A', 'Z' + 1);
        scarabBtn.GetComponent<SingleLetter>().Value= randomUpperChar.ToString();
        scarabBtn.GetComponentInChildren<Text>().text= randomUpperChar.ToString();  
        Destroy(gameObject);
    }
}
