using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DissolveController : MonoBehaviour
{
    public float dissolveAmount;
    public float dissolveSpeed;
    public bool isDissolving;
    public bool isDissolveComplete;
    [ColorUsageAttribute(true,true)]
    public Color outColor;
    [ColorUsageAttribute(true, true)]
    public Color inColor;

    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
       // mat = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDissolveComplete)
       // {
            //if (Input.GetKeyDown(KeyCode.A))
            //    isDissolving = true;

            //if (Input.GetKeyDown(KeyCode.S))
            //    isDissolving = false;

            if (isDissolving)
            {
                DissolveOut(/*dissolveSpeed, outColor*/);
            }

            //if (!isDissolving)
            //{
            //    DissolveIn(dissolveSpeed, inColor);
            //}
       // }
       
    }



    public void DissolveOut(/*float speed, Color color*/)
    {
        mat.SetFloat("_DissolveAmount", dissolveAmount);
        print("DissolveOut:"+gameObject.name);
      // gameObject.GetComponent<Image>().material=mat;
        mat.SetColor("_DissolveColor", outColor);
        if (dissolveAmount > -0.1)
            dissolveAmount -= Time.deltaTime * dissolveSpeed;
    }

    //public void DissolveIn(float speed, Color color)
    //{
    //    mat.SetColor("_DissolveColor", color);
    //    if (dissolveAmount < 1)
    //        dissolveAmount += Time.deltaTime * dissolveSpeed;
    //}
}
