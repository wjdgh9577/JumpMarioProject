using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public struct BGMData
{
    public AudioClip introClip;
    public AudioClip loopClip;
}

[Serializable]
public struct SFXData
{
    public AudioClip sfxClip;
    public bool loop;
}

public class SoundData : SerializedScriptableObject
{
    [Header("BGMs")]
    [SerializeField, DictionaryDrawerSettings(KeyLabel = "BGM ID", ValueLabel = "Clip")]
    Dictionary<string, BGMData> bgmDataDic = new Dictionary<string, BGMData>();

    [Header("Sound Effects")]
    [SerializeField, DictionaryDrawerSettings(KeyLabel = "SFX ID", ValueLabel = "Clip")]
    Dictionary<string, SFXData> sfxDataDic = new Dictionary<string, SFXData>();

    public bool TryGetValue(string bgmID, out BGMData bgmData)
    {
        return bgmDataDic.TryGetValue(bgmID, out bgmData);
    }

    public bool TryGetValue(string sfxID, out SFXData sfxData)
    {
        return sfxDataDic.TryGetValue(sfxID, out sfxData);
    }
}