using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;
using System;
using System.Linq;


public class LevelCreator : MonoBehaviour
{
    public static bool isPlay = false;
    public static string levelWord;
    public List<string> totalWords = new List<string>();
    public List<SingleLetter> lettersGrid = new List<SingleLetter>();
    public List<GameObject> hintObjs = new List<GameObject>();
    public List<int> hints_Index = new List<int>();
    private int wayIndex;
    private List<string> alphabets = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", };
    public List<char> weightedList = new List<char>();
    public int gridSize = 16; // Total boxes in the grid (4x4)
    private char[] grid;
    void Start()
    {
        // if (!isPlay)
        // {
        isPlay = true;

        // }
        CreatWord();
    }
    public void CreatWord()
    {
        Dictionary<char, float> alphabetProbabilities = new Dictionary<char, float>()
        {
            { 'A', 8.2f }, { 'B', 1.5f }, { 'C', 2.8f }, { 'D', 4.3f }, { 'E', 13f },
            { 'F', 2.2f }, { 'G', 1.7f }, { 'H', 5.9f }, { 'I', 7f }, { 'J', 0.2f },
            { 'K', 0.8f }, { 'L', 4f }, { 'M', 2.4f }, { 'N', 6.7f }, { 'O', 7.5f },
            { 'P', 1.6f }, { 'Q', 0.1f }, { 'R', 6f }, { 'S', 6.3f }, { 'T', 9.1f },
            { 'U', 2.8f }, { 'V', 1f }, { 'W', 1.9f }, { 'X', 0.2f }, { 'Y', 2f },
            { 'Z', 0.1f }
        };

        // 2. Generate Cumulative Probability List
        List<KeyValuePair<char, float>> cumulativeList = new List<KeyValuePair<char, float>>();
        float cumulativeSum = 0f;

        foreach (var item in alphabetProbabilities)
        {
            cumulativeSum += item.Value;
            cumulativeList.Add(new KeyValuePair<char, float>(item.Key, cumulativeSum));
        }
        // 3. Generate and Fill the 16-box Grid
        grid = new char[gridSize];
        FillGrid(cumulativeList, cumulativeSum);
        PrintGrid();
        fillTheRest();
    }
    void FillGrid(List<KeyValuePair<char, float>> cumulativeList, float maxProbability)
    {
        weightedList.Clear();
        System.Random random = new System.Random();

        for (int i = 0; i < gridSize; i++)
        {
            float randValue = (float)random.NextDouble() * maxProbability;

            foreach (var item in cumulativeList)
            {
                if (randValue <= item.Value)
                {
                    grid[i] = item.Key;
                    weightedList.Add(item.Key);
                    break;
                }
            }
        }
    }

    // Function to Print Grid in Console
    void PrintGrid()
    {
        string gridOutputResult = "";
        for (int i = 0; i < gridSize; i++)
        {
            gridOutputResult += grid[i] + " ";
            if ((i + 1) % 4 == 0) gridOutputResult += "\n"; // Break every 4 elements (4x4 grid)

        }
        Debug.Log(gridOutputResult);

        gridOutPut = ConvertStringToGrid(gridOutputResult);
        print(gridOutPut[0, 1]);
        WordsFinder();
    }
    //public void NewWord() 
    //{
    //   // int lvl = PlayerPrefs.GetInt("LEVEL_NUMBER");
    //   // print("Lvl: "+ lvl);
    //   // if (lvl < totalWords.Count)
    //    //{
    //       // levelWord = totalWords[lvl]; //PlayerPrefs.GetString("lvlWord");
    //       // createLevel();
    //        //fillTheRest();
    //    //}
    //}
    //private void createLevel()
    //{
    //    //place the first letter randomly on the grid
    //    int currLetter = Random.Range(0, lettersGrid.Count);
    //    lettersGrid[currLetter].Value = levelWord[0].ToString();
    //    lettersGrid[currLetter].GetComponentInChildren<Text>().text = levelWord[0].ToString();
    //    //remove the first placed letter form all letter that have a way to it
    //    foreach (SingleLetter way in lettersGrid)
    //    {
    //        if (way.tempPossibleWays.Contains(lettersGrid[currLetter].gameObject))
    //        {
    //            way.tempPossibleWays.Remove(lettersGrid[currLetter].gameObject);
    //        }
    //    }

    //    for (int i = 1; i < levelWord.Length; i++)
    //    {
    //        //pic a possible way
    //        wayIndex = Random.Range(0, lettersGrid[currLetter].tempPossibleWays.Count);
    //       // print("wayIndex: " + wayIndex);
    //        //test if the way is valid (isn't used before) else remove it from possible ways list and generated new index for a new way
    //        do
    //        {
    //            lettersGrid[currLetter].tempPossibleWays.Remove(lettersGrid[currLetter].tempPossibleWays[wayIndex].gameObject);
    //            //calculate new value
    //            wayIndex = Random.Range(0, lettersGrid[currLetter].tempPossibleWays.Count);
    //          //  print("wayIndex1: " + wayIndex);
    //            // if there is no way restart the scene to re - execute the code :*(yaatini bwsa rani maalem)
    //            if (lettersGrid[currLetter].tempPossibleWays.Count == 0)
    //            {
    //                SceneManager.LoadScene("Game");
    //                return;
    //            }
    //        }
    //        while (lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().Value != "");
    //        //is valid way 9om bel wejeb :)
    //        lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().Value = levelWord[i].ToString();
    //        lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>().GetComponentInChildren<Text>().text = levelWord[i].ToString();
    //        int tempLetterIndex = lettersGrid.IndexOf(lettersGrid[currLetter].tempPossibleWays[wayIndex].GetComponent<SingleLetter>());
    //        currLetter = tempLetterIndex;
    //       // print("wayIndex: " + wayIndex + "currLetter: " + currLetter);
    //    }
    //}
    private void fillTheRest()
    {
        hintObjs.Clear();
        //foreach (SingleLetter letter in lettersGrid)
        //{
        //    if (letter.Value == "")
        //    {
        //        int valIndex = Random.Range(0, alphabets.Count);
        //        letter.Value = alphabets[valIndex];
        //        letter.gameObject.GetComponentInChildren<Text>().text = alphabets[valIndex].ToString();
        //    }
        //    else
        //    {
        //        hintObjs.Add(letter.gameObject);
        //        // print("main: "+letter.gameObject.GetComponentInChildren<Text>().text);
        //    }
        //}
        for (int i = 0; i < weightedList.Count; i++)
        {
            // if (lettersGrid[i].Value == "")
            //{
            lettersGrid[i].GetComponent<SingleLetter>().Value = weightedList[i].ToString();
            lettersGrid[i].gameObject.GetComponentInChildren<Text>().text = weightedList[i].ToString();
            //}
        }
        for (int i = 0; i < hints_Index.Count; i++)
        {
            hintObjs.Add(lettersGrid[hints_Index[i]].gameObject);
        }
    }

    char[,] ConvertStringToGrid(string input)
    {
        string[] rows = input.Split('\n'); // Split by new line
        int size = rows.Length - 1;
        print("Row_size: " + size);
        char[,] grid = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            string[] chars = rows[i].Trim().Split(' '); // Split by space
            for (int j = 0; j < size; j++)
            {
                grid[i, j] = chars[j][0]; // Store character in grid
            }
        }
        return grid;
    }


    char[,] gridOutPut; //=
                        //{
                        //    { 'F', 'R', 'I', 'C' },
                        //    { 'I', 'M', 'T', 'O' },
                        //    { 'D', 'E', 'G', 'H' },
                        //    { 'L', 'H', 'L', 'T' }
                        //};

    // HashSet<string> dictionary = new HashSet<string>()
    //{
    //    "DOG", "FRIEND", "RICH", "TIME", "FIGHT", "LIGHT", "TILE", "HOT", "HIT",
    //    "IM", "DE", "GE", "LHL", "TIL", "IC", "TO", "DO", "ME", "IT"
    //};

    int rows = 4, cols = 4;
   // HashSet<string> foundWords = new HashSet<string>();
    List<(string, List<int>)> foundWords = new List<(string, List<int>)>();
    // 8 possible directions (left, right, up, down, diagonals)
    int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
    int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

    void WordsFinder()
    {
        foundWords.Clear();
        bool[,] visited = new bool[rows, cols];

        // Har letter se DFS start karein
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // DFS(i, j, "", visited, 0);
                DFS(i, j, "", new List<int>(), visited);
            }
        }

        // Print all found words
        print("Words Found in Grid:");
        //foreach (string word in foundWords)
        //{
        //    print(word);
        //}
        foreach (var word in foundWords)
        {
            hints_Index = word.Item2;
            print($"Word: {word.Item1}, Indexes: [{string.Join(", ", word.Item2)}]");
            break;
        }
        
    }
     void DFS(int i, int j, string currentWord, List<int> indexes, bool[,] visited)
    {
        if (foundWords.Count < 6)
        {
            // Out of bounds check or already visited
            if (i < 0 || j < 0 || i >= rows || j >= cols || visited[i, j])
                return;

            // Letter ko add karein
            currentWord += gridOutPut[i, j];
            indexes.Add((i * cols) + j);  // 1D index calculation
            visited[i, j] = true;

            // Agar dictionary mein hai, to word + full path indexes save karein
            //if (dictionary.Contains(currentWord))
            //{
            //    foundWords.Add((currentWord, new List<int>(indexes)));
            //}
            if (gameObject.GetComponent<GameController>().wordSet.Contains(currentWord.ToLower()))
            {
                foundWords.Add((currentWord, new List<int>(indexes)));
            }

            // Next letter ke liye adjacent check karein
            for (int d = 0; d < 8; d++)
            {
                int newX = i + dx[d];
                int newY = j + dy[d];

                // Sirf physically connected letter ko allow karein
                if (newX >= 0 && newY >= 0 && newX < rows && newY < cols && !visited[newX, newY])
                {
                    if (foundWords.Count < 6)
                    {
                        DFS(newX, newY, currentWord, indexes, visited);
                    }
                }
            }

            // Backtracking (visited mark ko remove karein aur last index delete karein)
            visited[i, j] = false;
            indexes.RemoveAt(indexes.Count - 1);
        }
    }
    //void DFS(int i, int j, string currentWord, bool[,] visited, int depth)
    //{
    //    if (foundWords.Count < 6)
    //    {
    //        // Agar depth zyada ho jaye (10 letters se zyada ka word avoid karein)
    //        if (depth > 10)
    //            return;

    //        // Out of bounds check or already visited
    //        if (i < 0 || j < 0 || i >= rows || j >= cols || visited[i, j])
    //            return;

    //        // Letter ko add karein
    //        currentWord += gridOutPut[i, j];
    //        visited[i, j] = true;

    //        // Agar dictionary mein hai, to save karein
    //        if (gameObject.GetComponent<GameController>().wordSet.Contains(currentWord.ToLower()))
    //        {
    //            foundWords.Add(currentWord);
    //        }

    //        // Next letter ke liye adjacent check karein
    //        for (int d = 0; d < 8; d++)
    //        {
    //            int newX = i + dx[d];
    //            int newY = j + dy[d];
    //            if (foundWords.Count < 6)
    //            {
    //                DFS(newX, newY, currentWord, visited, depth + 1);
    //            }
    //        }

    //        // Backtracking (visited mark ko remove karein)
    //        visited[i, j] = false;
    //    }
    //}

}


