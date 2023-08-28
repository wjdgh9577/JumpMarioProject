using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runningboy.Data
{
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
        [Header("Default BGMs")]
        [SerializeField, DictionaryDrawerSettings(KeyLabel = "Sector", ValueLabel = "Clip")]
        Dictionary<byte, BGMData> defaultBGMDataDic = new Dictionary<byte, BGMData>();

        [Header("Detail BGMs")]
        [SerializeField, DictionaryDrawerSettings(KeyLabel = "Sector/Section", ValueLabel = "Clip")]
        Dictionary<SectionData, BGMData> detailBGMDataDic = new Dictionary<SectionData, BGMData>();

        [Header("Sound Effects")]
        [SerializeField, DictionaryDrawerSettings(KeyLabel = "SFX ID", ValueLabel = "Clip")]
        Dictionary<string, SFXData> sfxDataDic = new Dictionary<string, SFXData>();

        public bool TryGetValue(SectionData sectionData, out BGMData bgmData)
        {
            if (detailBGMDataDic.TryGetValue(sectionData, out bgmData))
            {
                return true;
            }

            return defaultBGMDataDic.TryGetValue(sectionData.sectorNumber, out bgmData);
        }

        public bool TryGetValue(string sfxID, out SFXData sfxData)
        {
            return sfxDataDic.TryGetValue(sfxID, out sfxData);
        }
    }
}