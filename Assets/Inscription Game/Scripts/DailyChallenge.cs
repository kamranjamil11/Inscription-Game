using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyChallenge : MonoBehaviour
{
    public int id;
    public Text description;
    public GameObject coins_Tab, claimed;
    private void OnDisable()
    {
       Destroy(gameObject);
    }

}
