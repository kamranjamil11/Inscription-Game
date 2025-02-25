using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UIData", menuName = "UIGameData", order = 6)]
public class UiDataHolder : ScriptableObject
{
   public Sprite backGround;
   public Sprite IntroPannel;
   [Multiline]
   public string IntroText;
   public Color introOutline;
   public string Heading;
   public Color headingOutline;

   [Header("DifficultScreen")] public Sprite difficultyPannel;
   public string Title;
   public Color titleColor;
   public Sprite sliderSprite;
   public Sprite knobSprite;
   
}
