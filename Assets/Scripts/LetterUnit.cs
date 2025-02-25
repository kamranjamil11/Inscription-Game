using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterUnit : MonoBehaviour
{
    public GameObject selected_Sprite, un_Selected_Sprite;
    public TextMeshPro text;
    public Transform lineTarget;
    public Transform hintTargetPosition;
    public int column;
    public int row;
    public bool isPartOfWord = false;
    public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";// "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string upperCase_Alphabet =  "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static LetterUnit over = null;
    public BoardManager boardManager;
    public char Letter
    {
        set
        {
            _letter = value;
            text.text = _letter.ToString()/*.ToLower()*/;
            isPartOfWord = true;
        }
        get
        {
            return _letter;
        }
    }
    private char _letter;
    void Start()
    {
        Reset();
      // boardManager=FindObjectOfType<BoardManager>();
    }
    public void Reset()
    {
        Letter = Alphabet/*.ToLower()*/.ToCharArray().GetRandom();
        isPartOfWord = false;
    }
    //private void OnMouseDown()
    //{
    //    selected_Sprite.SetActive(true);
    //    un_Selected_Sprite.SetActive(false);
    //}
    //private void OnMouseUp()
    //{
    //    selected_Sprite.SetActive(true);
    //    un_Selected_Sprite.SetActive(false);
    //}
    //private void OnMouseUpAsButton()
    //{
    //    selected_Sprite.SetActive(true);
    //    un_Selected_Sprite.SetActive(false);
    //}
    void OnMouseEnter()
    {
        over = this;
       // boardManager.selected_Letters += text;
       // selected_Sprite.SetActive(true);
        //un_Selected_Sprite.SetActive(false);
        print("Enter");
        
    }
    void OnMouseExit()
    {
        if (over == this)
        {
           // selected_Sprite.SetActive(false);
            //un_Selected_Sprite.SetActive(true);
            over = null;
        }
    }
}
