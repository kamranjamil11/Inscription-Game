//using UnityEngine;
//using UnityEditor;

//public class BatchTextureFormatChanger : EditorWindow
//{
//    [MenuItem("Tools/Change All Texture Formats")]
//    public static void ChangeTextureFormats()
//    {
//        string[] textureGUIDs = AssetDatabase.FindAssets("t:Texture2D"); // Get all textures
//        int count = textureGUIDs.Length;

//        for (int i = 0; i < count; i++)
//        {
//            string path = AssetDatabase.GUIDToAssetPath(textureGUIDs[i]);
//            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

//            if (textureImporter != null)
//            {
//                // Change texture format for Android
//                //TextureImporterPlatformSettings androidSettings = new TextureImporterPlatformSettings
//                //{
//                //    name = "Android",
//                //    overridden = true,
//                //    format = TextureImporterFormat.ASTC_6x6, // Change to ASTC 6x6
//                //};
//                //textureImporter.SetPlatformTextureSettings(androidSettings);

//                // Change texture format for WebGL
//                TextureImporterPlatformSettings webGLSettings = new TextureImporterPlatformSettings
//                {
//                    name = "WebGL",
//                    maxTextureSize = 1024,
//                    overridden = true,
//                    format = TextureImporterFormat.ASTC_6x6, // Change to ETC2 RGBA8
//                };
//                textureImporter.SetPlatformTextureSettings(webGLSettings);

//                // Apply changes
//                AssetDatabase.ImportAsset(path);
//                Debug.Log($"Texture format changed: {path}");
//            }
//        }

//        AssetDatabase.SaveAssets();
//        Debug.Log($"✅ All {count} textures updated successfully!");
//    }
//}
