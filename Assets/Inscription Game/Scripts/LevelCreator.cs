using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BiffeProd
{
    public class LevelCreator : MonoBehaviour
    {
        public static bool isPlay=false;
        public static string levelWord;
        public List<string> totalWords = new List<string>();
        public List<SingleLetter> lettersGrid = new List<SingleLetter>();
        public List<GameObject> hintObjs=new List<GameObject>();
        private int wayIndex;
        private List<string> alphabets = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", };
        void Start()
        {
           // if (!isPlay)
           // {
                isPlay=true;
                NewWord();
           // }
        }
        public void NewWord() 
        {
            int lvl = PlayerPrefs.GetInt("LEVEL_NUMBER");
            print("Lvl: "+ lvl);
            if (lvl < totalWords.Count)
            {
                levelWord = totalWords[lvl]; //PlayerPrefs.GetString("lvlWord");
                createLevel();
                fillTheRest();
            }
        }
        private void createLevel()
        {
            //place the first letter randomly on the grid
            int currLetter = Random.Range(0, lettersGrid.Count);
            lettersGrid[currLetter].Value = levelWord[0].ToString();
            lettersGrid[currLetter].GetComponentInChildren<Text>().text = levelWord[0].ToString();
            //remove the first placed letter form all letter that have a way to it
            foreach (SingleLetter way in lettersGrid)
            {
                if (way.tempPossibleWays.Contains(lettersGrid[currLetter].gameObject))
                {
                    way.tempPossibleWays.Remove(lettersGrid[currLetter].gameObject);
                }
            }

            for (int i = 1; i < levelWord.Length; i++)
            {
                //pic a possible way
                wayIndex = Random.Range(0, lettersGrid[currLetter].tempPossibleWays.Count);
                print("wayIndex: " + wayIndex);
                //test if the way is valid (isn't used before) else remove it from possible ways list and generated new index for a new way
                do
                {
                    lettersGrid[currLetter].tempPossibleWays.Remove(lettersGrid[currLetter].tempPossibleWays[wayIndex].gameObject);
                    //calculate new value
                    wayIndex = Random.Range(0, lettersGrid[currLetter].tempPossibleWays.Count);
                    print("wayIndex1: " + wayIndex);
                    // if there is no way restart the scene to re - execute the code :*(yaatini bwsa rani maalem)
                    if (lettersGrid[currLetter].tempPossibleWays.Count == 0)
                    {
                        SceneManager.LoadScene("Game");
                        return;
                    }
                }
                while (lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().Value != "");
                //is valid way 9om bel wejeb :)
                lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().Value = levelWord[i].ToString();
                lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().GetComponentInChildren<Text>().text = levelWord[i].ToString();
                int tempLetterIndex = lettersGrid.IndexOf(lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>());
                currLetter = tempLetterIndex;
                print("wayIndex: " + wayIndex + "currLetter: " + currLetter);
            }
        }
        private void fillTheRest()
        {
            hintObjs.Clear();
            foreach (SingleLetter letter in lettersGrid)
            {
                if (letter.Value == "")
                {
                    int valIndex = Random.Range(0, alphabets.Count);
                    letter.Value = alphabets[valIndex];
                    letter.gameObject.GetComponentInChildren<Text>().text = alphabets[valIndex].ToString();
                }
                else 
                {
                    hintObjs.Add(letter.gameObject);
                    print("main: "+letter.gameObject.GetComponentInChildren<Text>().text);
                }
            }
        }
    }

}
