//using UnityEngine;
//using UnityEditor;
//using UnityEditorInternal;
//using System.Collections.Generic;

//public class WordDataEditor : EditorWindow
//{
//    private ReorderableList wordDataList;
//    private string savePath = "Assets/words.json";
//    private List<WordData> wordDataArray;

//    private void OnEnable()
//    {
//        // Load existing word data from JSON (if available)
//        if (System.IO.File.Exists(savePath))
//        {
//            string jsonData = System.IO.File.ReadAllText(savePath);
//            wordDataArray = JsonUtility.FromJson<WordDataWrapper>(jsonData).wordDataList;
//        }
//        else
//        {
//            wordDataArray = new List<WordData>();
//        }

//        // Create the ReorderableList
//        wordDataList = new ReorderableList(wordDataArray, typeof(WordData), true, true, true, true);

//        // Draw the elements in the ReorderableList
//        wordDataList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Word Data");
//        wordDataList.drawElementCallback = (rect, index, isActive, isFocused) =>
//        {
//            var element = wordDataArray[index];
//            rect.y += 2;

//            EditorGUI.BeginChangeCheck();
//            element.word = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.word);
//            element.soundPath = EditorGUI.TextField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width, EditorGUIUtility.singleLineHeight), element.soundPath);
//            if (EditorGUI.EndChangeCheck())
//            {
//                SaveWordDataToJson();
//            }
//        };

//        // Add a callback for adding elements to the ReorderableList
//        wordDataList.onAddCallback += OnAddWordData;
//    }

//    [MenuItem("Custom/Generate Words JSON")]
//    private static void ShowWindow()
//    {
//        GetWindow<WordDataEditor>("Generate Words JSON");
//    }

//    private void OnGUI()
//    {
//        // Display the ReorderableList
//        wordDataList.DoLayoutList();

//        // Save button
//        if (GUILayout.Button("Save JSON"))
//        {
//            SaveWordDataToJson();
//        }
//    }

//    private void SaveWordDataToJson()
//    {
//        WordDataWrapper wrapper = new WordDataWrapper();
//        wrapper.wordDataList = wordDataArray;

//        string jsonData = JsonUtility.ToJson(wrapper, true);
//        System.IO.File.WriteAllText(savePath, jsonData);
//        Debug.Log("Words JSON saved to: " + savePath);
//    }

//    private void OnAddWordData(ReorderableList list)
//    {
//        // Create a new WordData element and add it to the list
//        wordDataArray.Add(new WordData());
//        SaveWordDataToJson();
//    }
//}

//[System.Serializable]
//public class WordDataWrapper
//{
//    public List<WordData> wordDataList;
//}
