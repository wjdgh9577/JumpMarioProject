using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Runningboy.Data
{
    public class BackgroundData : SerializedScriptableObject
    {
        [SerializeField, DictionaryDrawerSettings(KeyLabel = "Sector", ValueLabel = "Sprite")]
        Dictionary<byte, Sprite> backgroundData = new Dictionary<byte, Sprite>();

        public bool TryGetValue(byte sectorNumber, out Sprite background)
        {
            return backgroundData.TryGetValue(sectorNumber, out background);
        }
    }
}