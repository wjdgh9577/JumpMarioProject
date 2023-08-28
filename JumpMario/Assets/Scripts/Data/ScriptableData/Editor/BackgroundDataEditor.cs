using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Runningboy.Data
{
    public class BackgroundDataEditor : Editor
    {
        const string scriptableObjectFolderPath = "Assets/Res";
        const string scriptableObjectFolderName = "ScriptableObjects";
        const string scriptableObjectName = "BackgroundData.asset";

        public static BackgroundData GetOrCreateSettingAsset()
        {
            string fullPath = Path.Combine(Path.Combine(scriptableObjectFolderPath, scriptableObjectFolderName), scriptableObjectName);
            BackgroundData instance = AssetDatabase.LoadAssetAtPath(fullPath, typeof(BackgroundData)) as BackgroundData;

            if (instance == null)
            {
                if (!Directory.Exists(Path.Combine(scriptableObjectFolderPath, scriptableObjectFolderName)))
                {
                    AssetDatabase.CreateFolder(scriptableObjectFolderPath, scriptableObjectFolderName);
                }

                instance = CreateInstance<BackgroundData>();
                AssetDatabase.CreateAsset(instance, fullPath);
                AssetDatabase.SaveAssets();
            }

            return instance;
        }

        [MenuItem("Tools/Runningboy/ScriptableDatas/BackgroundData")]
        public static void Edit()
        {
            Selection.activeObject = GetOrCreateSettingAsset();
        }
    }
}