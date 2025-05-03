using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;
using System;
using System.Linq;
using System.IO;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;





public class LevelCreator : MonoBehaviour
{    
    public static string levelWord;
    public List<SingleLetter> lettersGrid = new List<SingleLetter>();
    public List<SingleLetter> lotusPowerGrid = new List<SingleLetter>();
    public List<GameObject> hintObjs = new List<GameObject>();
    public List<int> hints_Index = new List<int>();
     
    public List<char> weightedList = new List<char>();
    public int gridSize = 16; // Total boxes in the grid (4x4)
    public List<char> grid;

    public GameController gameController;
    //void Start()
    //{
    //    gameController = gameObject.GetComponent<GameController>();       
    //}

    public void CreatWord(string id)
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
        // grid = new char[gridSize];
        //grid = new List<char>(gridSize);
        if (id == "FirstTime")
        {
            FillGrid(cumulativeList, cumulativeSum);
            PrintGrid();
            fillTheRest();
        }
        else
        {
            NextFillGrid(cumulativeList, cumulativeSum);
            Invoke("PrintGrid",1); //PrintGrid();
            Invoke("NextFillTheRest", 1);// NextFillTheRest();          
        }



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
                    //grid[i] = item.Key;
                    grid.Add(item.Key);
                    weightedList.Add(item.Key);
                    break;
                }
            }
        }
    }
    void NextFillGrid(List<KeyValuePair<char, float>> cumulativeList, float maxProbability)
    {

        System.Random random = new System.Random();

        for (int i = 0; i < gameController.activeLetters.Count; i++)
        {
            int value = gameController.activeLetters[i].GetComponent<SingleLetter>().id;
            grid[value] = ' ';
            weightedList[value] = ' ';
        }
        for (int i = 0; i < gridSize; i++)
        {
            float randValue = (float)random.NextDouble() * maxProbability;

            foreach (var item in cumulativeList)
            {
                if (randValue <= item.Value)
                {
                    foreach (var item1 in grid)
                    {
                        if (item1 == ' ')
                        {
                            int value = gameController.activeLetters[i].GetComponent<SingleLetter>().id;

                            grid[value] = item.Key;
                            weightedList[value] = (item.Key);
                            lettersGrid[value].GetComponent<SingleLetter>().Value = item.Key.ToString();
                            lettersGrid[value].gameObject.GetComponentInChildren<Text>().text = item.Key.ToString();
                            break;
                        }
                    }
                    break;
                }
            }

        }
    }
    // Function to Print Grid in Console
   public void PrintGrid()
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

    private void fillTheRest()
    {
        hintObjs.Clear();

        for (int i = 0; i < weightedList.Count; i++)
        {
            lettersGrid[i].GetComponent<SingleLetter>().Value = weightedList[i].ToString();
            lettersGrid[i].gameObject.GetComponentInChildren<Text>().text = weightedList[i].ToString();
            lotusPowerGrid[i].gameObject.GetComponentInChildren<Text>().text = weightedList[i].ToString();
        }
        for (int i = 0; i < hints_Index.Count; i++)
        {
            hintObjs.Add(lettersGrid[hints_Index[i]].gameObject);
        }
        Invoke("LotusPanelOff", 1);
    }
    private void NextFillTheRest()
    {
        hintObjs.Clear();
       // gameController.lotusPowerPanel.SetActive(false);
        for (int i = 0; i < hints_Index.Count; i++)
        {
            hintObjs.Add(lettersGrid[hints_Index[i]].gameObject);
        }
        gameController.activeLetters.Clear();
        gameController.isNextWork = true;
       
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


    char[,] gridOutPut;
    int rows = 4, cols = 4;

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
                DFS(i, j, "", new List<int>(), visited);
            }
        }


      //  print("Words Found in Grid:");
        //foreach (var word in foundWords)
        //{
        //    //  hints_Index = word.Item2;
        //   // print($"Word: {word.Item1}, Indexes: [{string.Join(", ", word.Item2)}]");
        //    // break;        
        //}
        int rnd = UnityEngine.Random.Range(0, foundWords.Count);
        hints_Index = foundWords[rnd].Item2;
        print("Hint_Obj: " + foundWords[rnd].Item1);
        if (gameController != null)
        {
            gameController.hintButton.interactable = true;
            gameController.scrabButton.interactable = true;
            gameController.lotusButton.interactable = true;
            gameController.wordText.text = foundWords[rnd].Item1;
        }
    }
    void DFS(int i, int j, string currentWord, List<int> indexes, bool[,] visited)
    {
        if (foundWords.Count < 1)
        {
            // Out of bounds check or already visited
            if (i < 0 || j < 0 || i >= rows || j >= cols || visited[i, j])
                return;

            // Letter ko add karein
            currentWord += gridOutPut[i, j];
            indexes.Add((i * cols) + j);  // 1D index calculation
            visited[i, j] = true;

            // Agar dictionary mein hai, to word + full path indexes save karein
            if (currentWord.Length < 5)
            {
                if (gameObject.GetComponent<GameController>().wordSet.Contains(currentWord.ToLower()))
                {
                    foundWords.Add((currentWord, new List<int>(indexes)));
                }
            }

            // Next letter ke liye adjacent check karein
            for (int d = 0; d < 8; d++)
            {
                int newX = i + dx[d];
                int newY = j + dy[d];

                // Sirf physically connected letter ko allow karein
                if (newX >= 0 && newY >= 0 && newX < rows && newY < cols && !visited[newX, newY])
                {
                    if (foundWords.Count < 1)
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


}


