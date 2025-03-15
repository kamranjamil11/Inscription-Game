using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyChallenge : MonoBehaviour
{
    public int id;
    public Text description;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}
    private void OnDisable()
    {
            Destroy(gameObject);
    }

}
