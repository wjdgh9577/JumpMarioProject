using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundDataEditor : Editor
{
    const string scriptableObjectFolderPath = "Assets/Res";
    const string scriptableObjectFolderName = "ScriptableObjects";
    const string scriptableObjectName = "SoundData.asset";

    public static SoundData GetOrCreateSettingAsset()
    {
        string fullPath = Path.Combine(Path.Combine(scriptableObjectFolderPath, scriptableObjectFolderName), scriptableObjectName);
        SoundData instance = AssetDatabase.LoadAssetAtPath(fullPath, typeof(SoundData)) as SoundData;

        if (instance == null)
        {
            if (!Directory.Exists(Path.Combine(scriptableObjectFolderPath, scriptableObjectFolderName)))
            {
                AssetDatabase.CreateFolder(scriptableObjectFolderPath, scriptableObjectFolderName);
            }

            instance = CreateInstance<SoundData>();
            AssetDatabase.CreateAsset(instance, fullPath);
            AssetDatabase.SaveAssets();
        }

        return instance;
    }

    [MenuItem("Tools/Runningboy/ScriptableDatas/SoundData")]
    public static void Edit()
    {
        Selection.activeObject = GetOrCreateSettingAsset();
    }
}