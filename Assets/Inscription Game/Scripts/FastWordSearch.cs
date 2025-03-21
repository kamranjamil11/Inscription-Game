//using Unity.Burst;
//using Unity.Collections;
//using Unity.Jobs;
//using UnityEngine;
//using System;
//public class FastWordSearch : MonoBehaviour
//{
//    private NativeArray<char> grid;
//    private NativeHashMap<int, bool> dictionary;
//    private NativeList<WordResult> foundWords;

//    private int rows = 4, cols = 4;

//    private void Start()
//    {
//        char[] gridData = {
//            'F', 'R', 'I', 'C',
//            'I', 'M', 'T', 'O',
//            'D', 'E', 'G', 'H',
//            'L', 'H', 'L', 'T'
//        };
//        grid = new NativeArray<char>(gridData, Allocator.Persistent);

//        dictionary = new NativeHashMap<int, bool>(20, Allocator.Persistent);
//        string[] words = { "DOG", "FRIEND", "RICH", "TIME", "FIGHT", "LIGHT", "TILE", "HOT", "HIT", "IM", "DE", "GE", "LHL", "TIL", "IC", "TO", "DO", "ME", "IT" };
//        foreach (var word in words)
//        {
//            dictionary.TryAdd(HashWord(word), true);
//        }

//        // 🔥 Preallocate enough space to avoid resize failures
//        foundWords = new NativeList<WordResult>(1000, Allocator.Persistent);


//        WordSearchJob job = new WordSearchJob
//        {
//            Grid = grid,
//            Dictionary = dictionary,
//            FoundWords = foundWords.AsParallelWriter(),
//            Rows = rows,
//            Cols = cols
//        };

//        JobHandle handle = job.Schedule(rows * cols, 1);
//        handle.Complete();

//        foreach (var result in foundWords)
//        {
//            Debug.Log($"Found: {result.Word}");
//        }
//    }

//    private void OnDestroy()
//    {
//        if (grid.IsCreated) grid.Dispose();
//        if (dictionary.IsCreated) dictionary.Dispose();
//        if (foundWords.IsCreated) foundWords.Dispose();
//    }

//    private int HashWord(string word)
//    {
//        int hash = 0;
//        foreach (char c in word)
//        {
//            hash = (hash * 31) + c;
//        }
//        return hash;
//    }
//}

//[BurstCompile]
//public struct WordSearchJob : IJobParallelFor
//{
//    [ReadOnly] public NativeArray<char> Grid;
//    [ReadOnly] public NativeHashMap<int, bool> Dictionary;
//    public NativeList<WordResult>.ParallelWriter FoundWords;
//    public int Rows;
//    public int Cols;

//    private static readonly int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
//    private static readonly int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

//    public void Execute(int index)
//    {
//        int row = index / Cols;
//        int col = index % Cols;
//        FixedString32Bytes word = "";
//        word.Clear();
//        word.Append(Grid[index]);
//        Explore(row, col, word, 0);
//    }

//    private void Explore(int row, int col, FixedString32Bytes currentWord, int depth)
//    {
//        if (depth > 10) return;

//        if (Dictionary.ContainsKey(HashWord(currentWord)))
//        {
//            // ✅ Ensure capacity before adding new words
//            //if (FoundWords.ListCapacity > FoundWords.Length) // Ensure we have space
//            //{
//            //    FoundWords.Add(new WordResult { Word = currentWord });
//            //}
//            FoundWords.AddNoResize(new WordResult { Word = currentWord });
//        }

//        for (int d = 0; d < 8; d++)
//        {
//            int newX = row + dx[d];
//            int newY = col + dy[d];
//            if (newX >= 0 && newY >= 0 && newX < Rows && newY < Cols)
//            {
//                if (currentWord.Length < currentWord.Capacity)  // ✅ Prevent overflow
//                {
//                    FixedString32Bytes newWord = currentWord;
//                    newWord.Append(Grid[newX * Cols + newY]);
//                    Explore(newX, newY, newWord, depth + 1);
//                }
//            }
//        }
//    }

//    private int HashWord(FixedString32Bytes word)
//    {
//        int hash = 0;
//        for (int i = 0; i < word.Length; i++)
//        {
//            hash = (hash * 31) + word[i];
//        }
//        return hash;
//    }
//}

//public struct WordResult
//{
//    public FixedString32Bytes Word;
//}
