//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//[CustomEditor(typeof(WordBranchGenerator))]
//public class WordBranchGeneratorEditor : Editor
//{
//    SerializedProperty wordBranchesProperty;
//    private SerializedProperty soundsProperty;

//    private void OnEnable()
//    {
//        wordBranchesProperty = serializedObject.FindProperty("wordBranches");
//        soundsProperty = serializedObject.FindProperty("soundsForWord");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        EditorGUILayout.PropertyField(wordBranchesProperty, true);
//        EditorGUILayout.PropertyField(soundsProperty, true);

//        if (GUILayout.Button("Add Word Branch"))
//        {
//            WordBranchGenerator wordBranchGenerator = (WordBranchGenerator)target;
//            ArrayUtility.Add(ref wordBranchGenerator.wordBranches, new WordBranch());
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}